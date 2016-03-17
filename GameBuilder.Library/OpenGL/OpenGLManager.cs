using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBuilder.Library.OpenGL.Buffers;
using GameBuilder.Library.OpenGL.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace GameBuilder.Library.OpenGL
{
    public class OpenGLManager
    {
        private Matrix4 ProjectionMatrix;
        private Matrix4 WorldMatrix;
        private Matrix4 ModelviewMatrix;

        private Vector3 CameraPosition;

        private Shader shader;
        private VertexFloatBuffer buffer;

        private int Width = 200;
        private int Height = 200;

        public void Load(GLControl glControl)
        {
            GL.ClearColor(Color.CornflowerBlue);

            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 0.5f, 10000.0f);
            WorldMatrix = new Matrix4();
            ModelviewMatrix = new Matrix4();

            CameraPosition = new Vector3(0.5f, 0.5f, 0);

            // Setup the shader
            var shaderManager = new ShaderManager();
            var vertex_source = shaderManager.LoadShader(ShaderType.VertexShader, "test1");
            var fragment_source = shaderManager.LoadShader(ShaderType.FragmentShader, "test1");

            shader = new Shader(ref vertex_source, ref fragment_source);

            //setup the vertex buffer [vbo]
            buffer = new VertexFloatBuffer(VertexFormat.XYZ_COLOR, 3);
            //just a triangle with full r g b
            buffer.AddVertex(0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f);
            buffer.AddVertex(0.5f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f);
            buffer.AddVertex(1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f);

            buffer.IndexFromLength();
            buffer.Load();
        }

        public void ReplaceShader(ShaderType fragmentShader, string shaderCode)
        {
            var vertexShaderCode = shader.VertexSource;
            shader = new Shader(ref vertexShaderCode, ref shaderCode);
        }

        public void Draw(GLControl glControl)
        {
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shader.Program);
            buffer.Bind(shader);
            GL.UseProgram(0);

            glControl.SwapBuffers();
        }

        public void Update(GLControl glControl)
        {
            CameraPosition.X += 0.001f;

            //prepare data to shader

            //set the world matrix
            WorldMatrix = Matrix4.CreateTranslation(-CameraPosition);

            //set out triangle position with the modelview matrix
            ModelviewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -2.0f);

            //combine all matrices
            //the different between GL and GLSL with matrix order
            //GL   modelview * worldview * projection;
            //GLSL projection * worldview * modelview;
            Matrix4 MVP_Matrix = ModelviewMatrix * WorldMatrix * ProjectionMatrix;

            //send to shader
            GL.UseProgram(shader.Program);
            //will return -1 without useprogram
            int mvp_matrix_location = GL.GetUniformLocation(shader.Program, "mvp_matrix");
            GL.UniformMatrix4(mvp_matrix_location, false, ref MVP_Matrix);
            GL.UseProgram(0);
        }
    }
}
