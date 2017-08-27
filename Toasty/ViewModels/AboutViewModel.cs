using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Toasty.Classes;

namespace Toasty.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            OpenWebsite = new RelayCommand<string>(OpenWebsiteExecute);
        }

        public string Title { get; } = $"About {AssemblyInfo.Title}";
        public ICommand OpenWebsite { get; }

        private void OpenWebsiteExecute(string hyperlink)
        {
            Process.Start(hyperlink);
        }
    }
}