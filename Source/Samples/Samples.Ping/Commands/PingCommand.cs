using System.Threading;
using ConsoleApp.Core;
using Samples.Ping.Models;

namespace Samples.Ping.Commands
{
    public class PingCommand
    {
        private readonly IConsole _console;
        private readonly IThreadService _thread;
        private const int Bytes = 32;

        public PingCommand(IConsole console, IThreadService thread)
        {
            _console = console;
            _thread = thread;
        }

        public string Execute(Args args)
        {
            var response = _console;
            var ping = new System.Net.NetworkInformation.Ping();

            for (int i = 0; i < 4; i++)
            {
                var reply = ping.Send(args.Host);
                response.Write(string.Format("Reply from {0}: bytes:{1} time", args.Host, Bytes));
                response.Write(reply.RoundtripTime < 1 ? "<1" : "=" + reply.RoundtripTime);
                response.Write(string.Format("ms TTL={0}", reply.Options.Ttl));
                response.WriteLine("");
                _thread.Sleep(1000);
            }
            response.WriteLine("Ping statistics for " + args.Host);

            return "";
        }
    }
}