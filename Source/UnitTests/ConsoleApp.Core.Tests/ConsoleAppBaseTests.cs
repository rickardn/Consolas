using System;
using ConsoleApp.Core.Tests.Helpers;
using NUnit.Framework;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ConsoleAppBaseTests
    {
        [Test]
        public void Match_Null_Throws()
        {
            var sut = new SimpleConsoleApp();
            Assert.Throws<ArgumentException>(() => 
                sut.Main(null));
        }

        [Test]
        public void Match_DefaultCommandNull_Throws()
        {
            var sut = new ConsoleAppWithNullDefaultCommand();
            Assert.Throws<ArgumentException>(() => sut.Main(null));
        }

        
    }
}
