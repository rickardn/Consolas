using NUnit.Framework;
using Should;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ArgumentSerializerTests_SerializeToStringArgs
    {
        private ArgumentSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ArgumentSerializer
            {
                Prefixes = new[] {"-"}
            };
        }

        [Test]
        public void SerializeToString_Empty_ReturnsEmptyString()
        {
            SerializeToString(new string[0]).ShouldEqual("");
        }

        [Test]
        public void SerializeToString_SingleArgWithPrefix_ReturnsArg()
        {
            SerializeToString(new[] {"-foo"}).ShouldEqual("-foo");
        }

        [TestCase(new[] {"-foo","-bar"}, "-foo -bar")]
        [TestCase(new[] {"-foo","-bar","-baz"}, "-foo -bar -baz")]
        [TestCase(new[] {"-foo","-bar","-baz"}, "-foo -bar -baz")]
        public void SerializeToString_MultipleArgsWithPrefix_ReturnsPrefixedArgsSeparatedBySingleSpace(string[] args, string expected)
        {
            SerializeToString(args).ShouldEqual(expected);
        }

        [Test]
        public void ArgWithValue_ReturnesArgWithoutValue()
        {
            SerializeToString(new[] {"-foo", "value"}).ShouldEqual("-foo");
        }

        [TestCase(new[] {"foo", "bar"}, "")]
        [TestCase(new[] {"-foo", "bar"}, "-foo")]
        [TestCase(new[] {"-foo", "-bar", "baz"}, "-foo -bar")]
        [TestCase(new[] {"-foo", "bar", "baz"}, "-foo")]
        [TestCase(new[] {"foo", "-bar", "baz"}, "-bar")]
        [TestCase(new[] {"foo", "bar", "-baz"}, "-baz")]
        public void MultipleArgsWithPrefixAndValue_ReturnsPrefixedArgsWithoutvalues(string[] args, string expected)
        {
            SerializeToString(args).ShouldEqual(expected);
        }

        private string SerializeToString(string[] args)
        {
            return _serializer.SerializeToString(args);
        }
    }
}
