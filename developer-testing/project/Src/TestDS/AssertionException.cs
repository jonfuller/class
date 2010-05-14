using System;

namespace TestDS
{
    public class AssertionException : Exception
    {
        public AssertionException(string message)
            : base(message)
        {
        }
    }
}