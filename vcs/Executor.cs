using System;
using System.Collections.Generic;


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
            if (_command.CommandName == "init") new Init(_command.CallDirectory).Execute(); 
        }
    }
}