using System.Diagnostics;
using System.IO;
using Notiify.Classes;
using Notiify.Interfaces;
using Notiify.Properties;
using Notiify.ViewModels;

namespace Notiify.Helpers
{
    public static class NotificationHelper
    {
        public static void Add(this INotification notification, bool visible = true)
        {
            var notificationViewModel = GetNewNotificationViewModel(notification);
            if (notificationViewModel == null)
            {
                return;
            }
            notificationViewModel.IsVisible = visible;
            notificationViewModel.Remove =
                () => { NotificationViewModelHelper.HideNotification(notificationViewModel); };
            App.Notifications.Add(notificationViewModel);
            while (Settings.Default.MaxNotifications > 0 && App.Notifications.Count > Settings.Default.MaxNotifications)
            {
                App.Notifications.RemoveAt(0);
            }
        }

        public static void Launch(this INotification notification)
        {
            string launchPath = null;
            if (notification.ScannerArgs is DirectoryScannerEventArgs)
            {
                var directoryScannerArgs = (DirectoryScannerEventArgs) notification.ScannerArgs;
                if (File.Exists(directoryScannerArgs.FileInfo.FullName))
                {
                    launchPath = directoryScannerArgs.FileInfo.FullName;
                }
            }

            if (!string.IsNullOrEmpty(launchPath))
            {
                Process.Start(launchPath);
            }
        }

        public static NotificationViewModelBase GetNewNotificationViewModel(INotification notification)
        {
            if (notification is TextNotification)
            {
                return new TextNotificationViewModel((TextNotification) notification);
            }
            if (notification is ImageNotification)
            {
                return new ImageNotificationViewModel((ImageNotification) notification);
            }
            return null;
        }
    }
}