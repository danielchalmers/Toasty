using System.Windows;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using Notiify.Classes;

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

        public static TaskbarIcon TrayIcon { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            TrayIcon = (TaskbarIcon) Current.FindResource("TrayIcon");
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Popup.Show($"An unhandled exception occurred:\n\n{e.Exception}",
                MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}