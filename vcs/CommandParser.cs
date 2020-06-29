using System.Collections.Generic;
using System;


namespace vcs
{

    public class CommandParser
    {
        private Command _command;
        private string[] _input;
        private CallDirectory _callDirectory;


        public CommandParser(CallDirectory callDirectory, string[] input)
        {
            _input = input;
            _callDirectory = callDirectory;

            _command = Parse();
        }

        public Command GetContent()
        {
            return _command;
        }

        private Command Parse()
        {
            string commandName = _input[0];
            List<string> properties = new List<string>();

            for (int i = 1; i < _input.Length; i++)
            {
                properties.Add(_input[i]);
            }

            return new Command(commandName, properties, _callDirectory);
        }
    }
}