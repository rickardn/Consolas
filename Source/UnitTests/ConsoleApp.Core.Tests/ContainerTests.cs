using Consolas.Core.Tests.Helpers;
using NUnit.Framework;
using Should;
using SimpleInjector;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        [Test]
        public void RegisterInitializer_BaseClass_RunsInitializerOnSubClass()
        {
            var container = new Container();
            container.Register<ITestService, TestService>();
            container.RegisterInitializer<TestContainer>(c => c.TestService = container.GetInstance<ITestService>());
            
            var testContainer = container.GetInstance<TestContainerDerivative>();
            
            testContainer.ShouldNotBeNull();
            testContainer.TestService.ShouldNotBeNull();
        }
    }
}
