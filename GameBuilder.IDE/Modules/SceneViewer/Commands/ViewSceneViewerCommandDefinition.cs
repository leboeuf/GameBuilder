using Gemini.Framework.Commands;

namespace GameBuilder.IDE.Modules.SceneViewer.Commands
{
    [CommandDefinition]
    public class ViewSceneViewerCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.SceneViewer";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "_3D Scene"; }
        }

        public override string ToolTip
        {
            get { return "3D Scene"; }
        }
    }
}