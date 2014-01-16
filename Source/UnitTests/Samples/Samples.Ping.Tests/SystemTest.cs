using ConsoleApp.Core.Tests;
using NUnit.Framework;

namespace Samples.Ping.Tests
{
    [TestFixture]
    public class SystemTest : ConsoleTest
    {
        [Test]
        public void Main_ValidHost_RendersResult()
        {
            Program.Main(new [] {"-Host", "google.com"});
            StringAssert.Contains("Reply from google.com", ConsoleOut.ToString());
        }
    }
}
