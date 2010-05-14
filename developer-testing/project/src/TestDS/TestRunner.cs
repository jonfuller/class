using System.Collections.Generic;
using System.Linq;

namespace TestDS
{
    public class TestRunner
    {
        public IEnumerable<SuiteRunResult> Run(IEnumerable<TestSuite> theSuites)
        {
            return theSuites.Select(RunSuite);
        }

        private static SuiteRunResult RunSuite(TestSuite theSuite)
        {
            return theSuite
                .TestContainers
                .Select(container => new{ container.Name, Result = container.Run() })
                .Aggregate(
                    new SuiteRunResult { Name = theSuite.Name, Results = Enumerable.Empty<ContainerRunResult>() },
                    (state, current) => new SuiteRunResult()
                    {
                        Name = state.Name,
                        Results = state.Results.Concat(current.Result)
                    });
        }
    }
}