using System;
using System.Collections.Generic;

namespace RestFS.Console.Storage
{
    public class FileAttributes
    {
        public FileAttributes(string name, long size, DateTime lastWriteTime, bool isDir)
        {
            LastWriteTime = lastWriteTime;
            Size          = size;
            Name          = name;
            IsDir         = isDir;
        }

        public DateTime LastWriteTime { get; }
        public long     Size          { get; }
        public string   Name          { get; }
        public bool     IsDir         { get; }

        public IDictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                {"LastWriteTime", LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss")},
                {"Size", Size.ToString()},
                {"Name", Name},
                {"IsDir", IsDir.ToString()}
            };
        }
    }
}