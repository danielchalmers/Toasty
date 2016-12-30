using System;
using System.IO;
using System.Timers;
using Notiify.Interfaces;
using Notiify.Properties;

namespace Notiify.Classes
{
    public class DirectoryScanner : IScanner
    {
        private readonly FolderScanSettings _folderScanSettings;
        private readonly Action<string, WatcherChangeTypes> _onEvent;
        private readonly Timer _timer;
        private FileSystemWatcher _fileSystemWatcher;

        public DirectoryScanner(FolderScanSettings folderScanSettings,
            Action<string, WatcherChangeTypes> onEvent)
        {
            _folderScanSettings = folderScanSettings;
            _onEvent = onEvent;
            _timer = new Timer {Interval = Settings.Default.DirectoryWatcherResetInterval};
            _timer.Elapsed += Timer_OnElapsed;
            InitialiseFileSystemWatcher();
        }

        public void Start()
        {
            _timer.Start();
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

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
            OnDirectoryEvent(e.FullPath, e.ChangeType);
        }

        private void OnDirectoryEvent(string path, WatcherChangeTypes watcherChangeTypes)
        {
            _onEvent?.Invoke(path, watcherChangeTypes);
        }

        private void Timer_OnElapsed(object sender, ElapsedEventArgs e)
        {
            // File system watcher can stop working after waking up from sleep, directory becomes unavailable, etc.
            var fileSystemWatcherEnableRaisingEvents = _fileSystemWatcher.EnableRaisingEvents;
            DeinitialiseFileSystemWatcher();
            InitialiseFileSystemWatcher();
            _fileSystemWatcher.EnableRaisingEvents = fileSystemWatcherEnableRaisingEvents;

            //TODO: Should manually check for files that file system watcher missed.
        }
    }
}