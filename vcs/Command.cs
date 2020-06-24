using System;
using System.Collections.Generic;
using System.IO;


namespace vcs
{
    public class Command
    {
        public string CommandName;
        public List<string> Properties;
        public CallDirectory CallDirectory;


        public Command(string command, List<string> properties, CallDirectory callDirectory)
        {
            CommandName = command;
            Properties = properties;
            CallDirectory = callDirectory;
        }
    }
}