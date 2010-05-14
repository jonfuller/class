using Machine.Specifications;

namespace TestDS.Tests
{
    [Subject("Class Test Case")]
    public class running_a_passing_test_case
    {
        static ClassTestCase testCase;
        static bool result;
        public static bool testRan;

        Establish context = () =>
            testCase = new ClassTestCase(typeof(SampleCase), typeof(SampleCase).GetMethod("PassingCase"));

        Because of = () =>
            result = testCase.Run();

        It should_result_in_success_result = () =>
            result.ShouldBeTrue();

        It should_actually_run_method = () =>
            testRan.ShouldBeTrue();

        private class SampleCase
        {
            public void PassingCase()
            {
                running_a_passing_test_case.testRan = true;
            }
        }
    }

    public class running_a_failing_test_case
    {
        static ClassTestCase testCase;
        static bool result = true;

        Establish context = () =>
            testCase = new ClassTestCase(typeof(SampleCase), typeof(SampleCase).GetMethod("FailingCase"));

        Because of = () =>
            result = testCase.Run();

        It should_result_in_failure_result = () =>
            result.ShouldBeFalse();

        private class SampleCase
        {
            public void FailingCase()
            {
                throw new AssertionException();
            }
        }
    }
}
