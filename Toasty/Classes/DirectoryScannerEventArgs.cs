using Newtonsoft.Json;
using System.IO;
using Toasty.Interfaces;

namespace Toasty.Classes
{
	public struct DirectoryScannerEventArgs : IScannerEventArgs
	{
		[JsonConstructor]
		public DirectoryScannerEventArgs(FileInfo fileInfo, WatcherChangeTypes changeTypes)
		{
			FileInfo = fileInfo;
			ChangeTypes = changeTypes;
		}

		public FileInfo FileInfo { get; }
		public WatcherChangeTypes ChangeTypes { get; }
	}
}