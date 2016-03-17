using System.ComponentModel.Composition;
using GameBuilder.IDE.Modules.Debugging.Commands;
using Gemini.Framework.Menus;
using GameBuilder.IDE.Utils;

namespace GameBuilder.IDE.Modules.Debugging
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuDefinition DebugMenu = new MenuDefinition(Gemini.Modules.MainMenu.MenuDefinitions.MainMenuBar, 5, ResourceTool.GetCommandText("Debug"));

        [Export]
        public static MenuItemGroupDefinition DebugMenuGroup = new MenuItemGroupDefinition(DebugMenu, 0);

        [Export]
        public static MenuItemDefinition DebugRunProjectMenuItem = new CommandMenuItemDefinition<RunProjectCommandDefinition>(DebugMenuGroup, 0);
    }
}