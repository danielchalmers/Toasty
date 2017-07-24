using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Toasty.Classes;
using Toasty.Enumerations;
using Toasty.Helpers;
using Toasty.Interfaces;
using Toasty.Properties;

namespace Toasty.ViewModels
{
    public class ToastsViewModel : ViewModelBase
    {
        private double _actualHeight;
        private double _actualWidth;
        private double _left;
        private double _top;

        public ToastsViewModel()
        {
            SettingsHelper.UpgradeSettings();
            GenerateTestToast =
                new RelayCommand(GenerateTestToastExecute);
            OnMouseEnter = new RelayCommand<MouseEventArgs>(OnMouseEnterExecute);
            OnMouseLeave = new RelayCommand<MouseEventArgs>(OnMouseLeaveExecute);

            App.ScannerEvent += OnScannerEvent;
        }

        public ICommand GenerateTestToast { get; }
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
            AddToastFromEventArgs(e);
        }

        private void AddToastFromEventArgs(DirectoryScannerEventArgs e)
        {
            IToast Toast = null;
            var isContentAcceptable = e.FileInfo.Exists() && e.FileInfo.GetSize() <= Settings.Default.MaxFileContentSize;
            if (isContentAcceptable)
            {
                if (e.FileInfo.IsImage())
                {
                    Toast = new ImageToast(e.FileInfo.GetName(), e.FileInfo.GetBitmap().ToByteArray(),
                        e.FileInfo.LastWriteTimeUtc.ToLocalTime(), e);
                }
                else
                {
                    if (Settings.Default.FileContentExtensions.Split('|').Contains(e.FileInfo.GetExtension()))
                    {
                        var lines = e.FileInfo.GetLinesContent().Take(Settings.Default.MaxFileContentLines);
                        Toast = new TextToast(e.FileInfo.GetName(),
                            string.Join(Environment.NewLine, lines),
                            e.FileInfo.LastWriteTimeUtc.ToLocalTime(), e);
                    }
                }
            }
            if (Toast == null)
            {
                Toast = new TextToast(e.FileInfo.GetName(), e.ChangeTypes.ToString(),
                    e.FileInfo.LastWriteTimeUtc.ToLocalTime(), e);
            }
            Toast.Add();
        }

        private void GenerateTestToastExecute()
        {
            new TextToast("Test", new Random().NextDouble().ToString(), DateTime.Now, null).Add();
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
            foreach (var ToastViewModel in App.Toasts.Where(x => x.IsVisible))
            {
                ToastViewModel.CancelDelayHide();
            }
        }

        private void OnMouseLeaveExecute(MouseEventArgs e)
        {
            foreach (var ToastViewModel in App.Toasts)
            {
                ToastViewModel.DelayHide();
            }
        }
    }
}