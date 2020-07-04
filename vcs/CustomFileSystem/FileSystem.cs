using System;
using System.IO;
using System.Collections.Generic;

namespace vcs.CustomFileSystem
{
    public class FileSystem
    {
        public Directory RootDirectory;
        private CallDirectory _callDirectory;

        public FileSystem(List<Path> paths, CallDirectory callDirectory)
        {
            _callDirectory = callDirectory;
            FileSystemParser parser = new FileSystemParser(paths);
            RootDirectory = parser.GetContent();
        }

        public FileSystem(List<Blob> blobs, CallDirectory callDirectory)
        {
            List<Path> paths = new List<Path>();

            foreach (Blob blob in blobs)
            {
                paths.Add(blob.FilePath);
            }
            FileSystemParser parser = new FileSystemParser(paths);
            RootDirectory = parser.GetContent();
        }
    }
}