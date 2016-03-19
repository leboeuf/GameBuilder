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
using GameBuilder.Library.Cameras;

namespace GameBuilder.Library.OpenGL
{
    public class OpenGLManager
    {
        private Matrix4 ProjectionMatrix;
        private Matrix4 WorldMatrix;
        private Matrix4 ModelviewMatrix;

        private Vector3 cameraPosition;
        private Vector3 cameraRotation;
        private Vector3 modelViewMatrixTranslationVector;
        private Vector3 projectionMatrixVector;

        public /*ICamera*/FirstPersonCamera Camera = new FirstPersonCamera();

        private Shader shader;
        private VertexFloatBuffer buffer;

        public Vector3 CameraPosition
        {
            get
            {
                return cameraPosition;
            }

            set
            {
                cameraPosition = value;
            }
        }
        public Vector3 CameraRotation
        {
            get
            {
                return cameraRotation;
            }

            set
            {
                cameraRotation = value;
            }
        }
        public Vector3 ModelViewMatrixTranslationVector
        {
            get
            {
                return modelViewMatrixTranslationVector;
            }

            set
            {
                modelViewMatrixTranslationVector = value;
            }
        }
        public Vector3 ProjectionMatrixVector
        {
            get
            {
                return projectionMatrixVector;
            }

            set
            {
                projectionMatrixVector = value;
            }
        }

        public void Load(GLControl glControl)
        {
            GL.ClearColor(Color.CornflowerBlue);

            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, glControl.Width / (float)glControl.Height, 0.5f, 10000.0f);
            WorldMatrix = new Matrix4();
            ModelviewMatrix = new Matrix4();

            CameraPosition = new Vector3(0.5f, 0.5f, 0);
            ModelViewMatrixTranslationVector = new Vector3(0.0f, 0.0f, -2.0f);
            ProjectionMatrixVector = new Vector3(glControl.Width / (float)glControl.Height, 0.5f, 10000.0f);

            // Setup the shader
            var shaderManager = new ShaderManager();
            var vertex_source = shaderManager.LoadShader(ShaderType.VertexShader, "test1");
            var fragment_source = shaderManager.LoadShader(ShaderType.FragmentShader, "test1");

            shader = new Shader(ref vertex_source, ref fragment_source);
            
            //setup the vertex buffer [vbo]
            buffer = new VertexFloatBuffer(VertexFormat.XYZ_COLOR, 3);
            //just a triangle with full r g b
            //buffer.AddVertex(0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.8f);
            //buffer.AddVertex(0.5f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.8f);
            //buffer.AddVertex(1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.8f);


            buffer = new VertexFloatBuffer(VertexFormat.XYZ_COLOR, 36);
            buffer.AddVertex(-1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.8f);
            buffer.AddVertex(-1.0f, -1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 0.8f);
            buffer.AddVertex(-1.0f, -1.0f, -1.0f, 0.0f, 1.0f, 0.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.8f);
            buffer.AddVertex(-1.0f, -1.0f, -1.0f, 0.0f, 1.0f, 0.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(-1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);
            buffer.AddVertex(1.0f, -1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.8f);

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
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shader.Program);
            buffer.Bind(shader);
            GL.UseProgram(0);

            glControl.SwapBuffers();
        }

        public void Update(GLControl glControl)
        {
            Camera.Update();
            //if (Camera.HandlesInput)
            //{
            //    // Recenter mouse pointer
            //    System.Windows.Forms.Cursor.Position = new Point(glControl.Bounds.Left + glControl.Bounds.Width / 2, glControl.Bounds.Top + glControl.Bounds.Height / 2);
            //}

            //set out triangle position with the modelview matrix
            //ModelviewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -2.0f);
            ModelviewMatrix = Matrix4.CreateTranslation(ModelViewMatrixTranslationVector);

            //ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, ProjectionMatrixVector.X, ProjectionMatrixVector.Y, ProjectionMatrixVector.Z);
            /*
            ModelviewMatrix = Matrix4.CreateRotationX(CameraRotation.X);
            ModelviewMatrix = Matrix4.CreateRotationY(CameraRotation.Y);
            ModelviewMatrix = Matrix4.CreateRotationZ(CameraRotation.Z);
            */
            //combine all matrices
            //the different between GL and GLSL with matrix order
            //GL   modelview * worldview * projection;
            //GLSL projection * worldview * modelview;
            //Matrix4 MVP_Matrix = ModelviewMatrix * WorldMatrix * ProjectionMatrix;
            Matrix4 MVP_Matrix = Camera.ViewMatrix * Camera.ProjectionMatrix;


            //send to shader
            GL.UseProgram(shader.Program);
            //will return -1 without useprogram
            int mvp_matrix_location = GL.GetUniformLocation(shader.Program, "mvp_matrix");
            GL.UniformMatrix4(mvp_matrix_location, false, ref MVP_Matrix);
            GL.UseProgram(0);
        }
    }
}
