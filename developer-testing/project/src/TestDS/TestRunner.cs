using System.Linq;

namespace TestDS
{
    public class TestRunner
    {
        public RunResult Run(TestSuite theSuite)
        {
            return theSuite
                .TestContainers
                .Select(container => container.Run())
                .Aggregate(
                    new RunResult() {Passes = 0, Failures = 0},
                    (state, current) => new RunResult()
                    {
                        Passes = state.Passes + current.Passes,
                        Failures = state.Failures + current.Failures
                    });
        }
    }
}