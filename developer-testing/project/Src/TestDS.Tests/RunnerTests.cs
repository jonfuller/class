using Machine.Specifications;

namespace TestDS.Tests
{
    [Subject("Test Runner")]
    public class Running_Suite_With_No_Tests : RunnerSpecs
    {
        Establish context =() =>
            TheSuite = Suites.NoTests;

        Because of = () =>
            Runner.Run(TheSuite);

        It suite_should_be_executed = () =>
            TheSuite.Executed.ShouldBeTrue();

        It should_tell_tracker_pass = () =>
            Tracker.Succeeded.ShouldBeTrue();

        It should_tell_tracker_zero_passing = () =>
            Tracker.NumberPassed.ShouldEqual(0);

        It should_tell_tracker_zero_failing = () =>
            Tracker.NumberFailed.ShouldEqual(0);
    }

    [Subject("Test Runner")]
    public class Running_Suite_With_1_Passing_Test : RunnerSpecs
    {
        Establish context =() =>
            TheSuite = Suites.OnePassingTest;

        Because of = () =>
            Runner.Run(TheSuite);

        It suite_should_be_executed = () =>
            TheSuite.Executed.ShouldBeTrue();

        It should_tell_tracker_pass = () =>
            Tracker.Succeeded.ShouldBeTrue();

        It should_tell_tracker_one_passing = () =>
            Tracker.NumberPassed.ShouldEqual(1);

        It should_tell_tracker_zero_failing = () =>
            Tracker.NumberFailed.ShouldEqual(0);
    }

    [Subject("Test Runner")]
    public class Running_Suite_With_1_Passing_Test_And_1_Failing_Test : RunnerSpecs
    {
        Establish context =() =>
            TheSuite = Suites.OnePassingTestOneFailingTest;

        Because of = () =>
            Runner.Run(TheSuite);

        It suite_should_be_executed = () =>
            TheSuite.Executed.ShouldBeTrue();

        It should_tell_tracker_pass = () =>
            Tracker.Succeeded.ShouldBeTrue();

        It should_tell_tracker_one_passing = () =>
            Tracker.NumberPassed.ShouldEqual(1);

        It should_tell_tracker_zero_failing = () =>
            Tracker.NumberFailed.ShouldEqual(1);
    }

    public abstract class RunnerSpecs
    {
        protected static TestTracker Tracker;
        protected static TestRunner Runner;
        protected static TestSuite TheSuite;

        Establish context =() =>
                           {
                               Tracker = new TestTracker();
                               Runner = new TestRunner(Tracker);
                           };
    }

    public static class Suites
    {
        public static TestSuite NoTests
        {
            get
            {
                return new TestSuite();
            }
        }

        public static TestSuite OnePassingTest
        {
            get
            {
                return new TestSuite()
                {
                    TestCases = new[]{new PassingTestCase()}
                };
            }
        }

        public static TestSuite OnePassingTestOneFailingTest
        {
            get
            {
                return new TestSuite()
                {
                    TestCases = new ITestCase[]
                    {
                        new PassingTestCase(),
                        new FailingTestCase()
                    }
                };
            }
        }
    }

    public class FailingTestCase : ITestCase
    {
        public bool Run()
        {
            return false;
        }
    }

    public class PassingTestCase : ITestCase
    {
        public bool Run()
        {
            return true;
        }
    }
}
