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
            if (_command.CommandName == "init") new InitCommand(_command).Execute(); 
            if (_command.CommandName == "status") new StatusCommand(_command.CallDirectory).Execute();
            if (_command.CommandName == "add") new AddCommand(_command).Execute();
        }
    }
}