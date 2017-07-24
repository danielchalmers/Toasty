using System.Windows;
using Toasty.Helpers;
using Toasty.Views;

namespace Toasty.Resources
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

        private void MenuItemManageSources_OnClick(object sender, RoutedEventArgs e)
        {
            SourceHelper.Manage();
        }

        private void MenuItemShowToasts_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var ToastViewModel in App.Toasts)
            {
                ToastViewModel.CancelDelayHide();
                ToastViewModel.Show();
            }
        }

        private void MenuItemHideToasts_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var ToastViewModel in App.Toasts)
            {
                ToastViewModel.Hide();
            }
        }

        private void MenuItemAbout_OnClick(object sender, RoutedEventArgs e)
        {
            var aboutDialog = new About();
            aboutDialog.ShowDialog();
        }
    }
}