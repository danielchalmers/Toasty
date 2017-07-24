using System.Windows;
using Toasty.Helpers;

namespace Toasty.Views
{
    /// <summary>
    ///     Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
            SettingsHelper.SaveSettings();
        }

        private void btnOK_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsHelper.SaveSettings();
            DialogResult = true;
        }

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsHelper.LoadSettings();
            DialogResult = true;
        }
    }
}