using System.Windows;
using Notiify.Windows;

namespace Notiify
{
    partial class TrayIconResources : ResourceDictionary
    {
        public TrayIconResources()
        {
            InitializeComponent();
        }

        private void MenuItemExit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItemOptions_OnClick(object sender, RoutedEventArgs e)
        {
            var optionsDialog = new Options();
            optionsDialog.ShowDialog();
        }
    }
}