using GameBuilder.Library.Graphics;
using OpenTK.Graphics;

namespace GameBuilder.Library.Entities
{
    public class SpriteEntity : Entity
    {
        protected Texture _texture;

        public SpriteEntity(float x, float y, float z, string spriteName)
            : base(x, y, z)
        {
            _texture = Assets.GetTexture(spriteName);
        }

        public override void Render()
        {
            base.Render();
            GraphicsManager.DrawTexture(_texture, _position.X, _position.Y, _position.Z, Color4.White);
        }
    }
}
