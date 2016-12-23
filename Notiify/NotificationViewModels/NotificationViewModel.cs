using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Notiify.NotificationTypes;

namespace Notiify.NotificationViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        public NotificationViewModel(INotification notification)
        {
            Notification = notification;
            Close = new RelayCommand(CloseExcecute);
        }

        public INotification Notification { get; }
        public Action Remove { get; set; }
        public ICommand Close { get; }

        public void CloseExcecute()
        {
            Remove?.Invoke();
        }
    }
}