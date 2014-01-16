using NUnit.Framework;
using Should;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ArgumentParserTests
    {
        private ArgumentParser _parser;

        [SetUp]
        public void BeforeEach()
        {
            _parser = new ArgumentParser();
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
        }

        [Test]
        public void Parse_SingleArg_ReturnsSingleArgument2()
        {
            var result = Parse(new[] { "arg" });
            result.Count.ShouldEqual(1);
        }

        [Test]
        public void Parse_MultipleArgs_ReturnsMultipleArguments()
        {
            var result = Parse(new[] {"arg1", "arg2"});
            result["arg1"].IsMatch.ShouldBeTrue();
            result["arg2"].IsMatch.ShouldBeTrue();
        }

        [Test]
        public void Parse_MultipleArgs_ReturnsExactlySameNumberOfArguments()
        {
            var result = Parse(new[] { "arg1", "arg2" });
            result.Count.ShouldEqual(2);
        }

        [Test]
        public void Parse_NameValuePair_ReturnsSingleArgument()
        {
            var result = Parse(new[] {"-arg", "val"});
            result["-arg"].IsMatch.ShouldBeTrue();
            result["-arg"].Value.ShouldEqual("val");
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
            result["-arg1"].Value.ShouldEqual("val1");
            result["-arg2"].Value.ShouldEqual("val2");
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
            var result = Parse(new[] {"val1", "-arg1", "val2"});
            result["val1"].IsMatch.ShouldBeTrue();
            result["-arg1"].Value.ShouldEqual("val2");
        }

        private ArgumentSet Parse(string[] args)
        {
            return _parser.Parse(args, new ArgumentSet());
        }
    }
}
