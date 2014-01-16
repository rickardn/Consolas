using ConsoleApp.Core;
using Samples.Ping.Models;

namespace Samples.Ping.Commands
{
    public class PingCommand : Command
    {
        private readonly IThreadService _thread;

        public PingCommand(IThreadService thread)
        {
            _thread = thread;
        }

        public void Execute(PingArgs args)
        {
            var viewModel = new PingViewModel
            {
                Host = args.Host,
                Thread = _thread
            };

            Render("View", viewModel);
        }
    }
}