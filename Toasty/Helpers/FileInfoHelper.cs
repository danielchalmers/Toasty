using System.Collections.Generic;
using System.IO;

namespace Toasty.Helpers
{
    public static class FileInfoHelper
    {
        public static string GetName(this FileInfo fileInfo)
        {
            return Path.GetFileName(fileInfo.FullName);
        }

        public static bool Exists(this FileInfo fileInfo)
        {
            return File.Exists(fileInfo.FullName);
        }

        public static long GetSize(this FileInfo fileInfo)
        {
            return new FileInfo(fileInfo.FullName).Length;
        }

        public static string GetTextContent(this FileInfo fileInfo)
        {
            return File.ReadAllText(fileInfo.FullName);
        }

        public static IEnumerable<string> GetLinesContent(this FileInfo fileInfo)
        {
            return File.ReadAllLines(fileInfo.FullName);
        }

        public static string GetExtension(this FileInfo fileInfo)
        {
            return Path.GetExtension(fileInfo.FullName);
        }
    }
}