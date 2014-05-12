namespace Consolas.Core.Tests.Helpers
{
    public class CommandWithRenderView : Command
    {
        public void Execute(CommandWithRenderViewArgs args)
        {
            Render("NonExistantView");
        }
    }
}