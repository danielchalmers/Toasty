using System.Windows;

namespace Notiify
{
    partial class TrayIconResources : ResourceDictionary
    {
        public TrayIconResources()
        {
            InitializeComponent();
        }

        private void MenuItemExit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}