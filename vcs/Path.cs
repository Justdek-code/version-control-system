using System.Collections.Generic;
using System;
using System.IO;


namespace vcs
{
    public class Path
    {
        private string _path;

        public Path(string path)
        {
            _path = FormatPath(path);
        }

        public Path(Path path, string append)
        {
            _path = FormatPath(path.ToString() + append);
        }

        public override string ToString()
        {
            return _path;
        }

        public string GetShortPath(CallDirectory callDirectory)
        {
            string repoPath = callDirectory.GetRepositoryFolder().ToString();
            string rootFolder = repoPath.Replace("\\.repo", "");

            string shortPath = _path.Replace(rootFolder, "");
            return shortPath;
        }  

        public bool IsEqual(Path compare)
        {
            if (this._path == compare._path)
            {
                return true;
            }

            return false;
        }

        public bool IsEmpty()
        {
            if (_path == String.Empty)
            {
                return true;
            }

            return false;
        }

        public bool IsDirectory()
        {
            FileAttributes fileAttributes = System.IO.File.GetAttributes(_path);

            if (fileAttributes.HasFlag(FileAttributes.Directory))
            {
                return true;
            }

            return false;
        }

        public Path RestorePath(List<string> directories)
        {   
            if (directories.Count == 0)
            {
                return new Path("");
            }

            string path = String.Empty;
            path += directories[0];

            for (int i = 1; i < directories.Count; i++)
            {
                path += "\\" + directories[i];
            }

            return new Path(path);
        }

        public Path GetUpperDirectory()
        {
            string newPath = String.Empty;

            PathParser pathParser = new PathParser(_path);
            List<string> directories = pathParser.GetContent();

            directories.RemoveAt(directories.Count - 1); // remove bottom directory

            if (directories.Count > 0)
            {
                newPath += directories[0];

                for (int i = 1; i < directories.Count; i++)
                {
                    newPath += "\\" + directories[i];
                }
            }

            return new Path(newPath);
        }

        public Path DeleteBottomDirectory()
        {
            PathParser pathParser = new PathParser(_path);
            List<string> directories = pathParser.GetContent();
            directories.RemoveAt(0);

            return RestorePath(directories);
        }

        private string FormatPath(string path)
        {
            return path.Replace("/", "\\");
        }
    }
}