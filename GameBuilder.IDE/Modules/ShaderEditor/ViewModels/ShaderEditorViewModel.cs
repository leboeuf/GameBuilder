using Caliburn.Micro;
using GameBuilder.IDE.Modules.SceneViewer.ViewModels;
using GameBuilder.IDE.Modules.SceneViewer.Views;
using GameBuilder.IDE.Modules.ShaderEditor.Views;
using Gemini.Framework;
using Gemini.Framework.Services;
using OpenTK.Graphics.OpenGL4;
using System.ComponentModel.Composition;
using System.Linq;

namespace GameBuilder.IDE.Modules.ShaderEditor.ViewModels
{
    [Export(typeof(ShaderEditorViewModel))]
    public class ShaderEditorViewModel : Document
    {
        private IShaderEditorView _shaderEditorView;

        public ShaderEditorViewModel()
        {
            DisplayName = "Shader Editor";
        }

        protected override void OnViewLoaded(object view)
        {
            _shaderEditorView = (IShaderEditorView)view;
            _shaderEditorView.TextEditor.Text = @"#version 400

layout(location = 0) out vec4 frag_color;
in vec4 color;

void main(void)
{
    frag_color = color;
}";

            _shaderEditorView.TextEditor.TextChanged += (sender, e) => CompileShader();
            CompileShader();

            base.OnViewLoaded(view);
        }

        private void CompileShader()
        {
            // Grab the first 3D Scene View to apply shader on it
            var sceneViewModel = IoC.Get<IShell>().Documents.OfType<SceneViewModel>().FirstOrDefault();
            var shaderCode = _shaderEditorView.TextEditor.Text;
            sceneViewModel.ReplaceShader(ShaderType.FragmentShader, shaderCode);
        }
    }
}
