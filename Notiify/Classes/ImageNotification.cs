using System;
using Newtonsoft.Json;
using Notiify.Interfaces;

namespace Notiify.Classes
{
    public struct ImageNotification : INotification
    {
        [JsonConstructor]
        public ImageNotification(string title, byte[] image, DateTime eventDateTime, IScannerEventArgs scannerArgs)
        {
            Title = title;
            Image = image;
            EventDateTime = eventDateTime;
            ScannerArgs = scannerArgs;
        }

        public byte[] Image { get; }
        public string Title { get; }
        public DateTime EventDateTime { get; }
        public IScannerEventArgs ScannerArgs { get; }
    }
}