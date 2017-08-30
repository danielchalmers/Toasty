using System.Collections.Generic;
using System.Linq;
using Toasty.Classes;
using Toasty.Interfaces;

namespace Toasty.Helpers
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
                if (scanSettings is FolderScanSettings folderScanSettings)
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
                if (scanner is DirectoryScanner directoryScanner)
                {
                    directoryScanner.FileEvent -= App.OnScannerEvent;
                    directoryScanner.Dispose();
                }
            }
            App.Scanners.Clear();
        }
    }
}