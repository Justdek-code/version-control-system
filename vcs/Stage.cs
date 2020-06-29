using System.Collections.Generic;
using System;

namespace vcs 
{
    public class Stage
    {
        public readonly List<Blob> Tracked;
        public readonly List<Blob> Untracked;

        public Stage(List<Blob> tracked, List<Blob> untracked)
        {
            Tracked = tracked;
            Untracked = untracked;
        }

        public void PrintInformation()
        {
            Console.WriteLine("tracked: ");
            foreach (Blob file in Tracked)
            {
                Console.WriteLine($"\t {file.FilePath}");
            }

            Console.WriteLine("\nuntracked: ");
            foreach (Blob file in Untracked)
            {
                Console.WriteLine($"\t {file.FilePath}");
            }
        }
    }
}