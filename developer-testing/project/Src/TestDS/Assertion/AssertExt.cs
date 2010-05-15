using System;

namespace TestDS.Assertion
{
    public static class AssertExt
    {
        public static void ShouldEqual<T>(this T actual, T expected)
        {
            if (actual.Equals(expected))
                return;
            throw new AssertionException("Not Equal.  Expected <{0}> but got <{1}>".FormatWith(expected, actual));
        }

        public static void ShouldNotBeNull(this object actual)
        {
            if (actual != null)
                return;
            throw new AssertionException("Is null.  Expected <not null> but got <null>");
        }

        public static void ShouldBeNull(this object actual)
        {
            if (actual == null)
                return;
            throw new AssertionException("Is not null.  Expected <null> but got <{0}>".FormatWith(actual));
        }
    }
}
