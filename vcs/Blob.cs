using System;
using System.Collections.Generic;
using System.IO;


namespace vcs
{
    public class Blob
    {
        public readonly string Hash;
        public readonly string FilePath;

        public Blob (string hash, string filePath)
        {
            Hash = hash;
            FilePath = filePath;
        }

        public Blob (Path filePath)
        {
            FilePath = filePath.GetContent();
            Hash = GetFileHash(filePath);
        }

        public bool IsEqual(Blob compare)
        {
            if (Hash == compare.Hash && FilePath == compare.FilePath)
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

        private string GetFileHash(Path filePath)
        {
            using (StreamReader file = File.OpenText(filePath.GetContent()))
            {
                string content = file.ReadToEnd();
                Hash hash = new Hash(content);
                return hash.GetContent();
            }
        }
    }
}