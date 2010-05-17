using System.Collections.Generic;
using System.Linq;
using TestDS;

namespace Runner
{
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