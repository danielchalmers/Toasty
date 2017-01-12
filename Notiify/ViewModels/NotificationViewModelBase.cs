using System;
using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Notiify.Helpers;
using Notiify.Interfaces;
using Notiify.Properties;

namespace Notiify.ViewModels
{
    public class NotificationViewModelBase : ViewModelBase
    {
        private readonly DispatcherTimer _hideTimer;
        private bool _isVisible;

        public NotificationViewModelBase(INotification notification)
        {
            Notification = notification;
            Close = new RelayCommand(CloseExcecute);
            OnMouseUp = new RelayCommand<MouseButtonEventArgs>(OnMouseUpExecute);
            _hideTimer = new DispatcherTimer {Interval = Settings.Default.NotificationDuration};
            _hideTimer.Tick += HideTimer_OnTick;

            IsVisible = true;
        }

        public INotification Notification { get; }
        public Action Remove { get; set; }
        public ICommand Close { get; }
        public ICommand OnMouseUp { get; }

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

        private void Dismiss()
        {
            Remove?.Invoke();
        }

        private void CloseExcecute()
        {
            Dismiss();
        }

        private void OnMouseUpExecute(MouseButtonEventArgs e)
        {
            Dismiss();
            Notification.Launch();
        }
    }
}