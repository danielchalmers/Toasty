﻿using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Notiify.NotificationTypes;

namespace Notiify.NotificationViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private bool _isVisible;

        public NotificationViewModel(INotification notification)
        {
            Notification = notification;
            Close = new RelayCommand(CloseExcecute);

            _isVisible = true;
        }

        public INotification Notification { get; }
        public Action Remove { get; set; }
        public ICommand Close { get; }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { Set(ref _isVisible, value); }
        }

        public void Hide()
        {
            IsVisible = false;
            Notification.Hidden = true;
        }

        public void CloseExcecute()
        {
            Remove?.Invoke();
        }
    }
}