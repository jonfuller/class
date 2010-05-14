using System;
using System.Linq;
using Machine.Specifications;

namespace TestDS.Tests
{
    [Subject("Class Container")]
    public class loading_a_class : ClassContainerSpecs
    {
        It should_load_methods_that_end_in_tests = () => {
            TheContainer.TestCases.All(test => test.Name.ToLower().EndsWith("test"));
            TheContainer.TestCases.Count().ShouldBeGreaterThan(0);
        };

        It should_load_methods_that_are_public = () =>
            TheContainer.TestCases.None(test => test.Name.ToLower() == "NonPublicTest").ShouldBeTrue();

        It should_load_methods_that_are_void = () =>
            TheContainer.TestCases.None(test => test.Name == "ReturnsIntTest").ShouldBeTrue();

        It should_load_methods_that_have_no_arguments = () =>
            TheContainer.TestCases.None(test => test.Name == "HasArgumentsTest").ShouldBeTrue();

        It should_not_load_methods_that_have_generic_arguments = () =>
            TheContainer.TestCases.None(test => test.Name == "GenericTest").ShouldBeTrue();
    }

    [Subject("Class Container")]
    public class running_a_passing_class : ClassContainerSpecs
    {
        static RunResult result;

        Because of = () =>
            result = new ClassContainer(typeof(PassingContainer)).Run();

        It should_result_in_success = () =>
            result.Passes.ShouldEqual(1);

        It should_not_result_in_failure = () =>
            result.Failures.ShouldEqual(0);

        private class PassingContainer
        {
            public void SomethingTest()
            {
            }
        }
    }

    [Subject("Class Container")]
    public class running_a_failing_class : ClassContainerSpecs
    {
        static RunResult result;
        static Exception exception;

        Because of = ()=>
            exception = Catch.Exception(() => result = new ClassContainer(typeof(FailingContainer)).Run());

        It should_not_throw = () =>
            exception.ShouldBeNull();

        It should_result_in_failure = () =>
            result.Failures.ShouldEqual(1);

        It should_not_result_in_success = () =>
            result.Passes.ShouldEqual(0);

        private class FailingContainer
        {
            public void FailingTest()
            {
                throw new AssertionException();
            }
        }
}

    public abstract class ClassContainerSpecs
    {
        protected static ClassContainer TheContainer;

        Establish context = () =>
            TheContainer = new ClassContainer(typeof(SampleContainer));

        internal class SampleContainer
        {
            public void WhateverTest()
            {
            }

            void NonPublicTest()
            {
            }

            public int ReturnsIntTest()
            {
                return 1;
            }

            public void HasArgumentsTest(int arg)
            {
            }

            public void GenericTest<T>()
            {
            }
        }
    }

}
