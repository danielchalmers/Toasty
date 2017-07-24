using System.Windows;

namespace Toasty.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void btnOK_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}