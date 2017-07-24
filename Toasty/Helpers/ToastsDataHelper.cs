using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Toasty.Interfaces;
using Toasty.Properties;
using Toasty.ViewModels;

namespace Toasty.Helpers
{
    public static class ToastsDataHelper
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        private static string GetSerializedToasts(IEnumerable<IToast> Toasts)
        {
            return JsonConvert.SerializeObject(Toasts, JsonSerializerSettings);
        }

        private static IEnumerable<IToast> GetDeserializedToasts(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<IToast>>(json, JsonSerializerSettings);
        }

        public static void SaveToastsData()
        {
            Settings.Default.ToastsData = GetSerializedToasts(App.Toasts.Select(x => x.Toast));
        }

        public static void LoadToastsData()
        {
            App.Toasts = new ObservableCollection<ToastViewModelBase>();
            var Toasts = GetDeserializedToasts(Settings.Default.ToastsData);
            if (Toasts == null)
            {
                return;
            }
            foreach (var Toast in Toasts)
            {
                Toast.Add(false);
            }
        }
    }
}