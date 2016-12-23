using GalaSoft.MvvmLight;
using Notiify.NotificationTypes;

namespace Notiify.NotificationViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        public NotificationViewModel(INotification notification)
        {
            Notification = notification;
        }

        public INotification Notification { get; }
    }
}