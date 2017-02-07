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
            Tokens.AssignmentOperator.IsMatch(input).ShouldBeTrue();
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
            Tokens.AssignmentOperator.IsMatch(input).ShouldBeFalse();
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
        [TestCase("text/xml")]
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

        [TestCase("a")]
        [TestCase("b")]
        [TestCase("A")]
        [TestCase("Abc")]
        [TestCase("A0b1c2")]
        [TestCase("a+")]
        [TestCase("A+")]
        [TestCase("Ab+")]
        [TestCase("Abc+")]
        [TestCase("A0b1c2+")]
        [TestCase("a-")]
        [TestCase("A-")]
        [TestCase("Ab-")]
        [TestCase("Abc-")]
        [TestCase("A0b1c2-")]
        public void BooleanStatement_ShouldMatch(string input) 
        {
            Tokens.BooleanStatement.IsMatch(input).ShouldBeTrue();
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
        public void BooleanStatement_ShoulNoydMatch(string input)
        {
            Tokens.BooleanStatement.IsMatch(input).ShouldBeFalse();
        }

        [TestCase("A0b1c2d3=!##324%&/")]
        [TestCase("A0b1c2d3==!##324%&/")]
        [TestCase("A0b1c2d3:!##324%&/")]
        [TestCase("A0b1c2d3::!##324%&/")]
        public void AssignmentStatement_ShouldMatch(string input)
        {
            Tokens.AssigmentStatement.IsMatch(input).ShouldBeTrue();
        }
    }
}
