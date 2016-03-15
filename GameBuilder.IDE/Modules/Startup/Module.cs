using System.ComponentModel.Composition;
using GameBuilder.IDE.Utils;
using Gemini.Framework;
using Gemini.Framework.Menus;

namespace GameBuilder.IDE.Modules.Startup
{
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        [Export]
        public static MenuDefinition DebugMenu = new MenuDefinition(Gemini.Modules.MainMenu.MenuDefinitions.MainMenuBar, 5, ResourceTool.GetCommandText("Debug"));

        [Export]
        public static MenuItemGroupDefinition DebugMenuGroup = new MenuItemGroupDefinition(DebugMenu, 0);

        public override void Initialize()
        {
            MainWindow.Title = ResourceTool.GetLabel("_ApplicationName");
        }
    }
}
