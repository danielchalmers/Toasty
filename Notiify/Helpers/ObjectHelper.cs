using Newtonsoft.Json;

namespace Notiify.Helpers
{
    public static class ObjectHelper
    {
        public static T Clone<T>(this T obj)
        {
            return
                JsonConvert.DeserializeObject<T>(
                    JsonConvert.SerializeObject(obj, SettingsHelper.JsonSerializerSettingsAllTypeHandling),
                    SettingsHelper.JsonSerializerSettingsAllTypeHandling);
        }
    }
}