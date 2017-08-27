using System.Collections.Generic;
using System.Linq;
using Toasty.Classes;

namespace Toasty.Helpers
{
    public static class AboutHelper
    {
        public static IReadOnlyList<Credit> Credits { get; } = new List<Credit>()
        {
            new Credit(
                "Material Design Icons",
                "https://design.google",
                "Google",
                Properties.Resources.Material_Design_Icons_License,
                true),

            new Credit(
                "Common Service Locator",
                "https://github.com/unitycontainer/commonservicelocator",
                "Microsoft",
                Properties.Resources.Common_Service_Locator_License),

            new Credit(
                "Extended WPF Toolkit",
                "https://github.com/xceedsoftware/wpftoolkit",
                "Xceed",
                Properties.Resources.Extended_WPF_Toolkit_License),

            new Credit(
                "WPF NotifyIcon",
                "http://hardcodet.net/wpf-notifyicon",
                "Philipp Sumi",
                Properties.Resources.WPF_NotifyIcon_License),

            new Credit(
                "MVVM Light",
                "http://galasoft.ch/mvvm",
                "Laurent Bugnion (GalaSoft)",
                Properties.Resources.MVVM_Light_License),

            new Credit(
                "Json.NET",
                "http://newtonsoft.com/json",
                "James Newton-King",
                Properties.Resources.Json_NET_License)
        }.OrderBy(x => x.Name).ToList();
    }
}