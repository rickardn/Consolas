﻿using System;
using ConsoleApp.Core.Tests.Helpers;
using NUnit.Framework;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ConsoleAppBaseTests : ConsoleTest
    {
        [Test]
        public void Match_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] {"-Parameter", "foo"});
            StringAssert.Contains("SimpleCommand", ConsoleOut.ToString());
        }

        [Test]
        public void Match_ExplicitDefaultArguments_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] {"-DefaultProperty1", "foo", "-DefaultProperty2", "bar"});
            StringAssert.Contains("foo", ConsoleOut.ToString());
            StringAssert.Contains("bar", ConsoleOut.ToString());
        }

        [Test]
        public void Match_ImplicitDefaultArguments_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] {"foo", "bar"});
            StringAssert.Contains("foo", ConsoleOut.ToString());
            StringAssert.Contains("bar", ConsoleOut.ToString());
            StringAssert.Contains("DefaultCommand", ConsoleOut.ToString());
        }

        [Test]
        public void Match_NoMatchingArgument_PrintsUsageMessageIfHelpCommandIsMissing()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] {"-NoMatchingArg"});
            StringAssert.Contains("Using", ConsoleOut.ToString());
        }

        [Test]
        public void Match_ArgumentWithoutCommand_ThrowsException()
        {
            var sut = new SimpleConsoleApp();
            Assert.Throws<NotImplementedException>(() =>
                sut.Main(new[] {"-ArgWithoutCommand", "foo"}));
        }

        [Test]
        public void Match_CommandWhichThrowsException_SameExceptionIsRethrown()
        {
            var sut = new SimpleConsoleApp();
            Assert.Throws<Exception>(() => sut.Main(new[] {"-Throw"}));
        }
    }
}