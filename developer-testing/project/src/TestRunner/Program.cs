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

            var executed = new TestRunner().Run(suite);
            var total = executed.Passes + executed.Failures.Count();

            _output.WriteLine("Loaded: {0}".FormatWith(Path.GetFileName(assemblyName)));
            _output.WriteLine("  {0} test(s) loaded.".FormatWith(total));
            _output.WriteLine("  {0}/{1} test(s) passed ({2} failures).".FormatWith(executed.Passes, total, executed.Failures.Count()));
            _output.WriteLine("Failures: ");
            executed.Failures.Each(f =>
            {
                _output.WriteLine("  Container {0} in suite {1} contained {2} failures.".FormatWith(f.Name, suite.Name, f.Failures.Count()));
                f.Failures.Each(r => _output.WriteLine("    {0} failed. [{1}]".FormatWith(r.Name, r.Message)));
            });

            _output.Flush();
            
            return executed.Failures.Count() == 0;
        }
    }
}
