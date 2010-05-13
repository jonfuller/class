using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        It should_load_not_load_methods_that_have_generic_arguments = () =>
            TheContainer.TestCases.None(test => test.Name == "GenericTest").ShouldBeTrue();
    }

    public abstract class ClassContainerSpecs
    {
        protected static ClassContainer TheContainer;

        Establish context = () =>
            TheContainer = new ClassContainer(typeof(SampleContainer));
    }

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
