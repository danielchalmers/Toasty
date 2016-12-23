using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace Notiify
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TaskbarIcon TrayIcon { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            TrayIcon = (TaskbarIcon) Current.FindResource("TrayIcon");
        }
    }
}