using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace TestDS.Tests
{
    [Subject("Test Runner")]
    public class Running_Suite_With_No_Tests : RunnerSpecs
    {
        Establish context = () =>
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
            Tracker.NumberFailed.ShouldEqual(0);
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
                    TestContainers = new[]{new PassingTestContainer()}
                };
            }
        }

        public static TestSuite OnePassingTestOneFailingTest
        {
            get
            {
                return new TestSuite()
                {
                    TestContainers = new ITestContainer[]
                    {
                        new PassingTestContainer(),
                        new FailingTestContainer()
                    }
                };
            }
        }
    }

    public class FailingTestContainer : ITestContainer
    {
        public IEnumerable<ITestCase> TestCases
        {
            get { return new ITestCase[0]; }
        }

        public RunResult Run()
        {
            return new RunResult()
            {
                Failures = 1,
                Passes = 0
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

        public RunResult Run()
        {
            return new RunResult()
                       {
                           Failures = 0,
                           Passes = 1
                       };
        }

        public string Name
        {
            get { return "passing"; }
        }
    }
}
