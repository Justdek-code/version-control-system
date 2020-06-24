using System;
using System.IO;
using System.Collections.Generic;

namespace vcs
{
    public class Init
    {
        private CallDirectory _callDirectory;

        public Init(CallDirectory callDirectory)
        {
            _callDirectory = callDirectory;
        }

        public void Execute()
        {
           

            if (!_callDirectory.IsUnderVersionControl())
            { 
                Path directoryPath = _callDirectory.GetPath();
                string path = directoryPath.GetContent() + "\\.repo";

                CreateRepository(path);
                Console.WriteLine($"The repository is successfully created in {directoryPath.GetContent()}");
            }
            else
            {
                Console.WriteLine("This directory is already under vcs control");
            }
        }

        private void CreateRepository(string path)
        {
            DirectoryInfo directory = Directory.CreateDirectory(path); 
            directory.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 

            Directory.CreateDirectory(path + "\\objects");
            Directory.CreateDirectory(path + "\\references");
            Directory.CreateDirectory(path + "\\commits");
        }
    }
}