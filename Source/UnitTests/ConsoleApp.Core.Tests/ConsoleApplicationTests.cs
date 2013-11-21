using System;
using ConsoleApp.Core.Tests.Helpers;
using NUnit.Framework;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ConsoleApplicationTests
    {
        [Test]
        public void Match_Null_Throws()
        {
            var sut = new MyConsoleAppBase();
            Assert.Throws<ArgumentException>(() => 
                sut.Main(null));
        }
    }
}
