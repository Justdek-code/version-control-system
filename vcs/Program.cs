using System.IO;
using System;

namespace vcs
{
    class Program
    {
        // vcs <command> <properties> ...

        static void Main(string[] args)
        {

            string[] command = { "add", "Hash.cs" };
            
            CallDirectory callDirectory = new CallDirectory();
            CommandParser commandParser = new CommandParser(callDirectory, args);
            Executor commandExecutor = new Executor(commandParser);
            commandExecutor.Run();
            
        }
    }
}
     