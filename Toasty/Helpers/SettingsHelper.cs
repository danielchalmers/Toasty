using Toasty.Properties;

namespace Toasty.Helpers
{
	public static class SettingsHelper
	{
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
			ToastsDataHelper.SaveToastsData();

			Settings.Default.Save();
		}

		public static void LoadSettings()
		{
			Settings.Default.Reload();
			UpgradeSettings();

			SourceDataHelper.LoadSourceData();
			ToastsDataHelper.LoadToastsData();
		}
	}
}