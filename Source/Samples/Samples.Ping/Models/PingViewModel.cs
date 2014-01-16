using System;

namespace Samples.Ping.Models
{
    public class PingViewModel
    {
        public int Bytes { get { return 32; } }
        public int Times { get { return 4; } }
        public string Host { get; set; }
        public IThreadService Thread { get; set; }

        public PingLineViewModel Ping()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1).Milliseconds);

            var ping = new System.Net.NetworkInformation.Ping();
            var reply = ping.Send(Host);
            var viewModel = new PingLineViewModel
            (
                bytes: Bytes,
                host: Host,
                reply: reply
            );

            return viewModel;
        }
    }
}