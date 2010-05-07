namespace TestDS
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
}