namespace Notiify.NotificationTypes
{
    public interface INotification
    {
        string Title { get; set; }
        bool Hidden { get; set; }
    }
}