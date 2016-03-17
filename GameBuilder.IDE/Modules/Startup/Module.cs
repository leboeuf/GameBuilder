using System.ComponentModel.Composition;
using GameBuilder.IDE.Utils;
using Gemini.Framework;

namespace GameBuilder.IDE.Modules.Startup
{
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        public override void Initialize()
        {
            MainWindow.Title = ResourceTool.GetLabel("_ApplicationName");
        }
    }
}
