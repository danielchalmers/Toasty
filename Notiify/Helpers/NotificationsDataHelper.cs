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

        public static void SaveNotificationsData()
        {
            Settings.Default.NotificationsData =
                JsonConvert.SerializeObject(App.Notifications.Select(x => x.Notification), JsonSerializerSettings);
        }

        public static void LoadNotificationsData()
        {
            App.Notifications = new ObservableCollection<NotificationViewModel>();
            var notifications = JsonConvert.DeserializeObject<ObservableCollection<INotification>>(
                Settings.Default.NotificationsData,
                JsonSerializerSettings);
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