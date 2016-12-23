using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Notiify.NotificationTypes;
using Notiify.NotificationViewModels;
using Notiify.Properties;

namespace Notiify.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Notifications = new ObservableCollection<NotificationViewModel>();
            Notifications.CollectionChanged += Notifications_OnCollectionChanged;
            GenerateTestNotification =
                new RelayCommand(GenerateTestNotificationExecute);
        }

        public ObservableCollection<NotificationViewModel> Notifications { get; }
        public ICommand GenerateTestNotification { get; }

        private async void Notifications_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null || e.NewItems.Count == 0)
            {
                return;
            }
            await Task.Delay(Settings.Default.NotificationDuration);
            foreach (var newItem in e.NewItems)
            {
                CloseNotification((NotificationViewModel) newItem);
            }
        }

        private void AddNotification(INotification notification)
        {
            var notificationViewModel = new NotificationViewModel(notification);
            notificationViewModel.Remove = () => { RemoveNotification(notificationViewModel); };
            Notifications.Add(notificationViewModel);
        }

        private void RemoveNotification(NotificationViewModel notificationViewModel)
        {
            if (!Notifications.Contains(notificationViewModel))
            {
                return;
            }
            Notifications.Remove(notificationViewModel);
        }

        private void CloseNotification(NotificationViewModel notificationViewModel)
        {
            if (!Notifications.Contains(notificationViewModel))
            {
                return;
            }
            notificationViewModel.Close?.Execute(null);
            RemoveNotification(notificationViewModel);
        }

        private void GenerateTestNotificationExecute()
        {
            AddNotification(new TextNotification
            {
                Title = "Test",
                Content = new Random().NextDouble().ToString()
            });
        }
    }
}