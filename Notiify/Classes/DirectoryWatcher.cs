using System;
using System.IO;
using System.Timers;
using Notiify.Properties;

namespace Notiify.Classes
{
    public class DirectoryWatcher
    {
        private readonly DirectoryWatcherSettings _directoryWatcherSettings;
        private readonly Action<string, WatcherChangeTypes> _onEvent;
        private readonly Timer _timer;
        private FileSystemWatcher _fileSystemWatcher;

        public DirectoryWatcher(DirectoryWatcherSettings directoryWatcherSettings,
            Action<string, WatcherChangeTypes> onEvent)
        {
            _directoryWatcherSettings = directoryWatcherSettings;
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

        public void SetSettings(DirectoryWatcherSettings directoryWatcherSettings)
        {
            _fileSystemWatcher.Path = directoryWatcherSettings.Path;
            _fileSystemWatcher.IncludeSubdirectories = directoryWatcherSettings.Recursive;
            _fileSystemWatcher.Filter = "*.*";
        }

        private void InitialiseFileSystemWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher();
            SetSettings(_directoryWatcherSettings);
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