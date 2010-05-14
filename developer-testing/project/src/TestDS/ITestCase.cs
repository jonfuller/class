using System;

namespace TestDS
{
    public interface ITestCase
    {
        TestCaseResult Run();
        string Name { get; }
    }

    public class TestCaseResult
    {
        public bool Pass { get; set;}
        public string Message { get; set; }
        public string Name { get; set; }
    }
}