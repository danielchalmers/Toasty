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

namespace Notiify.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Notifications = new ObservableCollection<NotificationViewModel>();
            Notifications.CollectionChanged += Notifications_OnCollectionChanged;
            GenerateNotification =
                new RelayCommand(
                    () =>
                    {
                        AddNotification(new TextNotification
                        {
                            Title = "test",
                            Content = new Random().NextDouble().ToString()
                        });
                    });
        }

        public ObservableCollection<NotificationViewModel> Notifications { get; }
        public ICommand GenerateNotification { get; }

        private async void Notifications_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null || e.NewItems.Count == 0)
            {
                return;
            }
            await Task.Delay(Settings.Default.NotificationDuration);
            foreach (var newItem in e.NewItems)
            {
                RemoveNotification((NotificationViewModel) newItem);
            }
        }

        public void AddNotification(INotification notification)
        {
            var notificationViewModel = new NotificationViewModel(notification);
            notificationViewModel.Remove = () => { RemoveNotification(notificationViewModel); };
            Notifications.Add(notificationViewModel);
        }

        public void RemoveNotification(NotificationViewModel notificationViewModel)
        {
            if (!Notifications.Contains(notificationViewModel))
            {
                return;
            }
            Notifications.Remove(notificationViewModel);
        }
    }
}