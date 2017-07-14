using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Notiify.Classes;
using Notiify.Enumerations;
using Notiify.Helpers;
using Notiify.Interfaces;
using Notiify.Properties;

namespace Notiify.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        private double _actualHeight;
        private double _actualWidth;
        private double _left;
        private double _top;

        public NotificationsViewModel()
        {
            SettingsHelper.UpgradeSettings();
            GenerateTestNotification =
                new RelayCommand(GenerateTestNotificationExecute);
            OnMouseEnter = new RelayCommand<MouseEventArgs>(OnMouseEnterExecute);
            OnMouseLeave = new RelayCommand<MouseEventArgs>(OnMouseLeaveExecute);

            App.ScannerEvent += OnScannerEvent;
        }

        public ICommand GenerateTestNotification { get; }
        public ICommand OnMouseEnter { get; }
        public ICommand OnMouseLeave { get; }

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

        private void OnScannerEvent(object sender, IScannerEventArgs e)
        {
            if (e is DirectoryScannerEventArgs)
            {
                DirectoryWatcher_OnEvent((DirectoryScannerEventArgs)e);
            }
        }

        private void DirectoryWatcher_OnEvent(DirectoryScannerEventArgs e)
        {
            AddNotificationFromEventArgs(e);
        }

        private void AddNotificationFromEventArgs(DirectoryScannerEventArgs e)
        {
            INotification notification = null;
            var isContentAcceptable = e.FileInfo.Exists() && e.FileInfo.GetSize() <= Settings.Default.MaxFileContentSize;
            if (isContentAcceptable)
            {
                if (e.FileInfo.IsImage())
                {
                    notification = new ImageNotification(e.FileInfo.GetName(), e.FileInfo.GetBitmap().ToByteArray(),
                        e.FileInfo.LastWriteTimeUtc.ToLocalTime(), e);
                }
                else
                {
                    if (Settings.Default.FileContentExtensions.Split('|').Contains(e.FileInfo.GetExtension()))
                    {
                        var lines = e.FileInfo.GetLinesContent().Take(Settings.Default.MaxFileContentLines);
                        notification = new TextNotification(e.FileInfo.GetName(),
                            string.Join(Environment.NewLine, lines),
                            e.FileInfo.LastWriteTimeUtc.ToLocalTime(), e);
                    }
                }
            }
            if (notification == null)
            {
                notification = new TextNotification(e.FileInfo.GetName(), e.ChangeTypes.ToString(),
                    e.FileInfo.LastWriteTimeUtc.ToLocalTime(), e);
            }
            notification.Add();
        }

        private void GenerateTestNotificationExecute()
        {
            new TextNotification("Test", new Random().NextDouble().ToString(), DateTime.Now, null).Add();
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

        private void OnMouseEnterExecute(MouseEventArgs e)
        {
            foreach (var notificationViewModel in App.Notifications.Where(x => x.IsVisible))
            {
                notificationViewModel.CancelDelayHide();
            }
        }

        private void OnMouseLeaveExecute(MouseEventArgs e)
        {
            foreach (var notificationViewModel in App.Notifications)
            {
                notificationViewModel.DelayHide();
            }
        }
    }
}