namespace TestDS
{
    public interface ITestCase
    {
        TestCaseResult Run();
        string Name { get; }
    }
}