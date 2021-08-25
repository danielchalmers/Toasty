using System;

namespace Toasty.Interfaces
{
	public interface IToast
	{
		string Title { get; }
		DateTime EventDateTime { get; }
		IScannerEventArgs ScannerArgs { get; }
	}
}