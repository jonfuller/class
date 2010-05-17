using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestDS;

namespace Runner
{
    public class Program
    {
        public static int Main(string[] cmdLineArgs)
        {
            var args = ParseArgs(cmdLineArgs);

            var reporter = new MultiReporter(Enumerable.Empty<IReporter>()
                .Append(new TextReporter(Console.Out))
                .AppendIf(
                    () => args[CmdArgs.XmlFileName].Select(x => new XmlReporter(() => new StreamWriter(x))),
                    () => args.ContainsKey(CmdArgs.XmlFileName))
                );

            var assemblies = (IEnumerable<string>)args[CmdArgs.Assemblies] ?? new string[0];

            var exitCode = new Application(reporter).Start(assemblies)
                ? 0
                : 1;

            Console.WriteLine(string.Empty);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            return exitCode;
        }

        public static Dictionary<string, List<string>> ParseArgs(string[] cmdLineArgs)
        {
            return CommandLineParser.ParseCommandLine(cmdLineArgs, true);
        }
    }

    public static class CmdArgs
    {
        public static string Assemblies = "";
        public static string XmlFileName = "xml";
    }
}
