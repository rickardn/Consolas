using System.Linq;
using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class ArgumentTypeCollectionTests
    {
        [Test]
        public void MethodUnderTest_Scenario_ExpectedBehavior()
        {
            var collection = new ArgumentTypeCollection();
            collection.Add<string>();

            collection.Single().ShouldEqual(typeof(string));
        }
    }
}