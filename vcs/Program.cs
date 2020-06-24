using System.IO;
using System;

namespace vcs
{
    class Program
    {
        // vcs <command> <properties> ...

        static void Main(string[] args)
        {

            string[] input = { "init" };

            new Executor(
                new CommandParser(     
                    new CallDirectory(),
                    input
                )
            ).Run();
            
        }
    }
}
     