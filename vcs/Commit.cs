using System.IO;
using System;


namespace vcs
{
    public class Commit
    {
        private string _treeHash;
        private string _parentCommit;
        private string  _message;

        public Commit(string treeHash,  string message, string parentCommit)
        {
            _treeHash = treeHash;
            _parentCommit = parentCommit;
            _message = message;
        }

        public Hash CreateCommitObject(CallDirectory callDirectory)
        {
            string content = ParseContent();
            Hash hash = new Hash(content);

            Path commitPath = callDirectory.GetRepositoryFolder().ConcatPath("commits");
            commitPath = commitPath.ConcatPath(hash.GetContent());
            using (StreamWriter streamWriter = new StreamWriter(commitPath.ToString()))
            {
                streamWriter.Write(content);
            }

            return hash;
        }

        private string ParseContent()
        {
            string line1 = "tree: " + _treeHash + "\n";
            string line2 = "parent: " + _parentCommit + "\n";
            string line3 = "message: " + _message;

            return line1 + line2 + line3;
        }
    }
}