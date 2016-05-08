using OpenTK;

namespace GameBuilder.Library.Graphics
{
    public class Camera
    {
        protected Matrix4 viewMatrix;

        public Camera(Vector3 _position)
        {
            viewMatrix = Matrix4.CreateTranslation(_position);
        }

        public virtual void Update()
        {
        }

        public virtual void LookThrough()
        {
            GraphicsManager.SetWorldMatrix(viewMatrix);
        }

    }
}
