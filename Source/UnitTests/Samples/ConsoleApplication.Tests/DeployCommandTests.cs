using NUnit.Framework;
using SampleConsoleApplication.Commands;

namespace SampleConsoleApplication.Tests
{
    [TestFixture]
    public class DeployCommandTests
    {
        [Test]
        public void ExpectedBehavior()
        {
            var command = new DeployCommand();
            var args = new DeployArgs();
            
            var result = command.Execute(args);

            StringAssert.AreEqualIgnoringCase(" ", result);
        }
    }
}
