using System;
using System.Collections.Generic;
using System.IO;


namespace vcs
{
    public class Command
    {
        public string CommandName;
        public List<string> Properties;
        public string CallDirectory;


        public Command(string command, List<string> properties, string callDirectory)
        {
            CommandName = command;
            Properties = properties;
            CallDirectory = callDirectory;
        }
    }
}