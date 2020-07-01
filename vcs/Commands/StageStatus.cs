using System;
using System.Collections.Generic;
using System.IO;


namespace vcs
{
    public class StageStatus
    {
        private CallDirectory _callDirectory;

        public StageStatus(CallDirectory callDirectory)
        {
            _callDirectory = callDirectory;
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