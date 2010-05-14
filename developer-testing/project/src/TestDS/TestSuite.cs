using System;
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

        public string Name { get; set; }
    }
}