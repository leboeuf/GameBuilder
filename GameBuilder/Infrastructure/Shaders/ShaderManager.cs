using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
using System.Reflection;

namespace GameBuilder.Infrastructure.Shaders
{
    public class ShaderManager
    {
        /// <summary>
        /// The base folder where to look for shaders.
        /// </summary>
        private readonly string _lookupPath = string.Empty;

        public ShaderManager()
        {
            _lookupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", string.Empty);
        }

        public string LoadShader(ShaderType shaderType, string fileName)
        {
            var folderName = string.Empty;
            var fileExtension = string.Empty;

            switch (shaderType)
            {
                case ShaderType.FragmentShader:
                    folderName = "Fragment";
                    fileExtension = "frag";
                    break;
                case ShaderType.VertexShader:
                    folderName = "Vertex";
                    fileExtension = "vert";
                    break;
                case ShaderType.GeometryShader:
                case ShaderType.TessEvaluationShader:
                case ShaderType.TessControlShader:
                case ShaderType.ComputeShader:
                default:
                    throw new NotImplementedException("Shader type not supported.");
            }

            var shaderCode = File.ReadAllText($"{_lookupPath}\\Assets\\Shaders\\{folderName}\\{fileName}.{fileExtension}");
            return shaderCode;
        }
    }
}
