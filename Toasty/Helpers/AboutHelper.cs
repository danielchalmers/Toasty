using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Toasty.Classes;

namespace Toasty.Helpers
{
    public static class AboutHelper
    {
        public static string AboutText
        {
            get
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine($"{AssemblyInfo.Title} {AssemblyInfo.Version}");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"Icon is modified and originally by Google (design.google)");

                stringBuilder.AppendLine();
                stringBuilder.AppendLine("Libraries:");
                foreach (var library in Libraries
                    .Select(x => $"{x.Key} ({x.Value})"))
                {
                    stringBuilder.AppendLine(library);
                }
                stringBuilder.AppendLine();

                stringBuilder.Append(AssemblyInfo.Copyright);
                return stringBuilder.ToString();
            }
        }

        public static string LicensesDirectory { get; } = Path.Combine(
            Path.GetDirectoryName(AssemblyInfo.Location),
            "Resources",
            "Licenses");

        private static Dictionary<string, string> Libraries { get; } = new Dictionary<string, string>
        {
            {"Common Service Locator", "commonservicelocator.codeplex.com"},
            {"Extended WPF Toolkit", "wpftoolkit.codeplex.com"},
            {"NotifyIcon", "hardcodet.net/projects/wpf-notifyicon"},
            {"MVVM Light", "galasoft.ch/mvvm"},
            {"Json.NET", "newtonsoft.com/json"}
        };
    }
}