﻿using ConsoleApp.Core;
using NSubstitute;
using NUnit.Framework;
using Samples.Grep.Commands;

namespace Samples.Grep.Tests
{
    [TestFixture]
    public class GrepCommandTests
    {
        [Test]
        public void ExpectedBehavior()
        {
            var console = Substitute.For<IConsole>();
            var command = new GrepCommand(console);

            var result = command.Execute(new GrepArgs
            {
                FileName = "doc.txt",
                Regex = "foo"
            });
             
            StringAssert.Contains("foo bar baz", result);
        }
    }
}
