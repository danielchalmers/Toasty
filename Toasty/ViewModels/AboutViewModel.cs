using System.Collections.Generic;
using System.Diagnostics;
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
        }

        public static IReadOnlyList<Credit> Credits { get; } = new List<Credit>()
        {
            new Credit(
                "Material Design Icons",
                "https://design.google",
                "Google",
                Properties.Resources.Material_Design_Icons,
                true),

            new Credit(
                "Common Service Locator",
                "https://github.com/unitycontainer/commonservicelocator",
                "Microsoft",
                Properties.Resources.Common_Service_Locator),

            new Credit(
                "Extended WPF Toolkit",
                "https://github.com/xceedsoftware/wpftoolkit",
                "Xceed",
                Properties.Resources.Extended_WPF_Toolkit),

            new Credit(
                "WPF NotifyIcon",
                "http://hardcodet.net/wpf-notifyicon",
                "Philipp Sumi",
                Properties.Resources.WPF_NotifyIcon),

            new Credit(
                "MVVM Light",
                "http://galasoft.ch/mvvm",
                "Laurent Bugnion (GalaSoft)",
                Properties.Resources.MVVM_Light),

            new Credit(
                "Json.NET",
                "http://newtonsoft.com/json",
                "James Newton-King",
                Properties.Resources.Json_NET)
        }.OrderBy(x => x.Name).ToList();

        public ICommand OpenWebsite { get; }
        public string Title { get; } = $"About {AssemblyInfo.Title}";

        private void OpenWebsiteExecute(string hyperlink)
        {
            Process.Start(hyperlink);
        }
    }
}