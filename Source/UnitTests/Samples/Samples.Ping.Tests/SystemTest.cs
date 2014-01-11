using NUnit.Framework;

namespace Samples.Ping.Tests
{
    [TestFixture]
    public class SystemTest
    {
        [Test]
        public void Main_()
        {
            Program.Main(new [] {"-Host", "google.com"});

        }
    }
}
