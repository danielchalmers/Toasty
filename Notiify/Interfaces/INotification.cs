﻿using System;

namespace Notiify.Interfaces
{
    public interface INotification
    {
        string Title { get; }
        DateTime EventDateTime { get; }
        IScannerEventData OriginData { get; }
    }
}