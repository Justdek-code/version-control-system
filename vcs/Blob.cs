using System;
using System.Collections.Generic;
using System.IO;


namespace vcs
{
    public class Blob
    {
        public readonly string Hash;
        public readonly Path FilePath;

        public Blob (string hash, Path filePath)
        {
            Hash = hash;
            FilePath = filePath;
        }

        public Blob (Path filePath)
        {
            FilePath = filePath;
            Hash = GetFileHash(filePath);
        }

        public void CreateBlobObject(CallDirectory callDirectory)
        {
            string objectPath = callDirectory.GetRepositoryFolder().ToString() + $"\\objects\\{Hash}"; 
            using (StreamWriter streamWriter = new StreamWriter(objectPath))
            {
                StreamReader contentReader = File.OpenText(FilePath.ToString());
                streamWriter.Write(contentReader.ReadToEnd());
            }
        }
    
        public bool IsEqual(Blob compare)
        {
            if (Hash == compare.Hash && FilePath.IsEqual(compare.FilePath))
            {
                return true;
            }

            return false;
        }

        public bool ContainsIn(List<Blob> list)
        {
            foreach (Blob blob in list)
            {
                if (this.IsEqual(blob))
                {
                    return true;
                }
            }

            return false;
        }

        public string GetShortPath(CallDirectory callDirectory)
        {
            string repoPath = callDirectory.GetRepositoryFolder().ToString();
            string rootFolder = repoPath.Replace("\\.repo", "");

            string shortPath = FilePath.ToString().Replace(rootFolder, "");
            return shortPath;
        }  

        private string GetFileHash(Path filePath)
        {
            using (StreamReader file = File.OpenText(filePath.ToString()))
            {
                string content = file.ReadToEnd();
                Hash hash = new Hash(content);
                return hash.GetContent();
            }
        }
    }
}