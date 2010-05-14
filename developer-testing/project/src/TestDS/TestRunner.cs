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
                    new SuiteRunResult { Name = theSuite.Name, Results = Enumerable.Empty<ContainerRunResult>() },
                    (state, current) => new SuiteRunResult()
                    {
                        Results = state.Results.Concat(current.Result)
                    });
        }
    }
}