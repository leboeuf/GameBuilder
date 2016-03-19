using System.Windows.Input;
using OpenTK;
using System;

namespace GameBuilder.Library.Cameras
{
    //http://neokabuto.blogspot.ca/2014/01/opentk-tutorial-5-basic-camera.html
    //http://gamedev.stackexchange.com/questions/60266/create-a-fly-camera-with-lookat
    public class FirstPersonCamera : ICamera
    {
        public Matrix4 ViewMatrix { get; private set; }
        public Matrix4 ProjectionMatrix { get; private set; }

        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }

        /// <summary>
        /// Determines whether to check the keyboard and mouse on update to move the camera.
        /// </summary>
        public bool HandlesInput { get; set; }

        private float _pitch;
        private float _yaw;

        public FirstPersonCamera()
        {
            Reset();
            HandlesInput = true; // TODO: remove here and handle from the IDE
        }

        private void Reset()
        {
            Position = new Vector3(0, 0, 50.0f);
            Target = new Vector3();

            ViewMatrix = Matrix4.Identity;
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 16 / 9, 0.5f, 500.0f);
        }

        public void Update()
        {
            if (HandlesInput)
            {
                HandleInput();
            }

            //ViewMatrix = Matrix4.LookAt(Position, Target, Vector3.UnitY);
            //ViewMatrix = Matrix4.CreateRotationY(_yaw) * Matrix4.CreateRotationY(_pitch) * Matrix4.CreateTranslation(Position) * ViewMatrix;
            ViewMatrix = Matrix4.CreateTranslation(Position) * Matrix4.CreateRotationY(_yaw) * Matrix4.CreateRotationY(_pitch);
            //ViewMatrix = Matrix4.LookAt(Position, Target, Vector3.UnitY);
        }

        private void HandleInput()
        {
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                HandlesInput = false;
            }

            if (Keyboard.IsKeyDown(Key.W))
            {
                Position = Vector3.Add(Position, Vector3.UnitZ);
            }

            if (Keyboard.IsKeyDown(Key.A))
            {
                Position = Vector3.Add(Position, Vector3.UnitX);
            }

            if (Keyboard.IsKeyDown(Key.S))
            {
                Position = Vector3.Subtract(Position, Vector3.UnitZ);
            }

            if (Keyboard.IsKeyDown(Key.D))
            {
                Position = Vector3.Subtract(Position, Vector3.UnitX);
            }

            if (Keyboard.IsKeyDown(Key.Z))
            {
                Position = Vector3.Subtract(Position, Vector3.UnitY);
            }

            if (Keyboard.IsKeyDown(Key.X))
            {
                Position = Vector3.Add(Position, Vector3.UnitY);
            }
        }

        public void SetPitchYaw(float pitch, float yaw)
        {
            _pitch = pitch;
            _yaw = yaw;
        }
    }
}
