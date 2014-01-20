using NUnit.Framework;
using Should;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class StringTokenizerWhiteSpaceTests
    {
        [Test]
        public void ExpectedBehavior()
        {
            var tokenizer = new StringTokenizer
            {
                Operators = new[] {" ", "--", "-"}
            };

            var result = tokenizer.Tokenize("-foo- -foo");
            result.ShouldEqual(new[] {"-", "foo", "-", " ", "-", "foo"});
        }
    }
}
