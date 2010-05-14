using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestDS
{
    public class AssemblyTestLoader
    {
        public IEnumerable<TestSuite> Load(IEnumerable<string> assemblyNames)
        {
            return assemblyNames
                .Select(Assembly.LoadFrom)
                .Select(asm =>
                    new TestSuite()
                    {
                        Name = asm.GetName().Name,
                        TestContainers = asm
                            .GetTypes()
                            .Where(t => t.Name.EndsWith("tests", StringComparison.InvariantCultureIgnoreCase))
                            .Where(t => !t.IsAbstract)
                            .Where(t => !t.IsEnum)
                            .Where(t => t.GetConstructor(new Type[] { }) != null)
                            .Select(t => new ClassContainer(t))
                    })
                .Eval()
                ;
        }
    }
}