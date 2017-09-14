using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            OpenLicense = new RelayCommand<Credit>(OpenLicenseExecute);
        }

        public static IReadOnlyList<Credit> Credits { get; } = new List<Credit>()
        {
            new Credit(
                "Material Design Icons",
                "https://design.google",
                "Google"),

            new Credit(
                "Common Service Locator",
                "https://github.com/unitycontainer/commonservicelocator",
                "Microsoft"),

            new Credit(
                "Extended WPF Toolkit",
                "https://github.com/xceedsoftware/wpftoolkit",
                "Xceed"),

            new Credit(
                "WPF NotifyIcon",
                "http://hardcodet.net/wpf-notifyicon",
                "Philipp Sumi"),

            new Credit(
                "MVVM Light",
                "http://galasoft.ch/mvvm",
                "GalaSoft"),

            new Credit(
                "Json.NET",
                "http://newtonsoft.com/json",
                "James Newton-King")
        }.OrderBy(x => x.Name).ToList();

        public ICommand OpenWebsite { get; }
        public ICommand OpenLicense { get; }

        private void OpenWebsiteExecute(string hyperlink)
        {
            Process.Start(hyperlink);
        }

        private void OpenLicenseExecute(Credit credit)
        {
            var licenseFilePath = Path.Combine(AssemblyInfo.Directory, "Resources", "Licenses", $"{credit.Name}.txt");
            Process.Start(licenseFilePath);
        }
    }
}