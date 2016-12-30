using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Notiify.Classes
{
    public class SourceScanSettingsBase
    {
        [PropertyOrder(0)]
        [DisplayName("Path")]
        public string Path { get; set; }
    }
}