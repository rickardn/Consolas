using Consolas.Core.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class AcitvatorCommandFactoryTests
    {
        [Test]
        public void CreateInstance()
        {
            var factory = new ActivatorCommandFactory();
            var instance = factory.CreateInstance(typeof (SimpleCommand));
            instance.ShouldBeType<SimpleCommand>();
            instance.ShouldNotBeNull();
        }
    }
}
