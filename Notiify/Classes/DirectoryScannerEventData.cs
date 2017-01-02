using System;
using System.IO;
using Newtonsoft.Json;
using Notiify.Interfaces;

namespace Notiify.Classes
{
    public struct DirectoryScannerEventData : IScannerEventData
    {
        [JsonConstructor]
        public DirectoryScannerEventData(FileInfo fileInfo, WatcherChangeTypes changeTypes)
        {
            Guid = Guid.NewGuid();
            FileInfo = fileInfo;
            ChangeTypes = changeTypes;
        }

        public Guid Guid { get; }
        public FileInfo FileInfo { get; }
        public WatcherChangeTypes ChangeTypes { get; }
    }
}