using System.IO;
using System;
using System.Collections.Generic;


namespace vcs
{

    public class CallDirectory
    {
        private Path _path;

        public CallDirectory()
        {
            _path = new Path(Directory.GetCurrentDirectory());
            
            Console.WriteLine(_path.ToString());
        }

        public Path GetPath()
        {
            return _path;
        }

        public bool IsUnderVersionControl()
        {
            Path repositryPath = GetRepositoryFolder();

            if (repositryPath.ToString() == String.Empty)
            {
                return false;
            }

            return true;
        }

        public Path GetRepositoryFolder()
        {
            Path tempPath = new Path(_path.ToString());

            while (!tempPath.IsEmpty())
            {
                string searchingDirectory = tempPath.ToString() + "\\.repo";

                if (Directory.Exists(searchingDirectory))
                {
                    return new Path(searchingDirectory);
                }

                tempPath = tempPath.GetUpperDirectory();
            }

            return new Path(String.Empty);
        }

        public Path GetCommonDirectory()
        {
            Path commonDirectory = GetRepositoryFolder().GetUpperDirectory(1);
            
            return commonDirectory;
        }
    }
}