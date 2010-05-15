using System;
using Machine.Specifications;
using TestDS.Assertion;

namespace TestDS.Tests.Assertions
{
    [Subject("Equality Assertions")]
    public class comparing_equal_integers : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldEqual(1, 1));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Equality Assertions")]
    public class comparing_nonequal_integers : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldEqual(1, 2));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected <2> but got <1>");
    }

    [Subject("Non-Nullity Assertions")]
    public class asserting_null_value_as_not_null : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldNotBeNull(null));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected <not null> but got <null>");
    }

    [Subject("Non-Nullity Assertions")]
    public class asserting_value_as_not_null : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldNotBeNull(3));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }


    [Subject("Nullity Assertions")]
    public class asserting_null_value_as_null : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeNull(null));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Nullity Assertions")]
    public class asserting_value_as_null : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeNull(3));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected <null> but got <3>");
    }

    public abstract class AssertionSpecs
    {
        protected static string message { get { return thrown.Message; } }
        protected static bool passed { get { return thrown == null; } }

        static Exception thrown;

        protected static void RunAssertion(Action assertion)
        {
            thrown = Catch.Exception(assertion);
        }
    }
}
