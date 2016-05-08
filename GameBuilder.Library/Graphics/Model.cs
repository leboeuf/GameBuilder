using System.Collections.Generic;

namespace GameBuilder.Library.Graphics
{
    public class Model
    {
        private List<ModelMesh> _meshes;

        public Model(List<ModelMesh> meshes)
        {
            _meshes = meshes;
        }

        public List<ModelMesh> GetMeshes()
        {
            return _meshes;
        }

        public void DeleteBuffers()
        {
            foreach (ModelMesh mesh in _meshes)
            {
                mesh.DeleteBuffers();
            }
            _meshes.Clear();
        }
    }
}
