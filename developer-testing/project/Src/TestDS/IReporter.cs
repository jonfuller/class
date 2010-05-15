using System.Collections.Generic;

namespace TestDS
{
    public interface IReporter
    {
        void Report(IEnumerable<SuiteRunResult> executed);
    }
}