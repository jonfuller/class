using System.Collections.Generic;

namespace TestDS
{
    public class TestSuite
    {
        public bool Executed;
        public IEnumerable<ITestContainer> TestContainers { get; set; }
        public string Name { get; set; }

        public TestSuite()
        {
            TestContainers = new ITestContainer[] {};
        }

    }
}