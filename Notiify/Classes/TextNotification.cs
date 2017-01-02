﻿using System;
using Newtonsoft.Json;
using Notiify.Interfaces;

namespace Notiify.Classes
{
    public struct TextNotification : INotification
    {
        [JsonConstructor]
        public TextNotification(string title, string content, DateTime eventDateTime)
        {
            Title = title;
            Content = content;
            EventDateTime = eventDateTime;
        }

        public string Content { get; }
        public string Title { get; }
        public DateTime EventDateTime { get; }
    }
}