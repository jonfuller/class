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

    [Subject("String Contains Assertions")]
    public class asserting_string_contains_when_it_does : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldContain("mama mia", "mama"));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }


    [Subject("String Contains Assertions")]
    public class asserting_string_contains_when_it_does_not : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldContain("mama mia", "meatball"));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("<mama mia> was expected to contain <meatball>, but didn't.");
    }


    [Subject("String Does Not Contain Assertions")]
    public class asserting_string_doest_not_contain_when_it_doesnt : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldNotContain("mama mia", "meatball"));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }


    [Subject("String Does Not Contain Assertions")]
    public class asserting_string_does_not_contain_when_it_does : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldNotContain("mama mia", "mama"));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("<mama mia> was expected to NOT contain <mama>, but did.");
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
