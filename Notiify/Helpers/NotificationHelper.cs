using Notiify.NotificationTypes;
using Notiify.NotificationViewModels;
using Notiify.Properties;

namespace Notiify.Helpers
{
    public static class NotificationHelper
    {
        public static void Add(this INotification notification)
        {
            var notificationViewModel = new NotificationViewModel(notification);
            notificationViewModel.Remove =
                () => { NotificationViewModelHelper.HideNotification(notificationViewModel); };
            App.Notifications.Add(notificationViewModel);
            while (Settings.Default.MaxNotifications > 0 && App.Notifications.Count > Settings.Default.MaxNotifications)
            {
                App.Notifications.RemoveAt(0);
            }
        }
    }
}