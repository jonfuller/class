using System.Collections.Generic;

namespace TestDS.Tests
{
    public class TestSuite
    {
        public bool Executed;
        public IEnumerable<ITestCase> TestCases;

        public TestSuite()
        {
            TestCases = new ITestCase[] {};
        }
    }
}