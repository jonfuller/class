using System.Collections.Generic;

namespace TestDS
{
    public class RunResult
    {
        public int Passes { get; set; }
        public IEnumerable<string> Failures { get; set; }
    }

    public class TestFailure
    {
        public string Suite { get; set; }
        public string Container { get; set; }
        public string Case { get; set; }
    }
}