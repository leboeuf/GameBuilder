using GameBuilder.IDE.Utils;
using Gemini.Framework.Commands;

namespace GameBuilder.IDE.Modules.ShaderEditor.Commands
{
    [CommandDefinition]
    public class ViewShaderEditorCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.ShaderEditor";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return ResourceTool.GetCommandText(CommandName); }
        }

        public override string ToolTip
        {
            get { return ResourceTool.GetCommandTooltip(CommandName); }
        }
    }
}
