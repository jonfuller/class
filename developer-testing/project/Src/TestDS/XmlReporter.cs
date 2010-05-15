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
            var summary = new XElement("Summary");
            summary.Add(executed.Select(x => new XElement("Loaded", x.Name)).ToArray());
            summary.Add(new XAttribute("passes", executed.Sum(x => x.Passes)));
            summary.Add(new XAttribute("failures", executed.Sum(x => x.Failures)));

            report.Add(summary);
            Document = new XDocument(report);
        }
    }
}