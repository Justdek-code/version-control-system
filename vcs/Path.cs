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
            _path = path;
        }

        public string GetContent()
        {
            return _path;
        }

        public bool IsEmpty()
        {
            if (_path == String.Empty)
            {
                return true;
            }

            return false;
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
    }
}