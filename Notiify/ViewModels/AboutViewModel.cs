using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Notiify.Classes;
using Notiify.Helpers;

namespace Notiify.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            ViewLicenses = new RelayCommand(ViewLicensesExecute);
        }

        public string Title { get; } = $"About {AssemblyInfo.Title}";
        public ICommand ViewLicenses { get; }

        private void ViewLicensesExecute()
        {
            Process.Start(AboutHelper.LicensesDirectory);
        }
    }
}