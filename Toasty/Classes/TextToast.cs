using System;
using Newtonsoft.Json;
using Toasty.Interfaces;

namespace Toasty.Classes
{
    public struct TextToast : IToast
    {
        [JsonConstructor]
        public TextToast(string title, string content, DateTime eventDateTime, IScannerEventArgs scannerArgs)
        {
            Title = title;
            Content = content;
            EventDateTime = eventDateTime;
            ScannerArgs = scannerArgs;
        }

        public string Content { get; }
        public string Title { get; }
        public DateTime EventDateTime { get; }
        public IScannerEventArgs ScannerArgs { get; }
    }
}