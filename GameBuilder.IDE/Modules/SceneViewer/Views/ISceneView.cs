using GameBuilder.Library;
using OpenTK.Graphics.OpenGL4;

namespace GameBuilder.IDE.Modules.SceneViewer.Views
{
    public interface ISceneView
    {
        State State { get; set; }

        /// <summary>
        /// Replaces a shader and then redraws the scene.
        /// </summary>
        void ReplaceShader(ShaderType shaderType, string shaderCode);

        void Invalidate();
    }
}