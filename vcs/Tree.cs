using System;
using System.Collections.Generic;
using System.IO;

namespace vcs
{
    public class Tree
    {
        public Path Path;
        public List<Blob> Blobs;
        public List<Tree> Subtrees;
        public string Hash;
        public string FolderName;

        public Tree(List<Blob> blobs, List<Tree> subtrees, string folderName)
        {
            Blobs = blobs;
            Subtrees = subtrees;
            FolderName = folderName;
        }

        public void CreateTreeObject(CallDirectory callDirectory)
        {
            string content = String.Empty;

            foreach (Tree subtree in Subtrees)
            {
                //tree <hash> <folderName>
                subtree.CreateTreeObject(callDirectory);
                content += CreateTreeDataUnit(subtree);
            }

            foreach (Blob blob in Blobs)
            {
                //blob <hash> <fileName>
                content += CreateTreeDataUnit(blob);
            }

            Hash hash = new Hash(content);
            Hash = hash.GetContent();

            Path objectPath = callDirectory.GetRepositoryFolder();
            objectPath = objectPath.ConcatPath("objects");
            objectPath = objectPath.ConcatPath(Hash);

            using (StreamWriter streamWriter = new StreamWriter(objectPath.ToString()))
            {
                streamWriter.Write(content);
            }
        }

        private string CreateTreeDataUnit(Blob blob)
        {
            string line = "blob" + " ";
            line += blob.Hash + " ";
            line += blob.FilePath.GetFileName() + "\n";
            
            return line;
        }

        private string CreateTreeDataUnit(Tree tree)
        {
            string line = "tree ";
            line += tree.Hash + " ";
            line += tree.FolderName + "\n";
            
            return line;
        }
    }
}