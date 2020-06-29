using System;
using System.IO;
using System.Collections.Generic;


namespace vcs.Commands
{
    // vcs add [-a]
    // vcs add <filePath>
    public class StageDirector
    {   
        private Command _command;

        public StageDirector(Command command)
        {
            _command = command;
        }

        public void Execute()
        {
            CallDirectory callDirectory = _command.CallDirectory;

            if (!callDirectory.IsUnderVersionControl())
            {
                throw new NotImplementedException("The call directory is not under version control system");
            }

            if (_command.Properties[0] == "-a")
            {
                Path rootDirectory = callDirectory.FindRepositoryRoot();

                IEnumerable<string> files = Directory.EnumerateFiles(rootDirectory.GetContent());

                foreach (string file in files)
                {

                }
            }
        }

        
    }
}