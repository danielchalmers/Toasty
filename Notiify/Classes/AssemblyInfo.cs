using System.Reflection;

namespace Notiify.Classes
{
    public static class AssemblyInfo
    {
        private static Assembly _assembly { get; } = Assembly.GetExecutingAssembly();

        public static string Title { get; } = _assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
    }
}