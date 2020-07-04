using System;
using System.IO;
using System.Collections.Generic;


namespace vcs
{
    public class IndexParser
    {
        private List<Blob> _stagedFiles;
        private CallDirectory _callDirectory;

        public IndexParser(CallDirectory callDirectory)
        {
            _callDirectory = callDirectory;
            _stagedFiles = ReadIndex();
        }

        public List<Blob> GetContent()
        {
            return new List<Blob>(_stagedFiles);
        }

        private List<Blob> ReadIndex()
        {
            List<Blob> blobsInfo = new List<Blob>();
            Path repositoryPath = _callDirectory.GetRepositoryFolder();
            string indexPath = repositoryPath.ToString() + "\\index";

            string[] lines = File.ReadAllLines(indexPath);
            foreach (string line in lines)
            {
                if (line == String.Empty)
                {
                    break;
                }
                
                blobsInfo.Add(ParseBlobInfo(line));
            }

            return blobsInfo;
        }

        private Blob ParseBlobInfo(string line)
        {
            string[] content = line.Split(" ");

            string hash = content[0]; 
            string filePath = content[1];

            return new Blob(hash, new Path(filePath));
        }
    }
}