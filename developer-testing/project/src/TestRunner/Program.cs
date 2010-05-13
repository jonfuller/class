using System;
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
            return new Application(Console.Out).Start(GetPath("OneTest.dll"))
                ? 0
                : 1;
        }
    }

    public class Application
    {
        private readonly TextWriter _output;

        public Application(TextWriter output)
        {
            _output = output;
        }

        public bool Start(string assemblyName)
        {
            var loader = new AssemblyTestLoader();
            var suite = loader.Load(assemblyName);
            var numLoaded = suite.TestContainers.Sum(container => container.TestCases.Count());

            var executed = suite
                .TestContainers
                .Select(container => container.Run())
                .Aggregate(
                    new RunResult() {Passes = 0, Failures = 0},
                    (state, current) => new RunResult()
                                        {
                                            Passes = state.Passes + current.Passes,
                                            Failures = state.Failures + current.Failures
                                        });

            _output.WriteLine("Loaded: {0}".FormatWith(Path.GetFileName(assemblyName)));
            _output.WriteLine("  {0} test(s) loaded.".FormatWith(numLoaded));
            _output.WriteLine("  {0}/{1} test(s) passed ({2} failures).".FormatWith(executed.Passes, numLoaded, executed.Failures));
            _output.Flush();
            
            return true;
        }
    }
}
