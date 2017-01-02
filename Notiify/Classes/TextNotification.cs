using System;
using Newtonsoft.Json;
using Notiify.Interfaces;

namespace Notiify.Classes
{
    public struct TextNotification : INotification
    {
        [JsonConstructor]
        public TextNotification(string title, string content, DateTime eventDateTime, IScannerEventData originData)
        {
            Title = title;
            Content = content;
            EventDateTime = eventDateTime;
            OriginData = originData;
        }

        public string Content { get; }
        public string Title { get; }
        public DateTime EventDateTime { get; }
        public IScannerEventData OriginData { get; }
    }
}