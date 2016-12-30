using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Notiify.Classes;
using Notiify.Properties;

namespace Notiify.Helpers
{
    public static class SourceDataHelper
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static void SaveSourceData()
        {
            Settings.Default.SourceData = JsonConvert.SerializeObject(App.Sources, JsonSerializerSettings);
        }

        public static void LoadSourceData()
        {
            App.Sources =
                JsonConvert.DeserializeObject<ObservableCollection<Source>>(Settings.Default.SourceData,
                    JsonSerializerSettings) ??
                new ObservableCollection<Source>();
        }
    }
}