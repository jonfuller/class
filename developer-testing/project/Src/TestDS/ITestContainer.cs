using System.Collections.Generic;

namespace TestDS
{
    public interface ITestContainer
    {
        IEnumerable<ITestCase> TestCases { get; }
        RunResult Run();
        string Name { get; }
    }
}