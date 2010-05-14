namespace SomeTests
{
    public abstract class AbstractClassTests
    {
    }

    public interface InterfaceTests
    {
    }

    class NonPublicTests
    {
    }

    public enum EnumTests
    {
        Value
    }

    public class OkTests
    {

    }

    public class NonDefaultCtorTests
    {
        public NonDefaultCtorTests(int number)
        {
        }
    }

    public class DefaultAndNonDefaultCtorTests
    {
        public DefaultAndNonDefaultCtorTests()
        {
        }

        public DefaultAndNonDefaultCtorTests(int number)
        {
        }
    }
}
