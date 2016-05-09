using OpenTK;

namespace GameBuilder.Library.Graphics
{
    public class Camera
    {
        public Matrix4 ViewMatrix;

        public Camera(Vector3 _position)
        {
            ViewMatrix = Matrix4.CreateTranslation(_position);
        }

        public virtual void Update()
        {
        }

        public virtual void LookThrough()
        {
            GraphicsManager.SetWorldMatrix(ViewMatrix);
        }

    }
}
