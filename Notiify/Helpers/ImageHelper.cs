﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using FileInfo = Notiify.Classes.FileInfo;

namespace Notiify.Helpers
{
    public static class ImageHelper
    {
        private static readonly List<string> SupportedExtensions = new List<string>
        {
            ".bmp",
            ".gif",
            ".ico",
            ".jpg",
            ".jpeg",
            ".png",
            ".tiff"
        };

        private static bool IsImage(string extension)
        {
            return SupportedExtensions.Contains(extension.ToLower());
        }

        public static bool IsImage(this FileInfo fileInfo)
        {
            return IsImage(fileInfo.GetExtension());
        }

        public static BitmapImage GetBitmap(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists())
            {
                return null;
            }
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
            {
                return null;
            }
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