using System.Windows;
using Notiify.Views;

namespace Notiify.Helpers
{
    public static class AppHelper
    {
        public static void LoadMainWindow()
        {
            var mainWindow = new NotificationsWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public static void OpenManageSourcesIfEmpty()
        {
            if (App.Sources.Count > 0)
            {
                return;
            }
            var manageSourcesDialog = new ManageSourcesWindow();
            manageSourcesDialog.ShowDialog();
        }
    }
}