using System.IO;
using Machine.Specifications;
using Runner;

namespace TestDS.Tests
{
    [Subject("Acceptance")]
    public class running_with_no_tests
    {
        static Application application;
        static StringWriter outputStream;
        static bool output;

        private Establish context = () =>
                                    {
                                        outputStream = new StringWriter();
                                        application = new Application(outputStream, new[]{"AssemblyWithNoTests.dll"});
                                    };

        Because of = () =>
            output = application.Start();

        It should_report_no_tests_found = () =>
            outputStream.ToString().ShouldEqual(@"Loaded: AssemblyWithNoTests.dll
  No tests loaded.
");

        It should_output_success_value = () =>
            output.ShouldBeTrue();
    }
}
