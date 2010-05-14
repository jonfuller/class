using System.Collections.Generic;

namespace TestDS
{
    public interface ITestContainer
    {
        IEnumerable<ITestCase> TestCases { get; }
        ContainerRunResult Run();
        string Name { get; }
    }
}