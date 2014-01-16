using System.Threading;

namespace Samples.Ping.Models
{
    public class ThreadService : IThreadService
    {
        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }
    }
}