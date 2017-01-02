using System;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Notiify.Interfaces;
using Notiify.Properties;

namespace Notiify.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _hideTimer;
        private bool _isVisible;

        public NotificationViewModel(INotification notification)
        {
            Notification = notification;
            Close = new RelayCommand(CloseExcecute);
            _hideTimer = new DispatcherTimer {Interval = Settings.Default.NotificationDuration};
            _hideTimer.Tick += HideTimer_OnTick;

            IsVisible = true;
        }

        public INotification Notification { get; }
        public Action Remove { get; set; }
        public ICommand Close { get; }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                Set(ref _isVisible, value);
                if (value)
                {
                    DelayHide();
                }
                else
                {
                    CancelDelayHide();
                }
            }
        }

        private void HideTimer_OnTick(object sender, EventArgs eventArgs)
        {
            Hide();
            _hideTimer.Stop();
        }

        public void Hide()
        {
            IsVisible = false;
        }

        public void DelayHide()
        {
            _hideTimer.Start();
        }

        public void CancelDelayHide()
        {
            _hideTimer.Stop();
        }

        public void Show()
        {
            IsVisible = true;
        }

        private void CloseExcecute()
        {
            Remove?.Invoke();
        }
    }
}