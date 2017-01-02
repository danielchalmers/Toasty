using System;

namespace Notiify.NotificationTypes
{
    public interface INotification
    {
        string Title { get; }
        DateTime EventDateTime { get; }
    }
}