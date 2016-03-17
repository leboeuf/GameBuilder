using Gemini.Framework;
using System.ComponentModel.Composition;

namespace GameBuilder.IDE.Modules.ShaderEditor
{
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}
