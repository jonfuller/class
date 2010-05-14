using TestDS;

namespace OneFailingTest
{
    public class FailingTests
    {
        public void FailingTest()
        {
            throw new AssertionException();
        }
    }
}
