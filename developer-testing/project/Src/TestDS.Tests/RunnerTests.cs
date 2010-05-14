using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace TestDS.Tests
{
    [Subject("Test Runner")]
    public class Running_Multiple_Suites : RunnerSpecs
    {
        Establish context = () =>
            TheSuites = Suites.NoTests.Concat(Suites.OnePassingTest).Concat(Suites.OnePassingTestOneFailingTest);

        Because of = () =>
            Results = Runner.Run(TheSuites);

        It should_result_in_two_passing = () =>
            Results.Sum(x => x.Passes).ShouldEqual(2);

        It should_result_in_one_failing = () =>
            Results.Sum(x => x.Failures).ShouldEqual(1);
    }

    [Subject("Test Runner")]
    public class Running_Suite_With_No_Tests : RunnerSpecs
    {
        Establish context = () =>
            TheSuite = Suites.NoTests;

        Because of = () =>
            Results = Runner.Run(TheSuite);

        It should_result_in_zero_passing = () =>
            Results.Sum(x => x.Passes).ShouldEqual(0);

        It should_result_in_zero_failing = () =>
            Results.Sum(x => x.Failures).ShouldEqual(0);
    }

    [Subject("Test Runner")]
    public class Running_Suite_With_1_Passing_Test : RunnerSpecs
    {
        Establish context =() =>
            TheSuite = Suites.OnePassingTest;

        Because of = () =>
            Results = Runner.Run(TheSuite);

        It should_result_in_one_passing = () =>
            Results.Sum(x => x.Passes).ShouldEqual(1);

        It should_result_in_zero_failing = () =>
            Results.Sum(x => x.Failures).ShouldEqual(0);
    }

    [Subject("Test Runner")]
    public class Running_Suite_With_1_Passing_Test_And_1_Failing_Test : RunnerSpecs
    {
        Establish context =() =>
            TheSuite = Suites.OnePassingTestOneFailingTest;

        Because of = () =>
            Results = Runner.Run(TheSuite);

        It should_result_in_one_passing = () =>
            Results.Sum(x => x.Passes).ShouldEqual(1);

        It should_result_in_zero_failing = () =>
            Results.Sum(x => x.Failures).ShouldEqual(1);
    }

    public abstract class RunnerSpecs
    {
        protected static TestRunner Runner;
        protected static IEnumerable<TestSuite> TheSuites;
        protected static IEnumerable<TestSuite> TheSuite;
        protected static IEnumerable<SuiteRunResult> Results;

        Establish context =() =>
                           {
                               Runner = new TestRunner();
                           };
    }

    public static class Suites
    {
        public static IEnumerable<TestSuite> NoTests
        {
            get
            {
                return new []{ new TestSuite() };
            }
        }

        public static IEnumerable<TestSuite> OnePassingTest
        {
            get
            {
                return new []{ new TestSuite()
                {
                    TestContainers = new[]{new PassingTestContainer()}
                }};
            }
        }

        public static IEnumerable<TestSuite> OnePassingTestOneFailingTest
        {
            get
            {
                return new []{ new TestSuite()
                {
                    TestContainers = new ITestContainer[]
                    {
                        new PassingTestContainer(),
                        new FailingTestContainer()
                    }
                }};
            }
        }
    }

    public class FailingTestContainer : ITestContainer
    {
        public IEnumerable<ITestCase> TestCases
        {
            get { return new ITestCase[0]; }
        }

        public ContainerRunResult Run()
        {
            return new ContainerRunResult()
            {
                Name = Name,
                Results = new[]
                {
                    new TestCaseResult(){Pass = false, Message = "yo"}
                }
            };
        }

        public string Name
        {
            get { return "failing"; }
        }
    }

    public class PassingTestContainer : ITestContainer
    {
        public IEnumerable<ITestCase> TestCases
        {
            get { return new ITestCase[0]; }
        }

        public ContainerRunResult Run()
        {
            return new ContainerRunResult()
            {
                Name = Name,
                Results = new[]
                {
                    new TestCaseResult(){Pass = true }
                }
            };
        }

        public string Name
        {
            get { return "passing"; }
        }
    }
}
