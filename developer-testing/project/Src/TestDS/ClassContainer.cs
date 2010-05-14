using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

    public class ClassTestCase : ITestCase
    {
        private readonly Type _type;
        private readonly MethodInfo _methodInfo;

        public ClassTestCase(Type type, MethodInfo methodInfo)
        {
            _type = type;
            _methodInfo = methodInfo;
        }

        public bool Run()
        {
            try
            {
                var testObj = Activator.CreateInstance(_type);
                _methodInfo.Invoke(testObj, new object[] { });
                return true;
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null && e.InnerException is AssertionException)
                    return false;
                throw;
            }
        }

        public string Name
        {
            get { return _methodInfo.Name; }
        }
    }
}