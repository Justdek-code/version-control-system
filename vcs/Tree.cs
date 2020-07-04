using System;
using System.Collections.Generic;


namespace vcs
{
    public class Tree
    {
        public Path Path;
        public List<Blob> Blobs;
        public List<Tree> Subtrees;

        public Tree(List<Blob> blobs, List<Tree> subtrees)
        {
            Blobs = blobs;
            Subtrees = subtrees;
        }
    }
}