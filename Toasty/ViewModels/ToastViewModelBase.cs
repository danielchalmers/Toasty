using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;
using System.Windows.Threading;
using Toasty.Helpers;
using Toasty.Interfaces;
using Toasty.Properties;

namespace Toasty.ViewModels
{
	public class ToastViewModelBase : ViewModelBase
	{
		private readonly DispatcherTimer _hideTimer;
		private bool _isVisible;

		public ToastViewModelBase(IToast toast)
		{
			Toast = toast;

			Close = new RelayCommand(CloseExcecute);
			OnMouseUp = new RelayCommand<MouseButtonEventArgs>(OnMouseUpExecute);

			_hideTimer = new DispatcherTimer { Interval = Settings.Default.ToastDuration };
			_hideTimer.Tick += HideTimer_OnTick;

			IsVisible = true;
		}

		public IToast Toast { get; }
		public ICommand Close { get; }
		public ICommand OnMouseUp { get; }

		public bool IsVisible
		{
			get => _isVisible;
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
			Hide();
		}

		private void OnMouseUpExecute(MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				Hide();
				Toast.Launch();
			}
		}
	}
}