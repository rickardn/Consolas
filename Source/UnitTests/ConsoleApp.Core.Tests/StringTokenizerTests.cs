using System.Linq;
using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class StringTokenizerTests
    {
        private StringTokenizer _tokenizer;
        private string[] _tokens;

        [SetUp]
        public void Setup()
        {
            _tokenizer = new StringTokenizer();
        }

        private void ShouldTokenize(string text, string[] expected)
        {
            _tokens = _tokenizer.Tokenize(text).ToArray();
            _tokens.ShouldEqual(expected);
        }

        [TestCase("", new string[0])]
        public void Tokenize_Empty_ReturnsEmpty(string text, string[] expected)
        {
            ShouldTokenize(text, expected);
        }

        [TestCase("foo", new[] { "foo" })]
        public void Tokenize_NoOperator_ReturnsToken(string text, string[] expected)
        {
            ShouldTokenize(text, expected);
        }

        [TestCase("--", new[] { "--" })]
        [TestCase("--foo", new[] { "--", "foo" })]
        [TestCase("foo--", new[] { "foo", "--" })]
        [TestCase("foo--bar", new[] { "foo", "--", "bar" })]
        public void Tokenize_SingleMultiCharOperator_ReturnsTokens(string text, string[] expected)
        {
            ShouldTokenize(text, expected);
        }

        [TestCase("----", new[] { "--", "--" })]
        [TestCase("------", new[] { "--", "--", "--" })]
        [TestCase("--------", new[] { "--", "--", "--", "--" })]
        [TestCase("foo----", new[] { "foo", "--", "--" })]
        [TestCase("--foo--", new[] { "--", "foo", "--" })]
        [TestCase("----foo", new[] { "--", "--", "foo" })]
        public void Tokenize_MultipleMultiCharOperators_ReturnsTokens(string text, string[] expected)
        {
            ShouldTokenize(text, expected);
        }

        [TestCase("-", new[] { "-" })]
        [TestCase("-foo", new[] { "-", "foo" })]
        [TestCase("foo-", new[] { "foo", "-" })]
        [TestCase("foo-bar", new[] { "foo", "-", "bar" })]
        
        [TestCase("/", new[] { "/" })]
        [TestCase("/foo", new[] { "/", "foo" })]
        [TestCase("foo/", new[] { "foo", "/" })]
        [TestCase("foo/bar", new[] { "foo", "/", "bar" })]

        [TestCase("+", new[] { "+" })]
        [TestCase("+foo", new[] { "+", "foo" })]
        [TestCase("foo+", new[] { "foo", "+" })]
        [TestCase("foo+bar", new[] { "foo", "+", "bar" })]
        public void Tokenize_SingleOperator_ReturnsTokens(string text, string[] expected)
        {
            ShouldTokenize(text, expected);
        }

        [TestCase("-foo-", new[] { "-", "foo", "-" })]
        [TestCase("//", new[] { "/", "/" })]
        [TestCase("///", new[] { "/", "/", "/" })]
        [TestCase("/foo/", new[] { "/", "foo", "/" })]
        [TestCase("foo//", new[] { "foo", "/", "/" })]
        [TestCase("//foo", new[] { "/", "/", "foo"})]
        public void Tokenize_MultipleOperators_ReturnsTokens(string text, string[] expected)
        {
            ShouldTokenize(text, expected);
        }

        [TestCase("---", new[] { "--", "-" })]
        [TestCase("-----", new[] { "--", "--", "-" })]
        [TestCase("---", new[] { "--", "-" })]
        [TestCase("-/+", new[] { "-", "/", "+" })]
        [TestCase("---/+", new[] { "--", "-", "/", "+" })]
        [TestCase("-/--+", new[] { "-", "/", "--", "+" })]
        [TestCase("-/+--", new[] { "-", "/", "+", "--" })]
        [TestCase("-a/b+c--", new[] { "-", "a", "/", "b", "+", "c", "--" })]
        [TestCase("a-b/c+d--e", new[] { "a", "-", "b", "/", "c", "+", "d", "--", "e" })]
        public void Tokenize_DifferentOperators_ReturnsTokens(string text, string[] expected)
        {
            ShouldTokenize(text, expected);
        }
    }
}
