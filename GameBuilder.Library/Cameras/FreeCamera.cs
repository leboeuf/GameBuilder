using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace GameBuilder.Library.Cameras
{
    //http://www.madgamedev.com/post/2010/09/05/Article-Simple-3D-Camera-in-XNA.aspx
    public class FreeCamera : ICamera
    {
        public Matrix4 ViewMatrix { get; private set; }
        public Matrix4 ProjectionMatrix { get; private set; }

        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }

        public FreeCamera()
        {
            Reset();
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
            ViewMatrix = Matrix4.LookAt(Position, Target, Vector3.UnitY);
        }
    }
}
