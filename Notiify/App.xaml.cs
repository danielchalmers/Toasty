using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using Notiify.Classes;
using Notiify.Helpers;
using Notiify.ViewModels;

namespace Notiify
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

        public static ObservableCollection<Source> Sources { get; set; }
        public static ObservableCollection<NotificationViewModelBase> Notifications { get; set; }

        public static TaskbarIcon TrayIcon { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SettingsHelper.LoadSettings();
            TrayIcon = (TaskbarIcon) Current.FindResource("TrayIcon");
            AppHelper.LoadMainWindow();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            SettingsHelper.SaveSettings();
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Popup.Show($"An unhandled exception occurred:\n\n{e.Exception}",
                MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}