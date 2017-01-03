using System.Windows;
using Notiify.Views;

namespace Notiify.Resources
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
            var manageSourcesDialog = new ManageSourcesWindow();
            manageSourcesDialog.ShowDialog();
        }

        private void MenuItemShowNotifications_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var notificationViewModel in App.Notifications)
            {
                notificationViewModel.CancelDelayHide();
                notificationViewModel.Show();
            }
        }

        private void MenuItemHideNotifications_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var notificationViewModel in App.Notifications)
            {
                notificationViewModel.Hide();
            }
        }
    }
}