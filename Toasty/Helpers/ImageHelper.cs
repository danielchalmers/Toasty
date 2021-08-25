using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Toasty.Helpers
{
	public static class ImageHelper
	{
		// https://msdn.microsoft.com/en-us/library/ee719654(v=VS.85).aspx#wpfc_codecs.
		private static readonly IReadOnlyList<string> SupportedExtensions = new[]
		{
			".bmp",
			".gif",
			".ico",
			".jpg",
			".jpeg",
			".png",
			".tiff",
			".wmp",
			".dds"
		};

		private static bool IsImage(string extension) =>
			SupportedExtensions.Contains(extension.ToLower());

		public static bool IsImage(this FileInfo fileInfo) =>
			IsImage(fileInfo.GetExtension());

		public static BitmapImage GetBitmap(this FileInfo fileInfo)
		{
			if (!fileInfo.Exists())
				return null;

			var bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
			bitmapImage.UriSource = new Uri(fileInfo.FullName, UriKind.Absolute);
			bitmapImage.EndInit();
			bitmapImage.Freeze();
			return bitmapImage;
		}

		public static byte[] ToByteArray(this BitmapImage image)
		{
			if (image == null)
				return null;

			var encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(image));

			using (var ms = new MemoryStream())
			{
				encoder.Save(ms);
				return ms.ToArray();
			}
		}
	}
}