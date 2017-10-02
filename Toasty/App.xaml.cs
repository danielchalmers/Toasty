using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using Toasty.Classes;
using Toasty.Helpers;
using Toasty.Interfaces;
using Toasty.ViewModels;

namespace Toasty
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        public static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
        public static string Company { get; } = Assembly.GetCompany();
        public static string Copyright { get; } = Assembly.GetCopyright();
        public static string Description { get; } = Assembly.GetDescription();
        public static string Directory { get; } = Assembly.GetDirectory();
        public static string Path { get; } = Assembly.GetPath();
        public static string Title { get; } = Assembly.GetTitle();
        public static Version Version { get; } = Assembly.GetVersion();

        public static ObservableCollection<Source> Sources { get; set; }
        public static ObservableCollection<ToastViewModelBase> Toasts { get; set; }
        public static List<IScanner> Scanners { get; set; }

        public static TaskbarIcon TrayIcon { get; private set; }

        public static event EventHandler<IScannerEventArgs> ScannerEvent;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SettingsHelper.LoadSettings();
            TrayIcon = (TaskbarIcon)Current.FindResource("TrayIcon");
            ScannerHelper.ReloadScanners();
            AppHelper.LoadMainWindow();
            AppHelper.OpenManageSourcesIfEmpty();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            SettingsHelper.SaveSettings();
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Alert.Show($"An unhandled exception occurred:\n\n{e.Exception}",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void NotifyScannerEvent(object sender, IScannerEventArgs e)
        {
            // Events can be running in another thread.
            Current.Dispatcher.BeginInvoke(new Action(delegate { ScannerEvent?.Invoke(sender, e); }));
        }

        public static void OnScannerEvent(object sender, DirectoryScannerEventArgs e)
        {
            NotifyScannerEvent(sender, e);
        }
    }
}