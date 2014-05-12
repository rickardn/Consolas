namespace Consolas.Core.Tests.Helpers
{
    public class CommandWithShowView : Command
    {
        public object Execute(CommandWithShowViewArgs args)
        {
            return View("NonExistantView");
        }
    }
}