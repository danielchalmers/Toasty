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
    }
}