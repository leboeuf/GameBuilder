using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using OpenTK;

namespace GameBuilder.Library.Graphics
{
    public class FpsCamera : Camera
    {
        private Point _previousMousePosition;
        private float _yaw = 0.5f;
        private float _pitch = 0.2f;

        public FpsCamera(Vector3 position)
            : base(position)
        {

        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.IsKeyDown(Key.W))
            {
                viewMatrix *= Matrix4.CreateTranslation(0, 0, 0.5f);
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                viewMatrix *= Matrix4.CreateTranslation(0, 0, -0.5f);
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                viewMatrix *= Matrix4.CreateTranslation(0.5f, 0, 0);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                viewMatrix *= Matrix4.CreateTranslation(-0.5f, 0, 0);
            }

            if (Keyboard.IsKeyDown(Key.Space))
            {
                viewMatrix *= Matrix4.CreateTranslation(0, -0.5f, 0);
            }
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                viewMatrix *= Matrix4.CreateTranslation(0, 0.5f, 0);
            }

            if (Keyboard.IsKeyDown(Key.Q))
            {
                viewMatrix *= Matrix4.CreateRotationZ(-0.02f);
            }
            if (Keyboard.IsKeyDown(Key.E))
            {
                viewMatrix *= Matrix4.CreateRotationZ(0.02f);
            }

            var mousePosition = Control.MousePosition;
            var mouseDeltaX = _previousMousePosition.X - mousePosition.X;
            var mouseDeltaY = _previousMousePosition.Y - mousePosition.Y;

            if (mouseDeltaX != 0)
            {
                viewMatrix *= Matrix4.CreateRotationY(0.02f * -mouseDeltaX);
            }
            if (mouseDeltaY != 0)
            {
                viewMatrix *= Matrix4.CreateRotationX(0.02f * -mouseDeltaY);
            }

            _previousMousePosition = mousePosition;
        }
    }
}
