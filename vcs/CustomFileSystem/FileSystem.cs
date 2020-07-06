using System;
using System.IO;
using System.Collections.Generic;

namespace vcs.CustomFileSystem
{
    public class FileSystem
    {
        private Directory _rootDirectory;
        private CallDirectory _callDirectory;

        public FileSystem(List<Path> paths, CallDirectory callDirectory)
        {
            _callDirectory = callDirectory;
            FileSystemParser parser = new FileSystemParser(paths);
            _rootDirectory = parser.GetContent();
        }

        public FileSystem(List<Blob> blobs, CallDirectory callDirectory)
        {
            List<Path> paths = new List<Path>();

            foreach (Blob blob in blobs)
            {
                paths.Add(blob.FilePath.GetShortPath(callDirectory));
            }
            FileSystemParser parser = new FileSystemParser(paths);
            _rootDirectory = parser.GetContent();
        }

        public Directory GetContent()
        {
            return _rootDirectory;
        }
    }
}