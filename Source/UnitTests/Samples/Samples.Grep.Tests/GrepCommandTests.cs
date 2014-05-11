using NUnit.Framework;
using Samples.Grep.Args;
using Samples.Grep.Commands;

namespace Samples.Grep.Tests
{
    [TestFixture]
    public class GrepCommandTests
    {
        [Test]
        public void Execute_ValidArgument_ReturnsGrepedText()
        {
            var command = new GrepCommand();

            var result = (string) command.Execute(new GrepArgs
            {
                FileName = "doc.txt",
                Regex = "foo"
            });
             
            StringAssert.Contains("foo bar baz", result);
        }
    }
}
