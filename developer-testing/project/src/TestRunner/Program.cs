using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TestDS;

namespace Runner
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Application(Console.Out, args).Start()
                ? 0
                : 1;
        }
    }

    public class Application
    {
        private readonly TextWriter _output;
        private readonly string _assemblyName;

        public Application(TextWriter output, string[] assemblyName)
        {
            _output = output;
            _assemblyName = assemblyName.First();
        }

        public bool Start()
        {
            var loader = new AssemblyTestLoader();
            _output.WriteLine("Loaded: {0}".FormatWith(_assemblyName));
            _output.WriteLine("  No tests loaded.");
            _output.Flush();
            return true;
        }
    }
}
