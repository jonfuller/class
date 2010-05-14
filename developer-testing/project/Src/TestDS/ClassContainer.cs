using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDS
{
    public class ClassContainer : ITestContainer
    {
        private readonly Type _type;
        private IEnumerable<ClassTestCase> _testCases;

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

        public RunResult Run()
        {
            return _testCases.Aggregate(
                new RunResult() {Passes = 0, Failures = 0},
                (state, testCase) =>
                    testCase.Run()
                        ? new RunResult() {Passes = state.Passes + 1, Failures = state.Failures}
                        : new RunResult() {Passes = state.Passes, Failures = state.Failures + 1});
        }

        public string Name
        {
            get { return _type.Name; }
        }
    }
}