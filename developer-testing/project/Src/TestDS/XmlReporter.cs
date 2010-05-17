using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TestDS
{
    public class XmlReporter : IReporter
    {
        private readonly Func<TextWriter> _writerCreator;

        public XmlReporter(Func<TextWriter> writerCreator)
        {
            _writerCreator = writerCreator;
        }

        public void Report(IEnumerable<SuiteRunResult> executed)
        {
            var report = new XElement("Report");

            var summary = MakeSummary(
                executed.Select(x => x.Name),
                executed.Sum(x => x.Passes),
                executed.Sum(x => x.Failures));
            
            var suites = executed.Select(MakeSuite);

            report.Add(summary);
            report.Add(suites.ToArray());
            using (var writer = _writerCreator())
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings(){CloseOutput = true, Indent = true}))
            {
                report.Save(xmlWriter);
            }
        }

        private XElement MakeSummary(IEnumerable<string> names, int passes, int failures)
        {
            var summary = new XElement("Summary");
            summary.Add(names.Select(x => new XElement("Loaded", x)).ToArray());
            summary.Add(new XAttribute("passes", passes));
            summary.Add(new XAttribute("failures", failures));

            return summary;
        }

        private XElement MakeSuite(SuiteRunResult result)
        {
            var suite = new XElement("Suite");
            suite.Add(new XAttribute("name", result.Name));
            suite.Add(new XAttribute("passes", result.Passes));
            suite.Add(new XAttribute("failures", result.Failures));
            suite.Add(result.Results.Select(MakeContainer).ToArray());

            return suite;
        }

        private XElement MakeContainer(ContainerRunResult result)
        {
            var container = new XElement("Container");
            container.Add(new XAttribute("name", result.Name));
            container.Add(new XAttribute("passes", result.Passes));
            container.Add(new XAttribute("failures", result.Failures));
            container.Add(result.Results.Select(MakeTestCase).ToArray());

            return container;
        }

        private XElement MakeTestCase(TestCaseResult result)
        {
            var testCase = new XElement("TestCase");
            testCase.Add(new XAttribute("name", result.Name));
            testCase.Add(new XAttribute("status", result.Pass ? "passed" : "failed"));
            if (!result.Pass)
                testCase.Add(new XAttribute("message", result.Message));

            return testCase;
        }
    }
}