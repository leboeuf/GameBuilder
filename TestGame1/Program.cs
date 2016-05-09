using GameBuilder.Library;
using GameBuilder.Library.Graphics;
using TestGame1.States;
using TestGame1.Tools;

namespace TestGame1
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new EngineWindow(ResourceTool.GetLabel("_windowTitle"));
            StateHandler.Push(new GameState(GraphicsManager.RenderMode.Perspective, false));
            window.Run(30.0f);
        }
    }
}
