using System.Windows;
using Notiify.Classes;
using Notiify.Properties;
using Notiify.Views;

namespace Notiify.Helpers
{
    public static class SourceHelper
    {
        private static void Add(Source source)
        {
            App.Sources.Add(source);
        }

        private static Source New(SourceScanSettingsBase scanSettings)
        {
            var newSource = new Source {ScanSettings = scanSettings};
            Add(newSource);
            return newSource;
        }

        public static Source NewFolder()
        {
            return New(new FolderScanSettings());
        }

        public static Source NewClone(this Source source)
        {
            var newCloneSource = (Source) source.Clone();
            Add(newCloneSource);
            return newCloneSource;
        }

        public static void Remove(this Source source)
        {
            if (Popup.Show(
                $"Are you sure you want to remove \"{source.Name}\"?",
                MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.No)
            {
                return;
            }
            App.Sources.Remove(source);
        }

        public static Source MoveUp(this Source source)
        {
            return App.Sources.MoveUp(source);
        }

        public static Source MoveDown(this Source source)
        {
            return App.Sources.MoveDown(source);
        }

        public static void Manage()
        {
            var manageSourcesDialog = new ManageSourcesWindow();
            manageSourcesDialog.ShowDialog();
            SourceDataHelper.SaveSourceData();
            Settings.Default.Save();
            ScannerHelper.ReloadScanners();
        }
    }
}