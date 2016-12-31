using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Notiify.NotificationViewModels;
using Notiify.Properties;

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
            Settings.Default.NotificationsData = JsonConvert.SerializeObject(App.Notifications, JsonSerializerSettings);
        }

        public static void LoadNotificationsData()
        {
            App.Notifications =
                JsonConvert.DeserializeObject<ObservableCollection<NotificationViewModel>>(
                    Settings.Default.NotificationsData,
                    JsonSerializerSettings) ??
                new ObservableCollection<NotificationViewModel>();
        }
    }
}