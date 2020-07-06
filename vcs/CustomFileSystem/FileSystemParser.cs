using System;
using System.Collections.Generic;
using System.IO;

namespace vcs.CustomFileSystem
{
    public class FileSystemParser
    {
        private List<Path> _paths;
        private Directory _directoryTree;

        public FileSystemParser(List<Path> paths)
        {
            _paths = paths;
            
            if (_paths.Count > 0)
            {
                _directoryTree = ParseDirectories();
            }
        }

        public Directory GetContent()
        {
            return _directoryTree;
        }

        private void ParseSubdirectory(Path subdirectoryPath, Directory parentDirectory)
        {
            subdirectoryPath = subdirectoryPath.DeleteBottomDirectory();

            PathParser pathParser = new PathParser(subdirectoryPath);
            List<string> parsedPath = pathParser.GetContent();

            if (parsedPath.Count == 1)
            {
                parentDirectory.Files.Add(new File(parsedPath[0]));
            }
            else
            {
                string directoryName = parsedPath[0];

                Directory subdirectory;
                if (parentDirectory.ContainsSubdirectory(directoryName))
                {
                    subdirectory = parentDirectory.GetSubdirectory(directoryName);
                }
                else
                {
                    subdirectory = new Directory(directoryName);
                    parentDirectory.AddSubdirectory(subdirectory);
                }

                ParseSubdirectory(subdirectoryPath, subdirectory);
            }
        }

        private Directory ParseDirectories()
        {
            Directory rootDirectory = GetRootDirectory(_paths[0]);

            foreach (Path path in _paths)
            {
                ParseSubdirectory(path, rootDirectory);
            }

            return rootDirectory;
        }

        private Directory GetRootDirectory(Path path)
        {
            //root directory is a general directory that every path must contain
            PathParser pathParser = new PathParser(path.ToString());
            string directoryName = pathParser.GetContent()[0];

            return new Directory(directoryName);
        }
    }
}