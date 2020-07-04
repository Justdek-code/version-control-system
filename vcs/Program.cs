using System.IO;
using System;
using vcs.CustomFileSystem;
using vcs.Commands;

namespace vcs
{
    class Program
    {
        // vcs <command> <properties> ...

        static void Main(string[] args)
        {

            string[] command = { "add", "Commands/StatusCommand.cs" };
            
            CallDirectory callDirectory = new CallDirectory();
            CommandParser commandParser = new CommandParser(callDirectory, args);
            Executor commandExecutor = new Executor(commandParser);
            commandExecutor.Run();
            
            StatusCommand status = new StatusCommand(callDirectory);
            Stage stage = status.GetCurrentStage();
            
            FileSystem customFileSystem = new FileSystem(stage.Tracked, callDirectory);
            
        }
    }
}
     