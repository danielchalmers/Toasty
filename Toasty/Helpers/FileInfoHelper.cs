using System.Collections.Generic;
using System.IO;

namespace Toasty.Helpers
{
	public static class FileInfoHelper
	{
		public static string GetName(this FileInfo fileInfo) =>
			Path.GetFileName(fileInfo.FullName);

		public static bool Exists(this FileInfo fileInfo) =>
			File.Exists(fileInfo.FullName);

		public static long GetSize(this FileInfo fileInfo) =>
			new FileInfo(fileInfo.FullName).Length;

		public static string GetTextContent(this FileInfo fileInfo) =>
			File.ReadAllText(fileInfo.FullName);

		public static IEnumerable<string> GetLinesContent(this FileInfo fileInfo) =>
			File.ReadAllLines(fileInfo.FullName);

		public static string GetExtension(this FileInfo fileInfo) =>
			Path.GetExtension(fileInfo.FullName);
	}
}