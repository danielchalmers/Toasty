using Newtonsoft.Json;

namespace Toasty.Helpers
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