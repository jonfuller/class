using System.Collections.Generic;

namespace TestDS
{
    public class MultiReporter : IReporter
    {
        private readonly IEnumerable<IReporter> _reporters;

        public MultiReporter(IEnumerable<IReporter> reporters)
        {
            _reporters = reporters;
        }

        public void Report(IEnumerable<SuiteRunResult> executed)
        {
            _reporters.Each(r => r.Report(executed));
        }
    }
}