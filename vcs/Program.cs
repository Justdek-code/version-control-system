using System.IO;
using System;

namespace vcs
{
    class Program
    {
        // vcs <command> <properties> ...

        static void Main(string[] args)
        {

            string[] command = { "init" };
            
            CallDirectory callDirectory = new CallDirectory();
            CommandParser commandParser = new CommandParser(callDirectory, command);
            Executor commandExecutor = new Executor(commandParser);
            commandExecutor.Run();
            
            IndexParser stage = new IndexParser(callDirectory);
            stage.GetContent();

            WorkingDirectory workingDirectory = new WorkingDirectory(callDirectory);
            workingDirectory.GetContent();

            StageStatus status = new StageStatus(callDirectory);
            status.GetCurrentStage().PrintInformation();
        }
    }
}
     