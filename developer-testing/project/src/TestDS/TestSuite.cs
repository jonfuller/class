using System.Collections.Generic;

namespace TestDS
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