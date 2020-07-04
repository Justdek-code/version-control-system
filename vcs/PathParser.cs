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

        public PathParser(Path path)
        {
            _directories = ParsePath(path.ToString());
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