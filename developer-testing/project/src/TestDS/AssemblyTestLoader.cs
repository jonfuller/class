using System;
using System.Linq;
using System.Reflection;

namespace TestDS
{
    public class AssemblyTestLoader
    {
        public AssemblyTestLoader()
        {
        }

        public TestSuite Load(string assemblyName)
        {
            var asm = Assembly.LoadFile(assemblyName);

            return new TestSuite()
                   {
                       TestContainers = asm
                        .GetTypes()
                        .Where(t => t.Name.EndsWith("tests", StringComparison.InvariantCultureIgnoreCase))
                        .Where(t => !t.IsAbstract)
                        .Where(t => !t.IsEnum)
                        .Select(t => new ClassContainer(t))
                   };
        }
    }
}