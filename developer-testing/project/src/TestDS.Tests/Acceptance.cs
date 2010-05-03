using System;
using System.IO;
using Machine.Specifications;

namespace TestDS.Tests
{
    [Subject("The Thing")]
    public class Acceptance
    {
        static Exception Exception;

        Because of = () =>
            Exception = Catch.Exception(() => { throw new Exception(); });

        It should_fail = () =>
            Exception.ShouldBeOfType<Exception>();
    }
}
