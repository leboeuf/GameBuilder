using Caliburn.Micro;
using GameBuilder.IDE.Modules.ShaderEditor.ViewModels;
using Gemini.Framework;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace GameBuilder.IDE.Modules.ShaderEditor.Commands
{
    [CommandHandler]
    public class ViewShaderEditorCommandHandler : CommandHandlerBase<ViewShaderEditorCommandDefinition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public ViewShaderEditorCommandHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.OpenDocument((IDocument) IoC.GetInstance(typeof(ShaderEditorViewModel), null));
            return TaskUtility.Completed;
        }
    }
}
