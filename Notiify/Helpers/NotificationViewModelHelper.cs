using Notiify.NotificationViewModels;

namespace Notiify.Helpers
{
    public static class NotificationViewModelHelper
    {
        public static void HideNotification(NotificationViewModel notificationViewModel)
        {
            if (!App.Notifications.Contains(notificationViewModel))
            {
                return;
            }
            notificationViewModel.Hide();
        }
    }
}