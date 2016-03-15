using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

namespace GameBuilder.IDE.Modules.Debugging.Commands
{
    [CommandHandler]
    public class RunProjectCommandHandler : CommandHandlerBase<RunProjectCommandDefinition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public RunProjectCommandHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            //_shell.StatusBar.Items[0].Message = "Command has been called";

            //// Debug: populate project explorer
            //var projectExplorer = (ProjectExplorerViewModel)IoC.Get<IProjectExplorer>();
            //projectExplorer.CreateNewProject();

            return TaskUtility.Completed;
        }
    }
}
