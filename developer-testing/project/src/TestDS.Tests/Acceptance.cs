﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Machine.Specifications;
using Runner;

namespace TestDS.Tests
{
    [Subject("Command Line")]
    public class Command_Line_Parsing
    {
        static Dictionary<string, List<string>> parsed;

        Because of = () =>      
            parsed = Program.ParseArgs(new []
            {
                "-xml", "output.xml",
                "OneTest.dll",
                "SomeTests.dll"
            });

        It should_parse_xml_filename = () =>
            parsed[CmdArgs.XmlFileName].First().ShouldEqual("output.xml");

        It should_parse_multiple_assemblies = () => {
            parsed[CmdArgs.Assemblies][0].ShouldEqual("OneTest.dll");
            parsed[CmdArgs.Assemblies][1].ShouldEqual("SomeTests.dll");
        };
    }

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

        It should_have_suite_for_each_assembly = () =>
            output.Root.Elements("Suite").Count().ShouldEqual(2);

        It should_have_name_on_each_suite = () =>
            output.Root.Elements("Suite").Select(x => x.Attribute("name").Value)
                .ShouldEachConformTo(name => name == "OneTest" || name == "OneFailingTest");

        It should_have_summary_of_passes_for_each_suite = () => {
            output.Root.Elements("Suite").First().Attribute("passes").Value.ShouldEqual("1");
            output.Root.Elements("Suite").Last().Attribute("passes").Value.ShouldEqual("0");
        };

        It should_have_summary_of_failures_for_each_suite = () => {
            output.Root.Elements("Suite").First().Attribute("failures").Value.ShouldEqual("0");
            output.Root.Elements("Suite").Last().Attribute("failures").Value.ShouldEqual("1");
        };

        It should_have_container_for_each_class_under_assembly_container = () =>
            output.Root.Elements("Suite").First().Elements("Container").Count().ShouldEqual(1);

        It should_have_name_on_each_container = () =>
            output.Root.Elements("Suite").First().Elements("Container").First().Attribute("name").Value.ShouldEqual("Tests");

        It should_have_summary_of_passes_for_each_container = () => {
            output.Root.Elements("Suite").First().Element("Container").Attribute("passes").Value.ShouldEqual("1");
            output.Root.Elements("Suite").Last().Element("Container").Attribute("passes").Value.ShouldEqual("0");
        };

        It should_have_summary_of_failures_for_each_container = () => {
            output.Root.Elements("Suite").First().Element("Container").Attribute("failures").Value.ShouldEqual("0");
            output.Root.Elements("Suite").Last().Element("Container").Attribute("failures").Value.ShouldEqual("1");
        };

        It should_have_case_for_each_test_method_in_class_under_container = () => {
            output.Root.Elements("Suite").First().Element("Container").Elements("TestCase").Count().ShouldEqual(1);
            output.Root.Elements("Suite").Last().Element("Container").Elements("TestCase").Count().ShouldEqual(1);
        };

        It should_have_name_on_each_test_case = () => {
            output.Root.Elements("Suite").First().Element("Container").Element("TestCase").Attributes("name").Count().ShouldEqual(1);
            output.Root.Elements("Suite").Last().Element("Container").Element("TestCase").Attributes("name").Count().ShouldEqual(1);
        };

        It should_have_pass_or_fail_status_on_each_test_case = () => {
            output.Root.Elements("Suite").First().Element("Container").Element("TestCase").Attribute("status").Value.ShouldEqual("passed");
            output.Root.Elements("Suite").Last().Element("Container").Element("TestCase").Attribute("status").Value.ShouldEqual("failed");
        };

        It should_have_fail_message_on_each_failing_test_case = () => {
            output.Root.Elements("Suite").First().Element("Container").Element("TestCase").Attributes("message").Count().ShouldEqual(0);
            output.Root.Elements("Suite").Last().Element("Container").Element("TestCase").Attributes("message").Count().ShouldEqual(1);
        };
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
        protected static XDocument output
        {
            get
            {
                using (var stream = new MemoryStream(memory.GetBuffer()))
                using (var reader = new StreamReader(stream))
                    return XDocument.Load(reader);
            }
        }

        protected static XmlReporter reporter;

        private static MemoryStream memory;
        private static StreamWriter writer;

        private Establish context = () =>
        {
            memory = new MemoryStream();
            writer = new StreamWriter(memory);
            reporter = new XmlReporter(() => writer);
            application = new Application(reporter);
        };
    }
}
