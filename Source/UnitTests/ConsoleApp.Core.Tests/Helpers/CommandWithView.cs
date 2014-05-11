namespace Consolas.Core.Tests.Helpers
{
    public class CommandWithView : Command
    {
        public void Execute(CommandWithViewArgs args)
        {
            Render("NonExistantView");
        }
    }
}