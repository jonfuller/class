using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestDS
{
    public class TextReporter : IReporter
    {
        private readonly TextWriter _output;

        public TextReporter(TextWriter output)
        {
            _output = output;
        }
        
        public void Report(IEnumerable<SuiteRunResult> executed)
        {
            var totalPasses = executed.Sum(x => x.Passes);
            var totalFailures = executed.Sum(x => x.Failures);
            var total = totalPasses + totalFailures;

            _output.WriteLine("Loaded: {0}".FormatWith(executed.Select(s => s.Name).Join(", ")));
            _output.WriteLine("  {0} {1} loaded.".FormatWith(
                total,
                "test".Pluralize(total)));
            _output.WriteLine("  {0}/{1} {2} passed ({3} {4}).".FormatWith(
                totalPasses,
                total,
                "test".Pluralize(total),
                totalFailures,
                "failure".Pluralize(totalFailures)));

            if (totalFailures > 0)
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
        }
    }
}