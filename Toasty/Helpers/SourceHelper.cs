using Toasty.Classes;
using Toasty.Properties;
using Toasty.Views;

namespace Toasty.Helpers
{
	public static class SourceHelper
	{
		private static Source New(SourceScanSettingsBase scanSettings)
		{
			var newSource = new Source { ScanSettings = scanSettings };
			Add(newSource);
			return newSource;
		}

		public static Source NewFolder()
		{
			return New(new FolderScanSettings());
		}

		public static Source NewClone(this Source source)
		{
			var newCloneSource = (Source)source.Clone();
			Add(newCloneSource);
			return newCloneSource;
		}

		private static void Add(Source source) => App.Sources.Add(source);

		public static void Remove(this Source source) => App.Sources.Remove(source);

		public static Source MoveUp(this Source source) => App.Sources.MoveUp(source);

		public static Source MoveDown(this Source source) => App.Sources.MoveDown(source);

		public static int GetIndex(this Source source) => App.Sources.IndexOf(source);

		public static void Manage()
		{
			var manageSourcesDialog = new ManageSourcesWindow();
			manageSourcesDialog.ShowDialog();
			SourceDataHelper.SaveSourceData();
			Settings.Default.Save();
			ScannerHelper.ReloadScanners();
		}
	}
}