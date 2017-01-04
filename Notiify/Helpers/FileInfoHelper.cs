using System.IO;
using FileInfo = Notiify.Classes.FileInfo;

namespace Notiify.Helpers
{
    public static class FileInfoHelper
    {
        public static string GetName(this FileInfo fileInfo)
        {
            return Path.GetFileName(fileInfo.FullName);
        }
    }
}