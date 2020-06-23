using System.IO;
using System;

namespace vcs
{
    class Program
    {
        // vcs <command> <properties> ...

        static void Main(string[] args)
        {

            new Executor(
                new CommandParser(     
                    new CurrentDirectory(),
                    args
                )
            ).Run();
            
        }
    }
}
     