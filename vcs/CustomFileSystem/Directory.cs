using System.Collections.Generic;
using System;

namespace vcs.CustomFileSystem
{
    public class Directory
    {
        public List<File> Files;
        public string DirectoryName;
        public List<Directory> Subdirectories;

        public Directory(List<File> files, List<Directory> subdirectories)
        {
            Files = files;
            Subdirectories = subdirectories;
        }

        public Directory(string directoryName)
        {
            Files = new List<File>();
            Subdirectories = new List<Directory>();
            DirectoryName = directoryName;
        }

        public bool ContainsSubdirectory(string subdirectoryName)
        {
            foreach (Directory existedSubdirectory in Subdirectories)
            {
                if (existedSubdirectory.DirectoryName == subdirectoryName)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddSubdirectory(Directory newSubdirectory)
        {
            if (!ContainsSubdirectory(newSubdirectory.DirectoryName))
            {
                Subdirectories.Add(newSubdirectory);
            }
        }

        public Directory GetSubdirectory(string name)
        {
            foreach (Directory subdirectory in Subdirectories)
            {
                if (subdirectory.DirectoryName == name)
                {
                    return subdirectory;
                }
            }

            throw new NotImplementedException($"error: custom file system there is no such subdirectory: {name} of {DirectoryName}");
        }

        public Tree ConvertIntoTree(CallDirectory callDirectory, Path currentPath)
        {
            List<Tree> subtrees = new List<Tree>();
            List<Blob> blobs = new List<Blob>();

            foreach (Directory subdirectory in Subdirectories)
            {
                Path nextPath = currentPath.ConcatPath(subdirectory.DirectoryName);
                subtrees.Add(subdirectory.ConvertIntoTree(callDirectory, nextPath));
            }

            foreach (File file in Files)
            {
                Path filePath = currentPath.ConcatPath(file.FileName);
                Blob blob = new Blob(filePath);
                blobs.Add(blob);
            }

            return new Tree(blobs, subtrees, DirectoryName);
        }

        private bool IsEqualName(Directory compare)
        {
            if (this.DirectoryName == compare.DirectoryName)
            {
                return true;
            }

            return true;
        }
    }
}