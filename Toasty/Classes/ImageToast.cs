using Newtonsoft.Json;
using System;
using Toasty.Interfaces;

namespace Toasty.Classes
{
	public struct ImageToast : IToast
	{
		[JsonConstructor]
		public ImageToast(string title, byte[] image, DateTime eventDateTime, IScannerEventArgs scannerArgs)
		{
			Title = title;
			Image = image;
			EventDateTime = eventDateTime;
			ScannerArgs = scannerArgs;
		}

		public byte[] Image { get; }
		public string Title { get; }
		public DateTime EventDateTime { get; }
		public IScannerEventArgs ScannerArgs { get; }
	}
}