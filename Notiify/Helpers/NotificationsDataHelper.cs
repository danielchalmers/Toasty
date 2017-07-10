using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Notiify.Interfaces;
using Notiify.Properties;
using Notiify.ViewModels;

namespace Notiify.Helpers
{
    public static class NotificationsDataHelper
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        private static string GetSerializedNotifications(IEnumerable<INotification> notifications)
        {
            return JsonConvert.SerializeObject(notifications, JsonSerializerSettings);
        }

        private static IEnumerable<INotification> GetDeserializedNotifications(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<INotification>>(json, JsonSerializerSettings);
        }

        public static void SaveNotificationsData()
        {
            Settings.Default.NotificationsData = GetSerializedNotifications(App.Notifications.Select(x => x.Notification));
        }

        public static void LoadNotificationsData()
        {
            App.Notifications = new ObservableCollection<NotificationViewModelBase>();
            var notifications = GetDeserializedNotifications(Settings.Default.NotificationsData);
            if (notifications == null)
            {
                return;
            }
            foreach (var notification in notifications)
            {
                notification.Add(false);
            }
        }
    }
}