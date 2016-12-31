using Notiify.NotificationTypes;
using Notiify.NotificationViewModels;

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
        }
    }
}