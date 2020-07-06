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

        public Path GetShortPath(CallDirectory callDirectory)
        {
            Path unnecessaryPath = callDirectory.GetRepositoryFolder();
            unnecessaryPath = unnecessaryPath.GetUpperDirectory(2); // pass .repo and initialized directory

            string shortPath = _path.Replace(unnecessaryPath.ToString() + "\\", "");
            return new Path(shortPath);
        }  

        public Path ConcatPath(Path concatPath)
        {
            string newPath = _path + "\\" + concatPath.ToString();
            return new Path(newPath);
        }

        public Path ConcatPath(string concatPath)
        {
            string newPath = _path + "\\" + concatPath;
            return new Path(newPath);
        }

        public string GetFileName()
        {
            if (!IsDirectory())
            {
                PathParser pathParser = new PathParser(_path);
                List<string> directories = pathParser.GetContent();

                return directories[directories.Count - 1];
            }

            return String.Empty;
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

        public Path GetUpperDirectory(int n = 1)
        {
            string newPath = String.Empty;

            PathParser pathParser = new PathParser(_path);
            List<string> directories = pathParser.GetContent();

            for (int i = 1; i <= n; i++)
            {
               directories.RemoveAt(directories.Count - 1);
            }

            return RestorePath(directories);
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