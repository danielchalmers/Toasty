using System.IO;
using Newtonsoft.Json;
using Notiify.Interfaces;

namespace Notiify.Classes
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