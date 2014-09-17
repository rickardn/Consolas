using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Consolas.Core.Tests
{
    public abstract class ConsoleTestBase
    {
        private TextWriter _consoleOutWriter;
        protected StringBuilder ConsoleOut;

        [SetUp]
        public virtual void Setup()
        {
            _consoleOutWriter = Console.Out;
            ConsoleOut = new StringBuilder();
            var testOut = new StringWriter(ConsoleOut);
            Console.SetOut(testOut);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Console.SetOut(_consoleOutWriter);
        }
    }
}