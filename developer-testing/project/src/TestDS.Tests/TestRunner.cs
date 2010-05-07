using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDS.Tests
{
    public class TestRunner
    {
        private readonly TestTracker _tracker;

        public TestRunner(TestTracker tracker)
        {
            _tracker = tracker;
        }

        public void Run(TestSuite theSuite)
        {
            theSuite.Executed = true;
            _tracker.Succeeded = true;

            var numPassed = 0;
            var numFailed = 0;

            theSuite.TestCases.Each(testCase =>
                                    {
                                        if (testCase.Run())
                                            numPassed++;
                                        else
                                            numFailed++;
                                    });

            _tracker.NumberPassed = numPassed;
            _tracker.NumberFailed = numFailed;
        }
    }

    public static class Ext
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> target, Action<T> action)
        {
            foreach(var item in target)
                action(item);
            return target;
        }
    }
}