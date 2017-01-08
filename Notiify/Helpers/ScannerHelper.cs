using System.Collections.Generic;
using System.Linq;
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
                DisposeScanners();
            }
            foreach (var scanSettings in App.Sources.Select(source => source.ScanSettings))
            {
                IScanner scanner = null;
                var folderScanSettings = scanSettings as FolderScanSettings;
                if (folderScanSettings != null)
                {
                    var directoryWatcher = new DirectoryScanner(folderScanSettings);
                    directoryWatcher.FileEvent += App.OnScannerEvent;
                    scanner = directoryWatcher;
                }
                if (scanner != null)
                {
                    App.Scanners.Add(scanner);
                    scanner.Start();
                }
            }
        }

        private static void DisposeScanners()
        {
            if (App.Scanners == null)
            {
                return;
            }
            foreach (var scanner in App.Scanners)
            {
                var directoryScanner = scanner as DirectoryScanner;
                if (directoryScanner != null)
                {
                    directoryScanner.FileEvent -= App.OnScannerEvent;
                    directoryScanner.Dispose();
                }
            }
            App.Scanners.Clear();
        }
    }
}