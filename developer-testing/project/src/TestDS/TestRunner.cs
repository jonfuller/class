using System.Collections.Generic;
using System.Linq;

namespace TestDS
{
    public class TestRunner
    {
        public SuiteRunResult Run(TestSuite theSuite)
        {
            return theSuite
                .TestContainers
                .Select(container => new{ container.Name, Result = container.Run() })
                .Aggregate(
                    new SuiteRunResult { Name = theSuite.Name, Passes = 0, Failures = new ContainerFailure[0] },
                    (state, current) => new SuiteRunResult()
                    {
                        Passes = state.Passes + current.Result.Passes,
                        Failures = current.Result.Results.All(r => r.Pass)
                            ? state.Failures
                            : state.Failures.Concat(
                                new ContainerFailure()
                                {
                                    Name = current.Name,
                                    Failures = current.Result
                                    .Results
                                    .Where(r => !r.Pass)
                                    .Eval()
                                })
                    });
        }
    }

    public class SuiteRunResult
    {
        public string Name { get; set; }
        public int Passes { get; set; }
        public IEnumerable<ContainerFailure> Failures { get; set; }
    }

    public class ContainerFailure
    {
        public string Name { get; set; }
        public IEnumerable<TestCaseResult> Failures;
    }
}