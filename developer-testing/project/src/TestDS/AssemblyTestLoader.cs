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
                        .Where(t => t.Name.EndsWith("tests", StringComparison.CurrentCultureIgnoreCase))
                        .Where(t => !t.IsAbstract)
                        .Where(t => !t.IsEnum)
                        .Select(t => new ClassContainer(t))
                   };
        }
    }

    public class ClassContainer : ITestContainer
    {
        private readonly Type _type;

        public ClassContainer(Type type)
        {
            _type = type;
        }

        public RunResult Run()
        {
            return new RunResult();
        }

        public string Name
        {
            get { return _type.Name; }
        }
    }
}