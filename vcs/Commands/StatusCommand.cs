using System;
using System.Collections.Generic;
using System.IO;


namespace vcs.Commands
{
    public class StatusCommand
    {
        private CallDirectory _callDirectory;

        public StatusCommand(CallDirectory callDirectory)
        {
            _callDirectory = callDirectory;
        }

        public void Execute()
        {
            GetCurrentStage().PrintInformation(_callDirectory);
        }

        public Stage GetCurrentStage()
        {
            List<Blob> untracked = new List<Blob>();
            List<Blob> tracked = new List<Blob>();

            IndexParser indexParser = new IndexParser(_callDirectory);
            WorkingDirectory workingDirectory = new WorkingDirectory(_callDirectory);

            List<Blob> stagedBlobs = indexParser.GetContent();
            List<Path> workingFiles = workingDirectory.GetContent();

            foreach (Path filePath in workingFiles)
            {
                Blob workingFile = new Blob(filePath);

                if (workingFile.ContainsIn(stagedBlobs))
                {
                    tracked.Add(workingFile);
                }
                else
                {
                    untracked.Add(workingFile);
                }
            }

            return new Stage(tracked, untracked);
        }
    }
}