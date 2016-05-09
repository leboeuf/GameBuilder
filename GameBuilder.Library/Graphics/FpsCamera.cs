using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using OpenTK;

namespace GameBuilder.Library.Graphics
{
    public class FpsCamera : Camera
    {
        private Point _previousMousePosition;

        public FpsCamera(Vector3 position) : base(position)
        {

        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.IsKeyDown(Key.W))
            {
                ViewMatrix *= Matrix4.CreateTranslation(0, 0, 0.5f);
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                ViewMatrix *= Matrix4.CreateTranslation(0, 0, -0.5f);
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                ViewMatrix *= Matrix4.CreateTranslation(0.5f, 0, 0);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                ViewMatrix *= Matrix4.CreateTranslation(-0.5f, 0, 0);
            }

            if (Keyboard.IsKeyDown(Key.Space))
            {
                ViewMatrix *= Matrix4.CreateTranslation(0, -0.5f, 0);
            }
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                ViewMatrix *= Matrix4.CreateTranslation(0, 0.5f, 0);
            }

            if (Keyboard.IsKeyDown(Key.Q))
            {
                ViewMatrix *= Matrix4.CreateRotationZ(-0.02f);
            }
            if (Keyboard.IsKeyDown(Key.E))
            {
                ViewMatrix *= Matrix4.CreateRotationZ(0.02f);
            }

            //var mousePosition = Control.MousePosition;
            //var mouseDeltaX = _previousMousePosition.X - mousePosition.X;
            //var mouseDeltaY = _previousMousePosition.Y - mousePosition.Y;

            //if (mouseDeltaX != 0)
            //{
            //    ViewMatrix *= Matrix4.CreateRotationY(0.02f * -mouseDeltaX);
            //}
            //if (mouseDeltaY != 0)
            //{
            //    ViewMatrix *= Matrix4.CreateRotationX(0.02f * -mouseDeltaY);
            //}

            //_previousMousePosition = mousePosition;
        }
    }
}
