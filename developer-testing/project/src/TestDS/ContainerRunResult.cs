using System.Collections.Generic;
using System.Linq;

namespace TestDS
{
    public class ContainerRunResult
    {
        public string Name { get; set; }
        public IEnumerable<TestCaseResult> Results { get; set; }

        public int Passes
        {
            get { return Results.Where(r => r.Pass).Count(); }
        }

        public int Failures
        {
            get { return Results.Where(r => !r.Pass).Count(); }
        }
    }
}