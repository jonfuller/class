using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDS.Assertion
{
    public static class AssertExt
    {
        public static void ShouldEqual<T>(this T actual, T expected)
        {
            if (actual.Equals(expected))
                return;
            throw new AssertionException("Not Equal.  Expected <{0}> but got <{1}>.".FormatWith(expected, actual));
        }

        public static void ShouldNotBeNull(this object actual)
        {
            if (actual != null)
                return;
            throw new AssertionException("Is null.  Expected <not null> but got <null>.");
        }

        public static void ShouldBeNull(this object actual)
        {
            if (actual == null)
                return;
            throw new AssertionException("Is not null.  Expected <null> but got <{0}>.".FormatWith(actual));
        }

        public static void ShouldContain(string actual, string substring)
        {
            if (actual.Contains(substring))
                return;
            throw new AssertionException("<{0}> was expected to contain <{1}>, but didn't.".FormatWith(actual, substring));
        }

        public static void ShouldNotContain(string actual, string notSubstring)
        {
            if (!actual.Contains(notSubstring))
                return;
            throw new AssertionException("<{0}> was expected to NOT contain <{1}>, but did.".FormatWith(actual, notSubstring));
        }

        public static void ShouldBeEmpty<T>(this IEnumerable<T> target)
        {
            if (target.Count() == 0)
                return;
            throw new AssertionException("Wasn't empty.  Contained {0} item(s): {1}.".FormatWith(
                target.Count(),
                target.Select(x => x.ToString()).Join(", ")));
        }

        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> target)
        {
            if (target.Count() > 0)
                return;
            throw new AssertionException("Was empty, but should not have been.");
        }

        public static void AllShould<T>(this IEnumerable<T> target, Func<T, bool> predicate)
        {
            if (target.All(predicate))
                return;

            var failers = target.Where(x => !predicate(x));
            throw new AssertionException("{0} {1} didn't match: {2}".FormatWith(
                failers.Count(),
                "item".Pluralize(failers.Count()),
                failers.Select(f => f.ToString()).Join(", ")));
        }

        public static void NoneShould<T>(this IEnumerable<T> target, Func<T, bool> predicate)
        {
            if (target.None(predicate))
                return;

            var matchers = target.Where(predicate);
            throw new AssertionException("{0} {1} matched: {2}".FormatWith(
                matchers.Count(),
                "item".Pluralize(matchers.Count()),
                matchers.Select(f => f.ToString()).Join(", ")));
        }

        public static void ShouldBeA<TExpected>(this object actual)
        {
            if (actual is TExpected)
                return;

            throw new AssertionException("Expected <{0}> but was <{1}>".FormatWith(
                typeof(TExpected).Name,
                actual.GetType().Name));
        }

        public static void ShouldNotBeA<TExpected>(this object actual)
        {
            if (!(actual is TExpected))
                return;

            throw new AssertionException("Expected to NOT be a <{0}>, but was.".FormatWith(typeof(TExpected).Name));
        }

        public static void ShouldBeTrue(this bool target)
        {
            if (target)
                return;

            throw new AssertionException("Expected to be true, but was false.");
        }

        public static void ShouldBeFalse(this bool target)
        {
            if (!target)
                return;

            throw new AssertionException("Expected to be false, but was true.");
        }

        public static void ShouldBeZero(this int zero)
        {
            if (zero == 0)
                return;

            throw new AssertionException("Expected to be 0, but was {0}.".FormatWith(zero));
        }

        public static void ShouldBeOne(this int one)
        {
            if (one == 1)
                return;

            throw new AssertionException("Expected to be 1, but was {0}.".FormatWith(one));
        }
    }
}
