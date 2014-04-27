namespace Consolas.Core.Tests
{
    public interface ITestService
    {
         
    }

    public class TestService : ITestService
    {
    }

    public class TestContainer
    {
        public ITestService TestService { get; set; }
    }

    public class TestContainerDerivative : TestContainer
    {
    }
}