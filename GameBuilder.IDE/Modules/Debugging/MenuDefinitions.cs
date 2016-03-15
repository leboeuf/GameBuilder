using System.ComponentModel.Composition;
using GameBuilder.IDE.Modules.Debugging.Commands;
using Gemini.Framework.Menus;

namespace GameBuilder.IDE.Modules.Debugging
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition DebugRunProjectMenuItem = new CommandMenuItemDefinition<RunProjectCommandDefinition>(Startup.Module.DebugMenuGroup, 0);
    }
}