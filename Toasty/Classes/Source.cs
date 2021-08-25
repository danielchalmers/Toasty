using System;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Toasty.Classes
{
	public class Source : ICloneable
	{
		[Browsable(false)]
		public DateTime AddDate { get; set; } = DateTime.Now;

		[PropertyOrder(0)]
		[DisplayName("Name")]
		public string Name { get; set; } = "Untitled";

		[ExpandableObject]
		[PropertyOrder(1)]
		[DisplayName("Scan Settings")]
		public SourceScanSettingsBase ScanSettings { get; set; }

		public object Clone() => MemberwiseClone();
	}
}