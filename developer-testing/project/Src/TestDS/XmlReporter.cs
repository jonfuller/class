using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TestDS
{
    public class XmlReporter : IReporter
    {
        public XDocument Document { get; private set; }

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
            Document = new XDocument(report);
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
            testCase.Add(new XAttribute("status", result.Pass));
            if (!result.Pass)
                testCase.Add(new XAttribute("message", result.Message));

            return testCase;
        }
    }
}