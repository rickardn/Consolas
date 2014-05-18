using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class TokensTests
    {
        [TestCase("-")]
        [TestCase("--")]
        [TestCase("/")]
        public void Prefix_ShouldMatch(string input)
        {
            Tokens.Prefix.IsMatch(input).ShouldBeTrue();
        }

        [TestCase("")]
        [TestCase("---")]
        [TestCase("----")]
        [TestCase("//")]
        [TestCase("///")]
        public void Prefix_ShouldNotMatch(string input)
        {
            Tokens.Prefix.IsMatch(input).ShouldBeFalse();
        }

        [TestCase("=")]
        [TestCase(":")]
        public void Operator_ShouldMatch(string input)
        {
            Tokens.Operator.IsMatch(input).ShouldBeTrue();
        }

        [TestCase("")]
        [TestCase("==")]
        [TestCase("===")]
        [TestCase("::")]
        [TestCase(":::")]
        [TestCase("=:")]
        [TestCase(":=")]
        [TestCase("=:=")]
        [TestCase(":=:")]
        public void Operator_ShouldNotMatch(string input)
        {
            Tokens.Operator.IsMatch(input).ShouldBeFalse();
        }

        [TestCase("-")]
        [TestCase("+")]
        public void BoolOperator_ShouldMatch(string input)
        {
            Tokens.BoolOperator.IsMatch(input).ShouldBeTrue();
        }

        [TestCase("")]
        [TestCase("--")]
        [TestCase("---")]
        [TestCase("++")]
        [TestCase("+++")]
        [TestCase("-+")]
        [TestCase("-+-")]
        [TestCase("+-")]
        [TestCase("+-+")]
        public void BoolOperator_ShouldNotMatch(string input)
        {
            Tokens.BoolOperator.IsMatch(input).ShouldBeFalse();
        }

        [TestCase("a")]
        [TestCase("z")]
        [TestCase("A")]
        [TestCase("Z")]
        [TestCase("aA")]
        [TestCase("aZ")]
        [TestCase("aa")]
        [TestCase("az")]
        [TestCase("a0")]
        [TestCase("a9")]
        [TestCase("Lorem1Ipsum2Dolor345Samit99")]
        public void Name_ShouldMatch(string input)
        {
            Tokens.Name.IsMatch(input).ShouldBeTrue();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("!")]
        [TestCase(":")]
        [TestCase("\\")]
        [TestCase("+")]
        [TestCase("=")]
        [TestCase("-")]
        [TestCase("å")]
        public void Name_ShouldNotMatch(string input)
        {
            Tokens.Name.IsMatch(input).ShouldBeFalse();
        }

        [TestCase("a")]
        [TestCase("abc")]
        [TestCase("x:")]
        [TestCase(".")]
        [TestCase("!")]
        [TestCase("!f")]
        [TestCase(".\\abc.txt")]
        public void Value_ShouldMatch(string input)
        {
            Tokens.Value.IsMatch(input).ShouldBeTrue();
        }

        [TestCase(@"\")]
        [TestCase(@"\")]
        [TestCase("\\a")]
        [TestCase(@"\a")]
        [TestCase(" ")]
        [TestCase(" a")]
        [TestCase("-")]
        [TestCase("-a")]
        [TestCase("/")]
        [TestCase("/a")]
        [TestCase("+")]
        [TestCase("+a")]
        [TestCase("=")]
        [TestCase("=a")]
        [TestCase(":")]
        [TestCase(":a")]
        public void Value_ShouldNotMatch(string input)
        {
            Tokens.Value.IsMatch(input).ShouldBeFalse();
        }

        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("   ")]
        [TestCase("    ")]
        [TestCase("     ")]
        [TestCase("      ")]
        [TestCase("       ")]
        public void WhiteSpace_ShouldMatch(string input)
        {
            Tokens.WhiteSpace.IsMatch(input).ShouldBeTrue();
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("!")]
        [TestCase("\t")]
        public void WhiteSpace_ShouldNotMatch(string input)
        {
            Tokens.WhiteSpace.IsMatch(input).ShouldBeFalse();
        }
    }
}
