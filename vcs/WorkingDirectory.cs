using System.Linq;
using System.Collections.Generic;
using System.IO;


namespace vcs
{
    public class WorkingDirectory
    {
        private CallDirectory _callDirectory;
        private List<Path> _files;

        public WorkingDirectory(CallDirectory callDirectory)
        {
            _callDirectory = callDirectory;
            _files = ExtractFiles(_callDirectory.GetPath());
        }

        public List<Path> GetContent()
        {
            return _files;
        }

        private List<Path> ExtractFiles(Path path)
        {
            List<Path> filesPaths = new List<Path>();

            string currentDirectory = path.GetContent();
            string[] files = Directory.GetFiles(currentDirectory);
 
            foreach (string filePath in files)
            {
                filesPaths.Add(new Path(filePath));
            }

            string[] directories = Directory.GetDirectories(currentDirectory);

            Path repositoryPath = _callDirectory.FindRepositoryRoot();
            foreach (string directory in directories)
            {
                if (directory == repositoryPath.GetContent())
                { 
                    continue; // skip .repo directory
                }
                
                Path subdirectory = new Path(directory);
                filesPaths.AddRange(ExtractFiles(subdirectory));
            }
            
            return filesPaths;
        }
    }
}