using System.IO;
using Machine.Specifications;
using Runner;

namespace TestDS.Tests
{
    [Subject("Acceptance")]
    public class Running_Suite_With_A_Test : ApplicationSpecs
    {
        Because of = () =>
            exitCode = application.Start();

        It should_report_assembly_loaded = () =>
            output.ShouldContain("Loaded: NoTests.dll");

        It should_report_one_test_found = () =>
            output.ShouldEqual(@"1 test(s) loaded.");

        //It should_output_success_value = () =>
        //    exitCode.ShouldBeTrue();
    }

    [Subject("Acceptance")]
    public class running_with_no_tests : ApplicationSpecs
    {
        Because of = () =>
            exitCode = application.Start();

        It should_report_assembly_loaded = () =>
            output.ShouldContain("Loaded: NoTests.dll");

        It should_report_no_tests_found = () =>
            output.ShouldContain("No tests loaded.");

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
            application = new Application(outputStream, new[] { Assemblies.NoTests });
        };
    }

}
