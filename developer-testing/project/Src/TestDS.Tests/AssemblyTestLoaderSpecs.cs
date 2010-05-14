using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Machine.Specifications;

namespace TestDS.Tests
{
    [Subject("Assembly Loader")]
    public class loading_an_assembly : AssemblyTestLoaderSpecs
    {
        Because of = () =>
            loadedSuites = TheLoader.Load(Assemblies.SomeTests);

        It should_load_containers_ending_with_the_word_tests = () => {
            loadedSuite.TestContainers.All(c => c.Name.ToLower().EndsWith("tests"));
            loadedSuite.TestContainers.Count().ShouldBeGreaterThan(0);
        };

        It should_not_load_containers_from_abstract_class = () =>
            loadedSuite.TestContainers.Any(c => c.Name.ToLower().Contains("abstract")).ShouldBeFalse();

        It should_not_load_containers_from_interfaces = () =>
            loadedSuite.TestContainers.Any(c => c.Name.ToLower().Contains("interface")).ShouldBeFalse();

        It should_not_load_containers_from_enums = () =>
            loadedSuite.TestContainers.Any(c => c.Name.ToLower().Contains("enum")).ShouldBeFalse();

        It should_not_load_containers_without_default_constructors = () =>
            loadedSuite.TestContainers.Any(c => c.Name == "NonDefaultCtorTests").ShouldBeFalse();

        It should_load_containers_with_default_and_other_constructors = () =>
            loadedSuite.TestContainers.Any(c => c.Name.ToLower().Contains("defaultandnondefaultctortests")).ShouldBeTrue();
    }

    [Subject("Assembly loader")]
    public class loading_an_assembly_with_no_tests : AssemblyTestLoaderSpecs
    {
        Because of = () =>
            loadedSuites = TheLoader.Load(Assemblies.NoTests);

        It should_load_an_empty_suite = () =>
            loadedSuite.TestContainers.Count().ShouldEqual(0);
    }

    public abstract class AssemblyTestLoaderSpecs
    {
        protected static IEnumerable<TestSuite> loadedSuites;
        protected static TestSuite loadedSuite { get { return loadedSuites.First(); } }
        protected static AssemblyTestLoader TheLoader;

        Establish context = () =>
            TheLoader = new AssemblyTestLoader();
    }

    public static class Assemblies
    {
        private static string[] GetPath(string assemblyName)
        {
            return new[] {Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase),
                assemblyName)
                .Substring(6)};
        }

        public static string[] OneTest
        {
            get { return GetPath("OneTest.dll"); }
        }

        public static string[] SomeTests
        {
            get { return GetPath("SomeTests.dll"); }
        }

        public static string[] NoTests
        {
            get { return GetPath("NoTests.dll"); }
        }

        public static string[] OneFailingTest
        {
            get { return GetPath("OneFailingTest.dll"); }
        }
    }
}
