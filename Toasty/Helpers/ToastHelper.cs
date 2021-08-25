using System.Diagnostics;
using System.IO;
using Toasty.Classes;
using Toasty.Interfaces;
using Toasty.Properties;
using Toasty.ViewModels;

namespace Toasty.Helpers
{
	public static class ToastHelper
	{
		public static void Add(this IToast Toast, bool visible = true)
		{
			var ToastViewModel = GetNewToastViewModel(Toast);
			if (ToastViewModel == null)
				return;

			ToastViewModel.IsVisible = visible;
			App.Toasts.Add(ToastViewModel);
			while (Settings.Default.MaxToasts > 0 && App.Toasts.Count > Settings.Default.MaxToasts)
			{
				App.Toasts.RemoveAt(0);
			}
		}

		public static void Launch(this IToast Toast)
		{
			string launchPath = null;
			if (Toast.ScannerArgs is DirectoryScannerEventArgs directoryScannerArgs)
			{
				if (File.Exists(directoryScannerArgs.FileInfo.FullName))
					launchPath = directoryScannerArgs.FileInfo.FullName;
			}

			if (!string.IsNullOrEmpty(launchPath))
			{
				Process.Start(launchPath);
			}
		}

		public static ToastViewModelBase GetNewToastViewModel(IToast Toast)
		{
			return Toast switch
			{
				TextToast => new TextToastViewModel((TextToast)Toast),
				ImageToast => new ImageToastViewModel((ImageToast)Toast),
				_ => null
			};
		}
	}
}