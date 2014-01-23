using System;
using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class ArgumentLL2ParserTests
    {
        private ArgumentLL2Parser _parser;

        [SetUp]
        public void BeforeEach()
        {
            _parser = new ArgumentLL2Parser();
        }

        [Test]
        public void Parse_Null_ReturnsEmptySet()
        {
            var result = Parse(null);
            result.ShouldBeEmpty();
        }

        [Test]
        public void Parse_EmptyArray_ReturnsEmptySet()
        {
            var result = Parse(new string[0]);
            result.ShouldBeEmpty();
        }

        [Test]
        public void Parse_SingleArg_ReturnsSingleArgument()
        {
            var result = Parse(new[] {"arg"});
            result["arg"].IsMatch.ShouldBeTrue();
            result["arg"].IsDefault.ShouldBeTrue();
            result.Count.ShouldEqual(1);
        }

        [Test]
        public void Parse_MultipleArgs_ReturnsMultipleArguments()
        {
            var result = Parse(new[] { "arg1", "arg2" });
            result["arg1"].IsMatch.ShouldBeTrue();
            result["arg2"].IsMatch.ShouldBeTrue();
        }

        [Test]
        public void Parse_NameValuePair_ReturnsSingleArgument()
        {
            var result = Parse(new[] { "-arg", "val" });
            result["arg"].IsMatch.ShouldBeTrue();
            result["arg"].Value.ShouldEqual("val");
            result["arg"].IsDefault.ShouldBeFalse();
            result.Count.ShouldEqual(1);
        }

        [Test]
        public void Parse_NameValuePair_ReturnsExactlySingleArgument()
        {
            var result = Parse(new[] { "-arg", "val" });
            result.Count.ShouldEqual(1);
        }

        [Test]
        public void Parse_NameValuePairs_ReturnsMultipleArguments()
        {
            var result = Parse(new[] { "-arg1", "val1", "-arg2", "val2" });
            result["arg1"].Value.ShouldEqual("val1");
            result["arg2"].Value.ShouldEqual("val2");
        }

        [Test]
        public void Parse_NameValuePairs_ReturnsExacltyMultipleArguments()
        {
            var result = Parse(new[] { "-arg1", "val1", "-arg2", "val2" });
            result.Count.ShouldEqual(2);
        }

        [Test]
        public void Parse_ComboOfArgAndNameValuePair_ReturnsMultipleArguments()
        {
            var result = Parse(new[] { "val1", "-arg1", "val2" });
            result["val1"].IsMatch.ShouldBeTrue();
            result["arg1"].Value.ShouldEqual("val2");
            result.Count.ShouldEqual(2);
        }

        [Test]
        public void Parse_BooleanOption_ReturnsTrueArgument()
        {
            var result = Parse(new[] {"-bool"});
            result["bool"].Value.ShouldEqual("True");
            result.Count.ShouldEqual(1);
        }

        [Test]
        public void Parse_MultipleBooleanOptions_ReturnsTrueArguments()
        {
            var result = Parse(new[] { "-bool1", "-bool2" });
            result["bool2"].Value.ShouldEqual("True");
            result["bool1"].Value.ShouldEqual("True");
            result.Count.ShouldEqual(2);
        }

        [Test]
        public void Parse_BooleanNegativeOption_ReturnsFalseArgument()
        {
            var result = Parse(new[] { "-bool", "false" });
            result["bool"].Value.ShouldEqual("false");
            result.Count.ShouldEqual(1);
        }

        [Test]
        public void Parse_AltPrefix_ReturnsArgument()
        {
            var result = Parse(new[] {"-foo", "--bar", "/baz"});
            result["foo"].Value.ShouldEqual("True");
            result["bar"].Value.ShouldEqual("True");
            result["baz"].Value.ShouldEqual("True");
            result.Count.ShouldEqual(3);
        }

        [Test]
        public void Parse_AltOperator_ReturnsArgument()
        {
            var result = Parse(new[] {"-name:value", "-foo=bar"});
            result["name"].Value.ShouldEqual("value");
            result["foo"].Value.ShouldEqual("bar");
            result.Count.ShouldEqual(2);
        }

        [TestCase("-name::value")]
        [TestCase("-name==value")]
        [TestCase("-name:=value")]
        [TestCase("-name=:value")]
        public void Parse_MoreThanOneOperator_ThrowsException(string arg)
        {
            Assert.Throws<Exception>(() => Parse(new[] {arg}));
        }

        [TestCase("-name:")]
        [TestCase("-name=")]
        public void Parse_OperatorEmptyValue_ReturnsArgument(string arg)
        {
            var result = Parse(new[] {arg});
            result["name"].Value.ShouldEqual("");
            result.Count.ShouldEqual(1);
        }

        [Test]
        public void Parse_OperatorEmptyValue2_ReturnsArgument()
        {
            var result = Parse(new[] { "-name=", "foo" });
            result["name"].Value.ShouldEqual("");
            result["foo"].Value.ShouldEqual("foo");
            result["foo"].IsDefault.ShouldBeTrue();
            result.Count.ShouldEqual(2);
        }

        [TestCase(new[] { "-name=bar", "foo" }, null)]
        [TestCase(new[] { "foo", "-name=bar" }, null)]
        [TestCase(new[] { "baz", "-name=bar", "foo" }, null)]
        public void Parse_ValueOption_ReturnsArgument(string[] args, string x)
        {
            var result = Parse(args);
            result["foo"].Value.ShouldEqual("foo");
            result["foo"].IsDefault.ShouldBeTrue();
        }

        [TestCase("-")]
        [TestCase("/")]
        public void Parse_InvalidValue1_ThrowsException(string value)
        {
            Assert.Throws<Exception>(() => Parse(new[] { "-name", value }));
        }

        [TestCase("=")]
        [TestCase(":")]
        [TestCase("+")]
        [TestCase(" ")]
        [TestCase("-/+=:")]
        public void Parse_InvalidValue2_ThrowsException(string value)
        {
            Assert.Throws<Exception>(() => Parse(new[] {"-name", value}));
        }

        [Test]
        public void Parse_BooleanPlusOperator_ReturnsArgument()
        {
            var result = Parse(new[] {"-bool+"});
            result.Count.ShouldEqual(1);
            result["bool"].Value.ShouldEqual("True");
        }

        [Test]
        public void Parse_BooleanMinusOperator_ReturnsArgument()
        {
            var result = Parse(new[] { "-bool-" });
            result.Count.ShouldEqual(1);
            result["bool"].Value.ShouldEqual("False");
        }

        [TestCase(new[] { "--/name", "value" }, null)]
        [TestCase(new[] { "---name", "value" }, null)]
        [TestCase(new[] { "--+name", "value" }, null)]
        [TestCase(new[] { "-+name", "value" }, null)]
        [TestCase(new[] { "/-name", "value" }, null)]
        [TestCase(new[] { "//name", "value" }, null)]
        [TestCase(new[] { "-name!", "value" }, null)]
        [TestCase(new[] { "-name ", "value" }, null)]
        [TestCase(new[] { "- name", "value" }, null)]
        [TestCase(new[] { " name", "value" }, null)]
        [TestCase(new[] { "-name_", "value" }, null)]
        [TestCase(new[] { "-_name", "value" }, null)]
        public void Parse_InvalidName_ThrowsException(string[] args, string s)
        {
            Assert.Throws<Exception>(() => Parse(args));
        }

        private ArgumentSet Parse(string[] args)
        {
            return _parser.Parse(args, new ArgumentSet());
        }
    }
}
