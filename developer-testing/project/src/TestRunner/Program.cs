using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TestDS;

namespace Runner
{
    class Program
    {
        private static string GetPath(string assemblyName)
        {
            return Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase),
                assemblyName)
                .Substring(6);
        }

        static int Main(string[] args)
        {
            var exitCode = new Application(new TextReporter(Console.Out)).Start(new []{GetPath("OneTest.dll"), GetPath("SomeTests.dll")})
                ? 0
                : 1;

            Console.WriteLine(string.Empty);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            return exitCode;
        }
    }

    public class Application
    {
        private readonly IReporter _reporter;

        public Application(IReporter reporter)
        {
            _reporter = reporter;
        }

        public bool Start(IEnumerable<string> assemblyNames)
        {
            var loader = new AssemblyTestLoader();
            var suites = loader.Load(assemblyNames);

            var executed = new TestRunner().Run(suites);

            _reporter.Report(executed);

            return executed.Sum(x => x.Failures) == 0;
        }
    }
}
