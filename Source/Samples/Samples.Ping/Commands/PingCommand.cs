using Consolas.Core;
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

        public object Execute(PingArgs args)
        {
            var viewModel = new PingViewModel
            {
                Host = args.Host,
                Thread = _thread
            };

            return View("View", viewModel);
        }
    }
}