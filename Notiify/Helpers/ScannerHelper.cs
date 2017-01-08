using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Notiify.Classes;
using Notiify.Interfaces;

namespace Notiify.Helpers
{
    public static class ScannerHelper
    {
        public static void ReloadScanners()
        {
            if (App.Scanners == null)
            {
                App.Scanners = new List<IScanner>();
            }
            else
            {
                App.Scanners.Clear();
            }
            foreach (var scanSettings in App.Sources.Select(source => source.ScanSettings))
            {
                IScanner scanner = null;
                var folderScanSettings = scanSettings as FolderScanSettings;
                if (folderScanSettings != null)
                {
                    var directoryWatcher = new DirectoryScanner(folderScanSettings);
                    directoryWatcher.FileEvent += (sender, args) =>
                    {
                        // Events can be running in another thread.
                        Application.Current.Dispatcher.Invoke(() => App.OnScannerEvent(directoryWatcher, args));
                    };
                    scanner = directoryWatcher;
                }
                if (scanner != null)
                {
                    App.Scanners.Add(scanner);
                    scanner.Start();
                }
            }
        }
    }
}