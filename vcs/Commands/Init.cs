using System;
using System.IO;
using System.Collections.Generic;

namespace vcs
{
    public class Init
    {
        private string _callDirectory;

        public Init(string callDirectory)
        {
            _callDirectory = callDirectory;
        }

        public void Execute()
        {
            string path = _callDirectory + "\\.repo";

            if (!Directory.Exists(path))
            {
                CreateRepository(path);
                Console.WriteLine("The repository is successfully created");
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