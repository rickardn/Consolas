using System;
using System.Collections.Generic;
using Consolas.Core.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class ArgumentMatcherDefaultArgumentsTests
    {
        private ArgumentMatcher matcher;

        [SetUp]
        public void Setup()
        {
            matcher = new ArgumentMatcher()
            {
                Types = new List<Type>
                {
                    typeof(DefaultParameters),
                    typeof(TwoParameters), 
                    typeof(SingleParameter), 
                    typeof(BooleanParameter),
                    typeof(MultiTypeParameter)
                },
            };
        }

        [Test]
        public void MatchToObject_NoParameterName_MatchesFirstDefaultArgument()
        {
            MatchToObject(new[] { "foo" }).ShouldNotBeNull();
        }

        [Test]
        public void MatchToObject_NoParameterName_SetsFirstDefaultArgumentValue()
        {
            DefaultParameters result = MatchToObject(new[] { "foo" });
            result.DefaultProperty1.ShouldEqual("foo");
        }

        [Test]
        public void MatchToObject_NoParameterNames_MatchesMultipleArguments()
        {
            MatchToObject(new[] { "foo", "bar" }).ShouldNotBeNull();
        }

        [Test]
        public void MatchToObject_NoParameterNames_SetsDefaultArgumentsInOrder1()
        {
            DefaultParameters result = MatchToObject(new[] { "foo", "bar" });
            result.DefaultProperty1.ShouldEqual("foo");
            result.DefaultProperty2.ShouldEqual("bar");
        }

        [Test]
        public void MatchToObject_NoParameterNames_SetsDefaultArgumentsInOrder2()
        {
            DefaultParameters result = MatchToObject(new[] { "foo", "bar", "baz" });
            result.DefaultProperty1.ShouldEqual("foo");
            result.DefaultProperty2.ShouldEqual("bar");
            result.DefaultProperty3.ShouldEqual("baz");
        }

        [Test]
        public void MatchToObject_BooleanDefaultArgument_SetsBooleanArgumentFalse()
        {
            DefaultParameters result = MatchToObject(new[] { "foo", "bar", "baz", "false" });
            result.DefaultProperty4.ShouldBeFalse();
        }

        [Test]
        public void MatchToObject_BooleanDefaultArgument_SetsBooleanArgumentTrue()
        {
            DefaultParameters result = MatchToObject(new[] { "foo", "bar", "baz", "true" });
            result.DefaultProperty4.ShouldBeTrue();
        }

        [Test]
        public void MatchToObject_BooleanDefaultArgument_TreatsInvalidBooleanAsFalse()
        {
            DefaultParameters result = MatchToObject(new[] { "foo", "bar", "baz", "lkj" });
            result.DefaultProperty4.ShouldBeFalse();
        }

        [Test]
        public void MatchToObject_ExplicitDefaultArgument_SetsDefaultArgument()
        {
            DefaultParameters result = MatchToObject(new[] {"-DefaultProperty1", "foo"});
            result.DefaultProperty1.ShouldEqual("foo");
            result.DefaultProperty2.ShouldBeNull();
        }

        [Test]
        public void MatchToObject_MixingImplicitDefaultArgumentsWithExplicit_MatchesDefaultArgument1()
        {
            DefaultParameters result = MatchToObject(new[] {"foo", "-DefaultProperty2", "bar"});
            result.DefaultProperty1.ShouldEqual("foo");
            result.DefaultProperty2.ShouldEqual("bar");
        }

        [Test]
        public void MatchToObject_MixingImplicitDefaultArgumentsWithExplicit_MatchesDefaultArgument2()
        {
            DefaultParameters result = MatchToObject(new[] { "foo", "bar", "baz" });
            result.DefaultProperty1.ShouldEqual("foo");
            result.DefaultProperty2.ShouldEqual("bar");
            result.DefaultProperty2.ShouldEqual("bar");
        }

        private DefaultParameters MatchToObject(string[] args)
        {
            return matcher.MatchToObject<DefaultParameters>(args);
        }
    }
}
