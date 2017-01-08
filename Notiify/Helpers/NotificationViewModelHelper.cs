using Notiify.ViewModels;

namespace Notiify.Helpers
{
    public static class NotificationViewModelHelper
    {
        public static void HideNotification(NotificationViewModelBase notificationViewModel)
        {
            if (!App.Notifications.Contains(notificationViewModel))
            {
                return;
            }
            notificationViewModel.Hide();
        }
    }
}