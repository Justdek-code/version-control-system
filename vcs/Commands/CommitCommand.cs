using System;
using System.IO;
using System.Collections.Generic;
using vcs.CustomFileSystem;


namespace vcs.Commands
{
    public class CommitCommand
    {
        private CallDirectory _callDirectory;
        private Command _command;

        public CommitCommand(Command command)
        {
            _callDirectory = command.CallDirectory;
            _command = command;
        }

        public void Execute()
        {
            StatusCommand status = new StatusCommand(_callDirectory);
            Stage stage = status.GetCurrentStage();

            FileSystem customFileSystem = new FileSystem(stage.Tracked, _callDirectory);
            CustomFileSystem.Directory directory = customFileSystem.GetContent();

            Path startPath = _callDirectory.GetCommonDirectory();
            Tree tree = directory.ConvertIntoTree(_callDirectory, startPath);
            tree.CreateTreeObject(_callDirectory);
            
            string commitMessage = String.Empty;
            if (_command.Properties.Count > 0)
            {
                commitMessage = _command.Properties[0];
            }

            Commit commit = new Commit(tree.Hash, commitMessage, "");
            Hash hash = commit.CreateCommitObject(_callDirectory);

            UpdateCommitReference(hash);
        }

        private void UpdateCommitReference(Hash hash)
        {
            Path referencePath = _callDirectory.GetRepositoryFolder().ConcatPath("references");
            referencePath = referencePath.ConcatPath("master"); // TODO when add branches

            using (StreamWriter streamWriter = new StreamWriter(referencePath.ToString()))
            {
                streamWriter.Write(hash.GetContent());
            }
        }
    }
}