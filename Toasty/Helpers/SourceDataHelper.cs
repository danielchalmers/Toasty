using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Toasty.Classes;
using Toasty.Properties;

namespace Toasty.Helpers
{
	public static class SourceDataHelper
	{
		private static readonly JsonSerializerSettings JsonSerializerSettings = new()
		{
			TypeNameHandling = TypeNameHandling.Auto,
			ObjectCreationHandling = ObjectCreationHandling.Replace,
			MissingMemberHandling = MissingMemberHandling.Ignore
		};

		private static string GetSerializedSources(ObservableCollection<Source> sources) =>
			JsonConvert.SerializeObject(sources, JsonSerializerSettings);

		private static ObservableCollection<Source> GetDeserializedSources(string json) =>
			JsonConvert.DeserializeObject<ObservableCollection<Source>>(json, JsonSerializerSettings);

		public static void SaveSourceData()
		{
			Settings.Default.SourceData = GetSerializedSources(App.Sources);
		}

		public static void LoadSourceData()
		{
			App.Sources = GetDeserializedSources(Settings.Default.SourceData) ?? new ObservableCollection<Source>();
		}
	}
}