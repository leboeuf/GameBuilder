using GameBuilder.Library.Graphics;
using OpenTK;
using OpenTK.Graphics;

namespace GameBuilder.Library.Entities
{
    public class ModelEntity : Entity
    {
        protected Model _model;
        protected Material _material = Material.Crimson;
        protected float _scale = 1.0f;

        public ModelEntity(float x, float y, float z, string modelName)
            : base(x, y, z)
        {
            _model = Assets.GetModel(modelName);
        }

        public void SetScale(float scale)
        {
            _scale = scale;
        }

        public override void Render()
        {
            base.Render();
            _material.Bind();
            Matrix4 matrix = Matrix4.CreateScale(_scale) * Matrix4.CreateTranslation(_position);

            foreach(ModelMesh mesh in _model.GetMeshes())
                GraphicsManager.RenderModelMesh(mesh, matrix, Color4.White);
        }

    }
}
