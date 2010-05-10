using System.Collections.Generic;

namespace TestDS
{
    public class TestSuite
    {
        public bool Executed;
        public IEnumerable<ITestContainer> TestContainers;

        public TestSuite()
        {
            TestContainers = new ITestContainer[] {};
        }
    }

    public interface ITestContainer
    {
        //IEnumerable<ITestCase> TestCases { get; }
        RunResult Run();
        string Name { get; }
    }

    public class RunResult
    {
        public int Passes { get; set; }
        public int Failures { get; set; }
    }

    public class TestContainer
    {
        public TestContainer()
        {
        }

        public string Name { get; set; }

        public RunResult Run()
        {
            return new RunResult()
                       {
                           Passes = 0,
                           Failures = 0
                       };
        }
    }
}