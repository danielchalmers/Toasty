using System;
using System.IO;
using Newtonsoft.Json;

namespace Notiify.Classes
{
    public struct FileInfo
    {
        [JsonConstructor]
        public FileInfo(string fullName, DateTime lastWriteTimeUtc)
        {
            FullName = fullName;
            LastWriteTimeUtc = lastWriteTimeUtc;
        }

        public FileInfo(string path)
        {
            FullName = path;
            LastWriteTimeUtc = File.GetLastWriteTimeUtc(path);
        }

        public string FullName { get; }
        public DateTime LastWriteTimeUtc { get; }
    }
}