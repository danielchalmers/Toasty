namespace Notiify.NotificationTypes
{
    public class TextNotification : INotification
    {
        public string Content { get; set; }
        public string Title { get; set; }
    }
}