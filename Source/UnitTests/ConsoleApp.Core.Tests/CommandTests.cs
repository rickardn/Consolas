using ConsoleApp.Core.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void Execute_SimpleExample_ReturnsResult()
        {
            var command = new SimpleCommand();
            var result = command.Execute(new SingleParameter
            {
                Parameter = "foo"
            });

            result.ShouldContain("SimpleCommand");
        }
    }
}