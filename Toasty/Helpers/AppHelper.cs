using System.Linq;
using System.Windows;
using Toasty.Views;

namespace Toasty.Helpers
{
	public static class AppHelper
	{
		public static void LoadMainWindow()
		{
			var mainWindow = new ToastsWindow();
			Application.Current.MainWindow = mainWindow;
			mainWindow.Show();
		}

		public static void OpenManageSourcesIfEmpty()
		{
			if (App.Sources.Any())
				return;

			SourceHelper.Manage();
		}
	}
}