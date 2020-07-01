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

            if (_command.Properties[0] == "--all")
            {

            }
            else
            {
                foreach(string file in _command.Properties)
                {
                    string filePath = callDirectory.GetPath().ToString() + "\\" + file;

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine($"error: {filePath} file path doesn't exist");
                        return;
                    }

                    StageStatus stageStatus = new StageStatus(callDirectory);
                    Stage stage = stageStatus.GetCurrentStage();

                    Blob fileBlob = new Blob(new Path(filePath));

                    if (fileBlob.ContainsIn(stage.Untracked))
                    {
                        Stage newStage = stage.AddToTracked(fileBlob);
                        newStage.WriteToIndex(callDirectory);
                        fileBlob.CreateBlobObject(callDirectory);
                    }
                    else
                    {
                        Console.WriteLine($"file \"{filePath}\" is already tracked");
                    }
                }
            }
        }
    }
}