using System;
using System.Collections.Generic;
using vcs.Commands;


namespace vcs
{

    public class Executor
    {
        private Command _command;
        
        public Executor(CommandParser parser)
        {
            _command = parser.GetContent();
        }

        public void Run()
        {
            string commandName = _command.CommandName;
            if (commandName == "init") new InitCommand(_command).Execute(); 
            if (commandName == "status") new StatusCommand(_command.CallDirectory).Execute();
            if (commandName == "add") new AddCommand(_command).Execute();
            if (commandName == "commit") new CommitCommand(_command).Execute();
        }
    }
}