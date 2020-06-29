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
            
            Console.WriteLine(_path.GetContent());
        }

        public Path GetPath()
        {
            return _path;
        }

        public bool IsUnderVersionControl()
        {
            Path repositryPath = FindRepositoryRoot();

            if (repositryPath.GetContent() == String.Empty)
            {
                return false;
            }

            return true;
        }

        public Path FindRepositoryRoot()
        {
            Path tempPath = new Path(_path.GetContent());

            while (!tempPath.IsEmpty())
            {
                string searchingDirectory = tempPath.GetContent() + "\\.repo";

                if (Directory.Exists(searchingDirectory))
                {
                    return new Path(searchingDirectory);
                }

                tempPath = tempPath.GetUpperDirectory();
            }

            return new Path(String.Empty);
        }
    }
}