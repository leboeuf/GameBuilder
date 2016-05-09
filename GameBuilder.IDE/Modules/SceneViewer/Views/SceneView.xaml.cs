using System;
using System.Drawing;
using System.Windows.Forms;
using Caliburn.Micro;
using Gemini.Modules.Output;
using OpenTK.Graphics.OpenGL4;
using UserControl = System.Windows.Controls.UserControl;
using System.Windows.Media;
using GameBuilder.Library;
using GameBuilder.Library.Entities;
using GameBuilder.Library.Graphics;
using OpenTK;

namespace GameBuilder.IDE.Modules.SceneViewer.Views
{
    /// <summary>
    /// Interaction logic for SceneView.xaml
    /// </summary>
    /// <remarks>
    /// GLControl loop from http://www.opentk.com/node/4100
    /// </remarks>
    public partial class SceneView : UserControl, ISceneView, IDisposable
    {
        public State State { get; set; }
        private readonly IOutput _output;

        public SceneView()
        {
            InitializeComponent();
            _output = IoC.Get<IOutput>();

            windowsFormsHost.Child = glControl;
        }

        private void GLControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (!OpenGLManager.Camera.HandlesInput)
            //{
            //    return;
            //}
        }

        public void Dispose()
        {
            
        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            GraphicsManager.Draw(glControl);
        }

        private void GLControl_Load(object sender, EventArgs e)
        {
            State = new GameState(GraphicsManager.RenderMode.Perspective, false);
            StateHandler.Push(State);

            GraphicsManager.Initialize(glControl);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            GraphicsManager.SetProjection(this.glControl.ClientRectangle);

            StateHandler.UpdateFrame(null);

            GraphicsManager.Clear();
            StateHandler.RenderFrame(null);

            glControl.Invalidate();
        }

        public void ReplaceShader(ShaderType shaderType, string shaderCode)
        {
            try
            {
                //GraphicsManager.ReplaceShader(shaderType, shaderCode);
                glControl.Invalidate();
            }
            catch (ApplicationException)
            {
                // Shader contains errors, do nothing.
            }
        }

        public void Invalidate()
        {
            glControl.Invalidate();
        }

        private void GLControl_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //GraphicsManager.Camera.HandlesInput = true;
            }
        }
    }

    public class GameState : State
    {
        public GameState(GraphicsManager.RenderMode renderMode, bool overlay)
            : base(renderMode, overlay)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            var camera = new FpsCamera(new Vector3(0, 0, 0));
            this.SetCamera(camera);

            var ent2 = new ModelEntity(0, 0, 0, "couch1.obj");
            this.AddEntity(ent2);
            ent2.SetScale(0.2f);
        }
    }
}
