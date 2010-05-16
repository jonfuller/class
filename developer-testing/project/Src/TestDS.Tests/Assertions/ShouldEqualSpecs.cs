using System;
using Machine.Specifications;
using AssertExt = TestDS.Assertion.AssertExt;

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

    [Subject("Collection is Empty Assertions")]
    public class asserting_collection_is_empty_when_it_is : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeEmpty(new object[0]));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Collection is Empty Assertions")]
    public class asserting_collection_is_empty_when_it_is_not : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeEmpty(new object[] { "hamburger" }));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Wasn't empty.  Contained 1 item(s): hamburger.");
    }

    [Subject("Collection is Not Empty Assertions")]
    public class asserting_collection_is_not_empty_when_has_items : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldNotBeEmpty(new object[] { "water" }));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Collection is Not Empty Assertions")]
    public class asserting_collection_is_not_empty_when_it_is_empty : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldNotBeEmpty(new object[0]));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Was empty, but should not have been.");
    }

    [Subject("Collection All Should Match Assertions")]
    public class asserting_collection_items_all_match_predicate_when_they_do : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.AllShould(new[] { "water", "beer" }, x => x.EndsWith("r")));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Collection All Should Match Assertions")]
    public class asserting_collection_items_all_match_predicate_when_they_do_not : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.AllShould(new[] { "coffee", "tea" }, x => x.EndsWith("e")));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("1 item didn't match: tea");
    }

    [Subject("Collection None Should Match Assertions")]
    public class asserting_collection_items_none_match_predicate_when_none_do : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.NoneShould(new[] { "water", "beer" }, x => x == "coffee"));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Collection None Should Match Assertions")]
    public class asserting_collection_items_none_match_predicate_when_some_do : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.NoneShould(new[] { "coffee", "tea" }, x => x.StartsWith("c")));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("1 item matched: coffee");
    }

    [Subject("Object Type Assertions")]
    public class item_should_be_of_type_and_is_of_type : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeA<DateTime>(DateTime.Now));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Object Type Assertions")]
    public class item_should_be_of_type_and_is_not_of_type : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeA<DateTime>(StringComparer.Ordinal));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected <DateTime> but was <OrdinalComparer>");
    }

    [Subject("Object Type Assertions")]
    public class item_should_not_be_of_type_and_is_not_of_type : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldNotBeA<DateTime>(new InvalidProgramException()));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Object Type Assertions")]
    public class item_should_not_be_of_type_and_is_of_type : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldNotBeA<DateTime>(DateTime.MinValue));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected to NOT be a <DateTime>, but was.");
    }


    [Subject("Boolean Assertions")]
    public class collection_should_be_true_when_true : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeTrue(true));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Collection Assertions")]
    public class collection_should_be_true_but_false : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeTrue(false));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected to be true, but was false.");
    }

    [Subject("Boolean Assertions")]
    public class collection_should_be_false_when_false : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeFalse(false));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Collection Assertions")]
    public class collection_should_be_false_but_true : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeFalse(true));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected to be false, but was true.");
    }

    [Subject("Number Assertions")]
    public class collection_should_be_one_when_one : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeOne(1));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Number Assertions")]
    public class collection_should_be_one_when_not_one : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeOne(0));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected to be 1, but was 0.");
    }

    [Subject("Number Assertions")]
    public class collection_should_be_zero_when_zero : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeZero(0));

        It should_pass = () =>
            passed.ShouldBeTrue();
    }

    [Subject("Number Assertions")]
    public class collection_should_be_zero_when_not_zero : AssertionSpecs
    {
        Because of = () =>
            RunAssertion(() => AssertExt.ShouldBeZero(1));

        It should_fail = () =>
            passed.ShouldBeFalse();

        It should_show_expected_and_actual_values = () =>
            message.ShouldContain("Expected to be 0, but was 1.");
    }

    // ability to specify output from command line

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
