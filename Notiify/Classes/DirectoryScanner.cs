using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using Notiify.Interfaces;
using Notiify.Properties;

namespace Notiify.Classes
{
    public class DirectoryScanner : IScanner, IDisposable
    {
        private readonly object _fileCheckLock = new object();
        private readonly FolderScanSettings _folderScanSettings;
        private readonly Timer _timer;
        private Dictionary<string, DateTime> _fileInfoCache;
        private FileSystemWatcher _fileSystemWatcher;

        public DirectoryScanner(FolderScanSettings folderScanSettings)
        {
            _folderScanSettings = folderScanSettings;
            _timer = new Timer {Interval = Settings.Default.DirectoryWatcherResetInterval};
            _timer.Elapsed += Timer_OnElapsed;
            InitialiseFileSystemWatcher();
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
            DeinitialiseFileSystemWatcher();
            _fileSystemWatcher?.Dispose();
        }

        public void Start()
        {
            _timer.Start();
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public event EventHandler<DirectoryScannerEventArgs> FileEvent;

        public void SetSettings(FolderScanSettings folderScanSettings)
        {
            _fileSystemWatcher.Path = folderScanSettings.Path;
            _fileSystemWatcher.IncludeSubdirectories = folderScanSettings.Recursive;
            _fileSystemWatcher.Filter = folderScanSettings.IncludeFilter;
        }

        private void InitialiseFileSystemWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher();
            SetSettings(_folderScanSettings);
            _fileSystemWatcher.Created += OnFileSystemWatcherEvent;
            _fileSystemWatcher.Changed += OnFileSystemWatcherEvent;
            _fileSystemWatcher.Deleted += OnFileSystemWatcherEvent;
            _fileSystemWatcher.Renamed += OnFileSystemWatcherEvent;
        }

        private void DeinitialiseFileSystemWatcher()
        {
            if (_fileSystemWatcher == null)
            {
                return;
            }
            _fileSystemWatcher.Created -= OnFileSystemWatcherEvent;
            _fileSystemWatcher.Changed -= OnFileSystemWatcherEvent;
            _fileSystemWatcher.Deleted -= OnFileSystemWatcherEvent;
            _fileSystemWatcher.Renamed -= OnFileSystemWatcherEvent;
            _fileSystemWatcher.Dispose();
            _fileSystemWatcher = null;
        }

        private void OnFileSystemWatcherEvent(object sender, FileSystemEventArgs e)
        {
            var fileInfo = new FileInfo(e.FullPath);
            var eventData = new DirectoryScannerEventArgs(fileInfo, e.ChangeType);
            OnDirectoryEvent(eventData);
        }

        private void OnDirectoryEvent(DirectoryScannerEventArgs e)
        {
            if (IsArgsDuplicated(e))
            {
                return;
            }
            UpdateFileInfoCache(e.FileInfo.FullName, e.FileInfo.LastWriteTimeUtc);
            FileEvent?.Invoke(this, e);
        }

        private bool IsArgsDuplicated(DirectoryScannerEventArgs args)
        {
            if (args.ChangeTypes != WatcherChangeTypes.Changed)
            {
                return false;
            }
            return
                _fileInfoCache
                    .Where(x => x.Key == args.FileInfo.FullName)
                    .Any(
                        x =>
                            DidFileChangeTooQuickly(x.Value, args.FileInfo));
        }

        private bool DidFileChangeTooQuickly(DateTime writeDateTime, FileInfo newFileInfo)
        {
            var timeSpan = newFileInfo.LastWriteTimeUtc - writeDateTime;
            return timeSpan < Settings.Default.DuplicateFileChangeTimeout;
        }

        private void UpdateFileInfoCache(string path, DateTime writeDateTime)
        {
            if (!_fileInfoCache.ContainsKey(path))
            {
                _fileInfoCache.Add(path, writeDateTime);
            }
            else
            {
                _fileInfoCache[path] = writeDateTime;
            }
        }

        private void Timer_OnElapsed(object sender, ElapsedEventArgs e)
        {
            // File system watcher can stop working after waking up from sleep, directory becomes unavailable, etc.
            var fileSystemWatcherEnableRaisingEvents = _fileSystemWatcher.EnableRaisingEvents;
            DeinitialiseFileSystemWatcher();
            InitialiseFileSystemWatcher();
            _fileSystemWatcher.EnableRaisingEvents = fileSystemWatcherEnableRaisingEvents;

            CheckForFiles();
        }

        private void CheckForFiles()
        {
            lock (_fileCheckLock)
            {
                var files = Directory.EnumerateFiles(_folderScanSettings.Path, _folderScanSettings.IncludeFilter,
                    _folderScanSettings.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    .Select(path => new FileInfo(path));
                if (_fileInfoCache == null)
                {
                    _fileInfoCache = new Dictionary<string, DateTime>();
                    foreach (var file in files)
                    {
                        UpdateFileInfoCache(file.FullName, file.LastWriteTimeUtc);
                    }
                    return;
                }
                foreach (var file in files)
                {
                    var fileExists = false;
                    var fileChanged = true;
                    foreach (var x in _fileInfoCache.Where(x => x.Key == file.FullName))
                    {
                        fileExists = true;
                        if (x.Value == file.LastWriteTimeUtc)
                        {
                            fileChanged = false;
                        }
                    }
                    if (fileExists && fileChanged)
                    {
                        OnDirectoryEvent(new DirectoryScannerEventArgs(file, WatcherChangeTypes.Changed));
                    }
                    if (!fileExists)
                    {
                        OnDirectoryEvent(new DirectoryScannerEventArgs(file, WatcherChangeTypes.Created));
                    }
                }
            }
        }
    }
}