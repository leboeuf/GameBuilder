using System.ComponentModel.Composition;
using System.Windows.Input;
using GameBuilder.IDE.Utils;
using Gemini.Framework.Commands;

namespace GameBuilder.IDE.Modules.Debugging.Commands
{
    [CommandDefinition]
    public class RunProjectCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Debug.RunProject";

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<RunProjectCommandDefinition>(new KeyGesture(Key.F5));

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
