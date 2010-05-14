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

        public bool Start(params string[] assemblyNames)
        {
            var loader = new AssemblyTestLoader();
            var suites = loader.Load(assemblyNames);

            var executed = new TestRunner().Run(suites);
            var totalPasses = executed.Sum(x => x.Passes);
            var totalFailures = executed.Sum(x => x.Failures);
            var total = totalPasses + totalFailures;

            _output.WriteLine("Loaded: {0}".FormatWith(suites.Select(s => s.Name).Join(", ")));
            _output.WriteLine("  {0} test(s) loaded.".FormatWith(total));
            _output.WriteLine("  {0}/{1} test(s) passed ({2} failures).".FormatWith(totalPasses, total, totalFailures));
            
            if (totalFailures > 0 )
                _output.WriteLine("Failures: ");
            executed.Each(suiteResult => suiteResult
                .Results
                .Where(containerResult => containerResult.Results.Any(r => !r.Pass))
                .Each(f =>
                {
                    _output.WriteLine("  {0}.{1} contained {2} failures.".FormatWith(suiteResult.Name, f.Name, f.Failures));
                    f.Results.Where(r => !r.Pass).Each(r => _output.WriteLine("    {0} failed. [{1}]".FormatWith(r.Name, r.Message)));
                }));

            _output.Flush();
            
            return totalFailures == 0;
        }
    }
}
