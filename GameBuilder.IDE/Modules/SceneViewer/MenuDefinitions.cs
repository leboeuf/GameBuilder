using System.ComponentModel.Composition;
using GameBuilder.IDE.Modules.SceneViewer.Commands;
using Gemini.Framework.Menus;

namespace GameBuilder.IDE.Modules.SceneViewer
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemGroupDefinition ViewScenesMenuGroup = new MenuItemGroupDefinition(
            Gemini.Modules.MainMenu.MenuDefinitions.ViewMenu, 0);

        [Export]
        public static MenuItemDefinition ViewSceneViewerMenuItem = new CommandMenuItemDefinition<ViewSceneViewerCommandDefinition>(
            ViewScenesMenuGroup, 0);
    }
}