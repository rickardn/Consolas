using NUnit.Framework;
using Should;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ArgumentSerializerTests_SerializeToStringOfT
    {
        private ArgumentSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ArgumentSerializer
            {
                Prefixes = new [] {"-"}
            };
        }

        [Test]
        public void SingleParameter_ReturnsParameter()
        {
            _serializer.SerializeToString<SingleParameter>().ShouldEqual("-Parameter");
        }

        [Test]
        public void TwoParameters_ReturnsParameters()
        {
            _serializer.SerializeToString<TwoParameters>().ShouldEqual("-Parameter1 -Parameter2");
        }

        [Test]
        public void BooleanParameter_ReturnsParameter()
        {
            _serializer.SerializeToString<BooleanParameter>().ShouldEqual("-IsTrue");
        }
    }

    public class SingleParameter
    {
        public string Parameter { get; set; }
    }

    public class TwoParameters
    {
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
    }

    public class BooleanParameter
    {
        public bool IsTrue { get; set; } 
    }

    public class MultiTypeParameter
    {
        public bool Bool { get; set; }
        public string String { get; set; }
    }

}
