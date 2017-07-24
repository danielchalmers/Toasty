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
            {
                return;
            }
            ToastViewModel.IsVisible = visible;
            ToastViewModel.Remove =
                () => { ToastViewModelHelper.HideToast(ToastViewModel); };
            App.Toasts.Add(ToastViewModel);
            while (Settings.Default.MaxToasts > 0 && App.Toasts.Count > Settings.Default.MaxToasts)
            {
                App.Toasts.RemoveAt(0);
            }
        }

        public static void Launch(this IToast Toast)
        {
            string launchPath = null;
            if (Toast.ScannerArgs is DirectoryScannerEventArgs)
            {
                var directoryScannerArgs = (DirectoryScannerEventArgs)Toast.ScannerArgs;
                if (File.Exists(directoryScannerArgs.FileInfo.FullName))
                {
                    launchPath = directoryScannerArgs.FileInfo.FullName;
                }
            }

            if (!string.IsNullOrEmpty(launchPath))
            {
                Process.Start(launchPath);
            }
        }

        public static ToastViewModelBase GetNewToastViewModel(IToast Toast)
        {
            if (Toast is TextToast)
            {
                return new TextToastViewModel((TextToast)Toast);
            }
            if (Toast is ImageToast)
            {
                return new ImageToastViewModel((ImageToast)Toast);
            }
            return null;
        }
    }
}