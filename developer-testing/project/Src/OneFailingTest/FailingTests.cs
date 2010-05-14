using TestDS;

namespace OneFailingTest
{
    public class ContainerFailingTests
    {
        public void CaseFailingTest()
        {
            throw new AssertionException("Bogus!");
        }
    }
}
