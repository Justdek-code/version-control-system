using System;
using System.Collections.Generic;


namespace vcs
{
    public class PathParser
    {
        private List<string> _directories;

        public PathParser(string path)
        {
            _directories = ParsePath(path);
        }

        public List<string> GetContent()
        {
            return _directories;
        }

        private List<string> ParsePath(string path)
        {
            List<string> directories = new List<string>(path.Split("\\"));

            return directories;
        }
    } 
}