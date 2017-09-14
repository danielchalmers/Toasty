using System;
using System.IO;
using System.Reflection;

namespace Toasty.Classes
{
    public static class AssemblyInfo
    {
        private static Assembly _assembly { get; } = Assembly.GetExecutingAssembly();

        public static string Title { get; } = _assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
        public static string Copyright { get; } = _assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
        public static string Location { get; } = _assembly.Location;
        public static string Directory { get; } = Path.GetDirectoryName(_assembly.Location);
        public static Version Version { get; } = _assembly.GetName().Version;
    }
}