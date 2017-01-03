using System.Windows;

namespace Notiify.Views
{
    /// <summary>
    ///     Interaction logic for ManageSourcesWindow.xaml
    /// </summary>
    public partial class ManageSourcesWindow : Window
    {
        public ManageSourcesWindow()
        {
            InitializeComponent();
        }

        private void btnOK_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}