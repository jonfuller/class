﻿using System;
using System.Reflection;

namespace TestDS
{
    public class ClassTestCase : ITestCase
    {
        private readonly Type _type;
        private readonly MethodInfo _methodInfo;

        public ClassTestCase(Type type, MethodInfo methodInfo)
        {
            _type = type;
            _methodInfo = methodInfo;
        }

        public TestCaseResult Run()
        {
            try
            {
                var testObj = Activator.CreateInstance(_type);
                _methodInfo.Invoke(testObj, new object[] { });
                return new TestCaseResult(){Name=Name, Pass = true};
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null && e.InnerException is AssertionException)
                    return new TestCaseResult(){Name = Name, Pass = false, Message = e.InnerException.Message};
                throw;
            }
        }

        public string Name
        {
            get { return _methodInfo.Name; }
        }
    }
}