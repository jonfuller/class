using System.IO;
using System.Linq;
using System.Xml.Linq;
using Machine.Specifications;
using Runner;

namespace TestDS.Tests
{
    [Subject("Acceptance")]
    public class Running_Suite_With_A_Test_With_Xml_Output : ApplicationSpecsXml
    {
        Because of = () =>
            application.Start(Assemblies.OneTest.Concat(Assemblies.OneFailingTest));

        It should_have_loaded_assemblies_in_summary = () => {
            output.Root.Element("Summary").Elements("Loaded").ShouldNotBeEmpty();
            output.Root.Element("Summary").Elements("Loaded").First().Value.ShouldEqual("OneTest");
        };

        It should_have_number_of_passes_in_summary = () =>
            output.Root.Element("Summary").Attribute("passes").Value.ShouldEqual("1");

        It should_have_number_of_failures_in_summary = () =>
            output.Root.Element("Summary").Attribute("failures").Value.ShouldEqual("1");

        It should_have_suite_for_each_assembly;

        It should_have_container_for_each_class_under_assembly_container;

        It should_have_case_for_each_test_method_in_class_under_container;

        It should_have_summary_of_passes_and_failures_for_each_suite;

        It should_have_summary_of_passes_and_failures_for_each_container;

        It should_have_pass_or_fail_status_on_each_test_case;

        It should_have_name_on_each_suite;

        It should_have_name_on_each_container;

        It should_have_name_on_each_test_case;
    }

    [Subject("Acceptance")]
    public class Running_Suite_With_A_Test : ApplicationSpecs
    {
        Because of = () =>
            exitCode = application.Start(Assemblies.OneTest);

        It should_report_assembly_loaded = () =>
            output.ShouldContain("Loaded: OneTest");

        It should_report_one_test_found = () =>
            output.ShouldContain(@"1 test loaded.");

        It should_report_one_test_out_of_one_test_passed = () =>
            output.ShouldContain(@"1/1 test passed");

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

        It should_report_zero_tests_out_of_one_test_passed = () =>
            output.ShouldContain(@"0/1 test passed");

        It should_report_one_test_out_of_one_failed = () =>
            output.ShouldContain(@"1 failure");

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
            output.ShouldContain("0 tests loaded.");

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
            application = new Application(new TextReporter(outputStream));
        };
    }

    public abstract class ApplicationSpecsXml
    {
        protected static Application application;
        protected static XDocument output { get { return reporter.Document; } }
        protected static XmlReporter reporter;

        private Establish context = () =>
        {
            reporter = new XmlReporter();
            application = new Application(reporter);
        };
    }
}
