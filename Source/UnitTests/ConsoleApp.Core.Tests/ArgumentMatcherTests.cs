using System;
using System.Collections.Generic;
using ConsoleApp.Core.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ArgumentMatcherTests
    {
        private ArgumentMatcher matcher;

        [SetUp]
        public void Setup()
        {
            matcher = new ArgumentMatcher()
            {
                Types = new List<Type>
                {
                    typeof(TwoParameters), 
                    typeof(SingleParameter), 
                    typeof(BooleanParameter),
                    typeof(MultiTypeParameter),
                    typeof(DefaultParameters)
                },
            };
        }

        private Type Match(string[] args)
        {
            return matcher.Match(args);
        }

        [TestCase(new[] { "-IsTrue" }, typeof(BooleanParameter))]
        [TestCase(new[] { "-Parameter" }, typeof(SingleParameter))]
        [TestCase(new[] { "-Parameter1", "-Parameter2" }, typeof(TwoParameters))]
        [TestCase(new[] { "-Bool", "-String" }, typeof(MultiTypeParameter))]
        public void Match_ShouldMatchExactSignature1(string[] args, Type expectedType)
        {
            Match(args).ShouldEqual(expectedType);
        }

        [TestCase(new[] { "-Parameter2", "-Parameter1" }, typeof(TwoParameters))]
        [TestCase(new[] { "-String", "-Bool" }, typeof(MultiTypeParameter))]
        public void Match_ShouldMatchNonDefaultOrder(string[] args, Type expectedType)
        {
            Match(args).ShouldEqual(expectedType);
        }

        [TestCase(new[] { "-Parameter2" }, typeof(TwoParameters))]
        [TestCase(new[] { "-Parameter1" }, typeof(TwoParameters))]
        [TestCase(new[] { "-String" }, typeof(MultiTypeParameter))]
        [TestCase(new[] { "-Bool" }, typeof(MultiTypeParameter))]
        public void Match_ShouldMatchPartials(string[] args, Type expected)
        {
            Match(args).ShouldEqual(expected);
        }

        [TestCase(new[] { "-Parameter1" }, typeof(TwoParameters))]
        [TestCase(new[] { "/Parameter1" }, typeof(TwoParameters))]
        [TestCase(new[] { "--Parameter1" }, typeof(TwoParameters))]
        public void Match_CommandPrefix(string[] args, Type expected)
        {
            Match(args).ShouldEqual(expected);
        }

        [Test]
        public void Match_NoPrefix()
        {
            Match(new[] {"IsTrue"})
                .ShouldEqual(typeof(BooleanParameter));
        }

        [TestCase(new[] { "-String", "foo" }, typeof(MultiTypeParameter))]
        [TestCase(new[] { "-Bool", "true" }, typeof(MultiTypeParameter))]
        [TestCase(new[] { "-Bool" }, typeof(MultiTypeParameter))]
        public void Match_MatchesValue(string[] args, Type expected)
        {
            Match(args).ShouldEqual(expected);
        }

        [Test]
        public void MatchToObject_MatchesStringValue()
        {
            var args = Args("-Parameter", "value");
            var result = matcher.MatchToObject<SingleParameter>(args);
            result.Parameter.ShouldEqual("value");
        }

        private static string[] Args(params string[] args)
        {
            return args;
        }

        [TestCase(new[] { "-IsTrue", "true" }, true)]
        [TestCase(new[] { "-IsTrue", "false" }, false)]
        [TestCase(new[] { "-IsTrue" }, true)]
        public void MatchToObject_MatchesBoolValue(string[] arg, bool expected)
        {
            var result = matcher.MatchToObject<BooleanParameter>(arg);
            result.IsTrue.ShouldEqual(expected);
        }

        [TestCase(new[] { "-Bool", "true" }, true)]
        [TestCase(new[] { "-Bool", "false" }, false)]
        [TestCase(new[] { "-Bool" }, true)]
        public void MatchToObject_MatchesBoolValue2(string[] arg, bool expected)
        {
            var result = matcher.MatchToObject<MultiTypeParameter>(arg);
            result.Bool.ShouldEqual(expected);
        }

        [TestCase(new[] { "/Bool"  }, true)]
        [TestCase(new[] { "-Bool"  }, true)]
        [TestCase(new[] { "--Bool" }, true)]
        public void MatchToObject_CommandPrefix(string[] arg, bool expected)
        {
            var result = matcher.MatchToObject<MultiTypeParameter>(arg);
            result.Bool.ShouldEqual(expected);
        }

        [TestCase(new [] { "-Parameter", "foo" }, "foo")]
        [TestCase(new [] { "-parameter", "foo" }, "foo")]
        public void MatchToObject_IgnoreCase(string[] args, string expected)
        {
            var result = matcher.MatchToObject<SingleParameter>(args);
            Assert.That(result.Parameter, Is.EqualTo(expected));
        }

        [Test]
        public void Match_NoMatch_ReturnsNull()
        {
            matcher.Match(new string[0])
                   .ShouldBeNull();
        }

        [Test]
        public void MatchToObject_NoMatch_ReturnsNull()
        {
            matcher.MatchToObject<SingleParameter>(new string[0])
                   .ShouldBeNull();
        }

        [Test]
        public void MatchToObject_NoParameterName_MatchesFirstDefaultArgument()
        {
            matcher.MatchToObject<DefaultParameters>(new[] {"foo"}).ShouldNotBeNull();
        }

        [Test]
        public void MatchToObject_NoParameterName_SetsFirstDefaultArgumentValue()
        {
            var result = matcher.MatchToObject<DefaultParameters>(new[] { "foo" });
            result.DefaultProperty1.ShouldEqual("foo");
        }

        [Test]
        public void MatchToObject_NoParameterNames_MatchesDefaultArgumentsInOrder()
        {
            matcher.MatchToObject<DefaultParameters>(new[] {"foo", "bar"}).ShouldNotBeNull();
        }

        [Test]
        public void MatchToObject_NoParameterNames_SetsDefaultArgumentsInOrder()
        {
            var result = matcher.MatchToObject<DefaultParameters>(new[] {"foo", "bar"});
            result.DefaultProperty1.ShouldEqual("foo");
            result.DefaultProperty2.ShouldEqual("bar");
        }
    }
}
