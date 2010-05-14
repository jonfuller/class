using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDS
{
    public class ClassContainer : ITestContainer
    {
        private readonly Type _type;
        private readonly IEnumerable<ClassTestCase> _testCases;

        public ClassContainer(Type type)
        {
            _type = type;
            _testCases = _type
                    .GetMethods()
                    .Where(m => m.Name.EndsWith("test", StringComparison.InvariantCultureIgnoreCase))
                    .Where(m => m.ReturnType == typeof(void))
                    .Where(m => m.GetParameters().Count() == 0)
                    .Where(m => !m.IsGenericMethod)
                    .Select(m => new ClassTestCase(_type, m));
        }

        public IEnumerable<ITestCase> TestCases { get { return _testCases; } }
        public string Name
        {
            get { return _type.Name; }
        }

        public ContainerRunResult Run()
        {
            return _testCases.Aggregate(
                new ContainerRunResult() {Results = Enumerable.Empty<TestCaseResult>()},
                (state, testCase) => new ContainerRunResult
                {
                    Name = Name,
                    Results = state.Results.Concat(testCase.Run())
                });
        }
    }
}