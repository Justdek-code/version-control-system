using System;
using System.IO;
using System.Collections.Generic;


namespace vcs.Commands
{
    // vcs add [-a]
    // vcs add <filePath>
    public class AddCommand
    {   
        private Command _command;
        private CallDirectory _callDirectory;

        public AddCommand(Command command)
        {
            _command = command;
            _callDirectory = command.CallDirectory;
        }

        public void Execute()
        {
            if (!_callDirectory.IsUnderVersionControl())
            {
                throw new NotImplementedException("The call directory is not under version control system");
            }

            if (_command.Properties[0] == "--all")
            {

            }
            else
            {
                foreach(string file in _command.Properties)
                {
                    AddFile(file);
                }
            }
        }

        private void AddFile(string file)
        {
            string filePath = _callDirectory.GetPath().ToString() + "\\" + file;

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"error: {filePath} file path doesn't exist");
                return;
            }

            StatusCommand stageStatus = new StatusCommand(_callDirectory);
            Stage stage = stageStatus.GetCurrentStage();

            Blob fileBlob = new Blob(new Path(filePath));

            if (fileBlob.ContainsIn(stage.Untracked))
            {
                Stage newStage = stage.AddToTracked(fileBlob);
                newStage.WriteToIndex(_callDirectory);
                fileBlob.CreateBlobObject(_callDirectory);
            }
            else
            {
                Console.WriteLine($"file \"{filePath}\" is already tracked");
            }
        }
    }
}