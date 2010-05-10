namespace TestDS
{
    public interface ITestCase
    {
        bool Run();
        string Name { get; }
    }
}