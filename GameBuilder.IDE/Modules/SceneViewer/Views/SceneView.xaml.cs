using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using Caliburn.Micro;
using GameBuilder.Library.OpenGL;
using Gemini.Modules.Output;
using OpenTK.Graphics.OpenGL4;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;
using System.Windows.Media;
using Gemini.Modules.Inspector;
using GameBuilder.IDE.Modules.Inspector;
using GameBuilder.IDE.Modules.SceneViewer.ViewModels;
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
        private readonly IOutput _output;
        public OpenGLManager _openGLManager = new OpenGLManager();

        // A yaw and pitch applied to the viewport based on input
        private Point _previousPosition;
        private float _yaw = 0.5f;
        private float _pitch = 0.2f;

        public OpenGLManager OpenGLManager
        {
            get
            {
                return _openGLManager;
            }
        }

        public SceneView()
        {
            InitializeComponent();
            _output = IoC.Get<IOutput>();

            windowsFormsHost.Child = glControl;
        }

        private void GLControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_openGLManager.Camera.HandlesInput)
            {
                return;
            }

            _yaw += (float)(e.Location.X - _previousPosition.X) * .01f;
            _pitch += (float)(e.Location.Y - _previousPosition.Y) * .01f;
            _openGLManager.Camera.SetPitchYaw(_pitch, _yaw);

            _previousPosition = e.Location;
        }

        public void Dispose()
        {
            
        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            _openGLManager.Draw(glControl);
        }

        private void GLControl_Load(object sender, EventArgs e)
        {
            _openGLManager.Load(glControl);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            _openGLManager.Update(glControl);
            glControl.Invalidate();
        }

        public void ReplaceShader(ShaderType shaderType, string shaderCode)
        {
            try
            {
                _openGLManager.ReplaceShader(shaderType, shaderCode);
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
                _openGLManager.Camera.HandlesInput = true;
            }
        }
    }
}
