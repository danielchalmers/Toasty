using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Toasty.Classes
{
    public class FolderScanSettings : SourceScanSettingsBase
    {
        [PropertyOrder(10)]
        [DisplayName("Recursive")]
        public bool Recursive { get; set; } = true;

        [PropertyOrder(11)]
        [DisplayName("Include Filter")]
        public string IncludeFilter { get; set; } = "*.*";

        [PropertyOrder(12)]
        [DisplayName("Exclude Filter")]
        public string ExcludeFilter { get; set; } = string.Empty;

        public override string ToString()
        {
            return "Folder Scan Settings";
        }
    }
}