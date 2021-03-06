using System;
using System.IO;
using System.Collections.Generic;

namespace vcs.Commands
{
    // vcs init
    public class InitCommand
    {
        private Command _command;

        public InitCommand(Command command)
        {
            _command = command;
        }

        public void Execute()
        {
            CallDirectory callDirectory = _command.CallDirectory;

            if (!callDirectory.IsUnderVersionControl())
            { 
                Path directoryPath = callDirectory.GetPath();
                string path = directoryPath.ToString() + "\\.repo";

                InitializeRepository(path);
                Console.WriteLine($"The repository is successfully created in {directoryPath.ToString()}");
            }
            else
            {
                Console.WriteLine("This directory is already under vcs control");
            }
        }

        private void InitializeRepository(string path)
        {
            DirectoryInfo directory = Directory.CreateDirectory(path); 
            directory.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 

            Directory.CreateDirectory(path + "\\objects");
            Directory.CreateDirectory(path + "\\references");
            Directory.CreateDirectory(path + "\\commits");

            using (FileStream fileStream = File.Create(path + "\\index")) {}
            using (FileStream fileStream = File.Create(path + "\\HEAD")) {}
            using (FileStream fileStream = File.Create(path + "\\references\\master")) {}
        }
    }
}