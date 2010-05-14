using System.IO;
using Machine.Specifications;
using Runner;

namespace TestDS.Tests
{
    [Subject("Acceptance")]
    public class Running_Suite_With_A_Test : ApplicationSpecs
    {
        Because of = () =>
            exitCode = application.Start(Assemblies.OneTest);

        It should_report_assembly_loaded = () =>
            output.ShouldContain("Loaded: OneTest");

        It should_report_one_test_found = () =>
            output.ShouldContain(@"1 test(s) loaded.");

        It should_report_one_test_out_of_one_test_passed = () =>
            output.ShouldContain(@"1/1 test(s) passed");

        It should_report_zero_failures = () =>
            output.ShouldContain(@"0 failures");

        It should_not_have_failures_section = () =>
            output.ShouldNotContain("Failures:");

        It should_output_success_value = () =>
            exitCode.ShouldBeTrue();
    }

    [Subject("Acceptance")]
    public class Running_Suite_With_A_Failing_Test : ApplicationSpecs
    {
        Because of = () =>
            exitCode = application.Start(Assemblies.OneFailingTest);

        It should_report_zero_tesst_out_of_one_test_passed = () =>
            output.ShouldContain(@"0/1 test(s) passed");

        It should_report_one_test_out_of_one_failed = () =>
            output.ShouldContain(@"1 failures");

        It should_output_failure_value = () =>
            exitCode.ShouldBeFalse();

        It should_output_failure_message = () =>
            output.ShouldContain("Bogus!");

        It should_output_failure_suite = () =>
            output.ShouldContain("OneFailingTest");

        It should_output_failure_container = () =>
            output.ShouldContain("ContainerFailingTests");

        It should_output_failure_case = () =>
            output.ShouldContain("CaseFailingTest");
    }

    [Subject("Acceptance")]
    public class Running_Suite_With_NoTests : ApplicationSpecs
    {
        Because of = () =>
            exitCode = application.Start(Assemblies.NoTests);

        It should_report_assembly_loaded = () =>
            output.ShouldContain("Loaded: NoTests");

        It should_report_no_tests_found = () =>
            output.ShouldContain("0 test(s) loaded.");

        It should_output_success_value = () =>
            exitCode.ShouldBeTrue();
    }

    public abstract class ApplicationSpecs
    {
        protected static Application application;
        protected static StringWriter outputStream;
        protected static string output { get { return outputStream.ToString(); } }
        protected static bool exitCode;

        private Establish context = () =>
        {
            outputStream = new StringWriter();
            application = new Application(outputStream);
        };
    }

}
