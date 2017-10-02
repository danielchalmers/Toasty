using System;
using System.Reflection;

namespace Toasty.Helpers
{
    public static class AssemblyHelper
    {
        public static string GetCompany(this Assembly assembly) => assembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company;

        public static string GetCopyright(this Assembly assembly) => assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;

        public static string GetDescription(this Assembly assembly) => assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

        public static string GetDirectory(this Assembly assembly) => System.IO.Path.GetDirectoryName(assembly.GetPath());

        public static string GetPath(this Assembly assembly) => assembly.Location;

        public static string GetTitle(this Assembly assembly) => assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;

        public static Version GetVersion(this Assembly assembly) => assembly.GetName().Version;
    }
}