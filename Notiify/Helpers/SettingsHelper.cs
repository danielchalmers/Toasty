using Newtonsoft.Json;
using Notiify.Properties;

namespace Notiify.Helpers
{
    public static class SettingsHelper
    {
        public static readonly JsonSerializerSettings JsonSerializerSettingsAllTypeHandling = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static void UpgradeSettings()
        {
            if (Settings.Default.MustUpgrade)
            {
                Settings.Default.Upgrade();
                Settings.Default.MustUpgrade = false;
                Settings.Default.Save();
            }
        }

        public static void SaveSettings()
        {
            SourceDataHelper.SaveSourceData();
            Settings.Default.Save();
        }

        public static void LoadSettings()
        {
            Settings.Default.Reload();
            UpgradeSettings();
            SourceDataHelper.LoadSourceData();
        }
    }
}