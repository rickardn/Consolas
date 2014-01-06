using System;
using System.IO;
using System.Text;
using ConsoleApp.Core.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ConsoleAppBaseTests
    {
        [Test]
        public void Match_EndToEndTest()
        {
            var textWriter = Console.Out;
            var sb = new StringBuilder();
            var testOut = new StringWriter(sb);
            Console.SetOut(testOut);

            var sut = new SimpleConsoleApp();
            sut.Main(new []{"-Parameter","foo"});

            StringAssert.Contains("SimpleCommand", sb.ToString());
            Console.SetOut(textWriter);
        }
    }
}
