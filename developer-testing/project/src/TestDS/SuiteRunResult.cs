using System.Collections.Generic;
using System.Linq;

namespace TestDS
{
    public class SuiteRunResult
    {
        public string Name { get; set; }
        public int Passes { get { return Results.Sum(r => r.Passes); } }
        public int Failures { get { return Results.Sum(r => r.Failures); } }
        public IEnumerable<ContainerRunResult> Results { get; set; }
    }
}