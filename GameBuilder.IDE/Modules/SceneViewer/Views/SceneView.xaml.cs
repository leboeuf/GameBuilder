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
        private System.Windows.Point _previousPosition;
        private float _yaw = 0.5f;
        private float _pitch = 0.2f;

        public SceneView()
        {
            InitializeComponent();
            _output = IoC.Get<IOutput>();

            windowsFormsHost.Child = glControl;
        }
        
        /// <summary>
        /// Invoked when our second control is ready to render.
        /// </summary>
        //private void OnGraphicsControlDraw(object sender, DrawEventArgs e)
        //{
        //    e.GraphicsDevice.Clear(Color.CornflowerBlue);

        //    // Create the world-view-projection matrices for the cube and camera
        //    var position = ((SceneViewModel) DataContext).Position;
        //    Matrix world = Matrix.CreateFromYawPitchRoll(_yaw, _pitch, 0f) * Matrix.CreateTranslation(position);
        //    Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 2.5f), Vector3.Zero, Vector3.Up);
        //    Matrix projection = Matrix.CreatePerspectiveFieldOfView(1, e.GraphicsDevice.Viewport.AspectRatio, 1, 10);

        //    // Draw a cube
        //    _cube.Draw(world, view, projection, Color.LightGreen);
        //}

        // Invoked when the mouse moves over the second viewport
        private void OnGraphicsControlMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);

            // If the left or right buttons are down, we adjust the yaw and pitch of the cube
            if (e.LeftButton == MouseButtonState.Pressed ||
                e.RightButton == MouseButtonState.Pressed)
            {
                _yaw += (float) (position.X - _previousPosition.X) * .01f;
                _pitch += (float) (position.Y - _previousPosition.Y) * .01f;
                //GraphicsControl.Invalidate();
            }

            _previousPosition = position;
        }

        // We use the left mouse button to do exclusive capture of the mouse so we can drag and drag
        // to rotate the cube without ever leaving the control
        private void OnGraphicsControlHwndLButtonDown(object sender, MouseEventArgs e)
        {
            _output.AppendLine("Mouse left button down");
            _previousPosition = e.GetPosition(this);
            //GraphicsControl.CaptureMouse();
            //GraphicsControl.Focus();
        }

        private void OnGraphicsControlHwndLButtonUp(object sender, MouseEventArgs e)
        {
            _output.AppendLine("Mouse left button up");
            //GraphicsControl.ReleaseMouseCapture();
        }

        private void OnGraphicsControlKeyDown(object sender, KeyEventArgs e)
        {
            _output.AppendLine("Key down: " + e.Key);
        }

        private void OnGraphicsControlKeyUp(object sender, KeyEventArgs e)
        {
            _output.AppendLine("Key up: " + e.Key);
        }

        private void OnGraphicsControlHwndMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _output.AppendLine("Mouse wheel: " + e.Delta);
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
    }
}
