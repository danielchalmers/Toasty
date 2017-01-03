using System.Windows;

namespace Notiify.Views
{
    /// <summary>
    ///     Interaction logic for ManageSources.xaml
    /// </summary>
    public partial class ManageSources : Window
    {
        public ManageSources()
        {
            InitializeComponent();
        }

        private void btnOK_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}