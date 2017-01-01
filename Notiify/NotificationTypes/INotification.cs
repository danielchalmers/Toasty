using System;

namespace Notiify.NotificationTypes
{
    public interface INotification
    {
        string Title { get; set; }
        DateTime EventDateTime { get; set; }
    }
}