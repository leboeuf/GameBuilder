using GameBuilder.IDE.Modules.ShaderEditor.Commands;
using Gemini.Framework.Menus;
using System.ComponentModel.Composition;

namespace GameBuilder.IDE.Modules.ShaderEditor
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition ViewShaderEditorMenuItem = new CommandMenuItemDefinition<ViewShaderEditorCommandDefinition>(Gemini.Modules.MainMenu.MenuDefinitions.ViewToolsMenuGroup, 10);
    }
}
