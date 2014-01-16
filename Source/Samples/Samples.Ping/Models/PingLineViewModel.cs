using System.Net.NetworkInformation;

namespace Samples.Ping.Models
{
    public class PingLineViewModel
    {
        public PingReply Reply { get; set; }
        public string Host { get; set; }
        public int Bytes { get; set; }

        public PingLineViewModel(PingReply reply, string host, int bytes)
        {
            Reply = reply;
            Host = host;
            Bytes = bytes;
        }

        public int Ttl
        {
            get { return Reply.Options.Ttl; }
        }

        public string RoundTripTime
        {
            get { return Reply.RoundtripTime < 1 ? "<1" : "=" + Reply.RoundtripTime; }
        }
    }
}