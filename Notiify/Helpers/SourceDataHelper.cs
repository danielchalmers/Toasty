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

        private static string GetSerializedSources(ObservableCollection<Source> sources)
        {
            return JsonConvert.SerializeObject(sources, JsonSerializerSettings);
        }

        private static ObservableCollection<Source> GetDeserializedSources(string json)
        {
            return JsonConvert.DeserializeObject<ObservableCollection<Source>>(json, JsonSerializerSettings);
        }

        public static void SaveSourceData()
        {
            Settings.Default.SourceData = GetSerializedSources(App.Sources);
        }

        public static void LoadSourceData()
        {
            App.Sources = GetDeserializedSources(Settings.Default.SourceData) ??
                new ObservableCollection<Source>();
        }
    }
}