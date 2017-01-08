using System;
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
        private readonly object eventLock = new object();
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
            var eventData = new DirectoryScannerEventArgs(new FileInfo(e.FullPath), e.ChangeType);
            OnDirectoryEvent(eventData);
        }

        private void OnDirectoryEvent(DirectoryScannerEventArgs e)
        {
            lock (eventLock)
            {
                if (IsArgsDuplicatedInNotifications(e))
                {
                    return;
                }
                FileEvent?.Invoke(this, e);
            }
        }

        private bool IsArgsDuplicatedInNotifications(DirectoryScannerEventArgs args)
        {
            if (args.ChangeTypes != WatcherChangeTypes.Changed)
            {
                return false;
            }
            return
                App.Notifications.ToList().Select(x => x.Notification.ScannerArgs)
                    .OfType<DirectoryScannerEventArgs>()
                    .Select(x => x.FileInfo)
                    .Where(x => x.FullName == args.FileInfo.FullName)
                    .Any(
                        x =>
                            DidFileChangeTooQuickly(x, args.FileInfo));
        }

        private bool DidFileChangeTooQuickly(FileInfo originalFileInfo, FileInfo newFileInfo)
        {
            var timeSpan = newFileInfo.LastWriteTimeUtc - originalFileInfo.LastWriteTimeUtc;
            return timeSpan < Settings.Default.DuplicateFileChangeTimeout;
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
                    _folderScanSettings.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                var notificationFileInfos = App.Notifications.ToList().Select(x => x.Notification.ScannerArgs)
                    .OfType<DirectoryScannerEventArgs>()
                    .Select(x => x.FileInfo).ToList();
                foreach (var file in files.Select(path => new FileInfo(path)))
                {
                    var fileExists = false;
                    var fileChanged = true;
                    foreach (var x in notificationFileInfos.Where(x => x.FullName == file.FullName))
                    {
                        fileExists = true;
                        if (x.LastWriteTimeUtc == file.LastWriteTimeUtc)
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