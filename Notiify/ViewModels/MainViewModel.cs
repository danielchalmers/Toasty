using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Notiify.Classes;
using Notiify.Enumerations;
using Notiify.NotificationTypes;
using Notiify.NotificationViewModels;
using Notiify.Properties;

namespace Notiify.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DirectoryWatcher _directoryWatcher;
        private double _actualHeight;

        private double _actualWidth;

        private double _left;
        private double _top;

        public MainViewModel()
        {
            Notifications = new ObservableCollection<NotificationViewModel>();
            Notifications.CollectionChanged += Notifications_OnCollectionChanged;
            GenerateTestNotification =
                new RelayCommand(GenerateTestNotificationExecute);

            _directoryWatcher = new DirectoryWatcher(new DirectoryWatcherSettings {Path = "Test"}, (path, type) =>
            {
                // Events can be running in another thread.
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() => DirectoryWatcher_OnEvent(path, type)));
            });
            _directoryWatcher.Start();
        }

        public ObservableCollection<NotificationViewModel> Notifications { get; }
        public ICommand GenerateTestNotification { get; }

        public double ActualWidth
        {
            get { return _actualWidth; }
            set
            {
                if (Set(ref _actualWidth, value))
                {
                    UpdateLocation();
                }
            }
        }

        public double ActualHeight
        {
            get { return _actualHeight; }
            set
            {
                if (Set(ref _actualHeight, value))
                {
                    UpdateLocation();
                }
            }
        }

        public double Left
        {
            get { return _left; }
            set { Set(ref _left, value); }
        }

        public double Top
        {
            get { return _top; }
            set { Set(ref _top, value); }
        }

        private void DirectoryWatcher_OnEvent(string path, WatcherChangeTypes watcherChangeTypes)
        {
            AddNotification(new TextNotification
            {
                Title = Path.GetFileNameWithoutExtension(path),
                Content = watcherChangeTypes.ToString()
            });
        }

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

        private double GetLeft()
        {
            switch (Settings.Default.DockPosition)
            {
                case DockPosition.Right:
                    return SystemParameters.WorkArea.Right - ActualWidth;
                default:
                    return 0;
            }
        }

        private double GetTop()
        {
            return SystemParameters.WorkArea.Bottom - ActualHeight;
        }

        private void UpdateLocation()
        {
            Left = GetLeft();
            Top = GetTop();
        }
    }
}