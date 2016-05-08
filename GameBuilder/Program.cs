using System;
using System.Drawing;
using GameBuilder.Library;
using GameBuilder.Library.Audio;
using GameBuilder.Library.Entities;
using GameBuilder.Library.Graphics;
using GameBuilder.Library.Gui;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTKCameraPort
{/*
    class Program : GameWindow
    {
        private Matrix4 cameraMatrix;
        private float[] mouseSpeed = new float[2];
        private Vector2 mouseDelta;
        private Vector3 location;
        private Vector3 up = Vector3.UnitY;
        private float pitch = 0.0f;
        private float facing = 0.0f;
        private float moveSensitivity = 1.5f;

        public Program()
            : base(1024, 768)
        {
            CursorVisible = false;
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            cameraMatrix = Matrix4.Identity;
            location = new Vector3(0f, 10f, 0f);
            mouseDelta = new Vector2();

            System.Windows.Forms.Cursor.Position = new Point(Bounds.Left + Bounds.Width / 2, Bounds.Top + Bounds.Height / 2);

            Mouse.Move += new EventHandler<MouseMoveEventArgs>(OnMouseMove);
        }

        void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            mouseDelta = new Vector2(e.XDelta, e.YDelta);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadMatrix(ref cameraMatrix);

            // Display some planes
            for (int x = -10; x <= 10; x++)
            {
                for (int z = -10; z <= 10; z++)
                {
                    GL.PushMatrix();
                    GL.Translate((float)x * 5f, 0f, (float)z * 5f);
                    GL.Begin(BeginMode.Quads);
                    GL.Color3(Color.Red);
                    GL.Vertex3(1f, 4f, 0f);
                    GL.Color3(Color.Orange);
                    GL.Vertex3(-1f, 4f, 0f);
                    GL.Color3(Color.Brown);
                    GL.Vertex3(-1f, 0f, 0f);
                    GL.Color3(Color.Maroon);
                    GL.Vertex3(1f, 0f, 0f);
                    GL.End();
                    GL.PopMatrix();
                }
            }
            SwapBuffers();
        }

        Point pointer_current, pointer_previous;
        Size pointer_delta;

        void UpdateMousePosition()
        {
            pointer_previous = pointer_current;
            pointer_current = System.Windows.Forms.Cursor.Position;
            pointer_delta = new Size(pointer_current.X - pointer_previous.X,
                pointer_current.Y - pointer_previous.Y);

            System.Windows.Forms.Cursor.Position =
                new Point(Bounds.Left + (Bounds.Width / 2), Bounds.Top + (Bounds.Height / 2));
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            UpdateMousePosition();

            if (Keyboard[Key.W])
            {
                location.X += (float)Math.Cos(facing) * moveSensitivity;
                location.Z += (float)Math.Sin(facing) * moveSensitivity;
            }

            if (Keyboard[Key.S])
            {
                location.X -= (float)Math.Cos(facing) * moveSensitivity;
                location.Z -= (float)Math.Sin(facing) * moveSensitivity;
            }

            if (Keyboard[Key.A])
            {
                location.X -= (float)Math.Cos(facing + Math.PI / 2) * moveSensitivity;
                location.Z -= (float)Math.Sin(facing + Math.PI / 2) * moveSensitivity;
            }

            if (Keyboard[Key.D])
            {
                location.X += (float)Math.Cos(facing + Math.PI / 2) * moveSensitivity;
                location.Z += (float)Math.Sin(facing + Math.PI / 2) * moveSensitivity;
            }

            mouseSpeed[0] *= 0.9f;
            mouseSpeed[1] *= 0.9f;
            mouseSpeed[0] += mouseDelta.X / 1000f;
            mouseSpeed[1] -= mouseDelta.Y / 1000f;
            mouseDelta = new Vector2();

            facing += mouseSpeed[0];
            pitch += mouseSpeed[1];
            Vector3 lookatPoint = new Vector3((float)Math.Cos(facing), (float)Math.Sin(pitch), (float)Math.Sin(facing));
            cameraMatrix = Matrix4.LookAt(location, location + lookatPoint, up);

            if (Keyboard[Key.Escape])
                Exit();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }



        public static void Main(string[] args)
        {
            using (Program p = new Program())
            {
                p.Run();
            }
        }
    }
}

/*using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using GameBuilder.Infrastructure.Shaders;
using GameBuilder.Model.Constants;

namespace Tutorial 
{
    class GL4Window : GameWindow
    {
        private Matrix4 ProjectionMatrix;
        private Matrix4 WorldMatrix;
        private Matrix4 ModelviewMatrix;

        private Vector3 CameraPosition;

        private Shader shader;
        private VertexFloatBuffer buffer;

        public GL4Window(int width = 800, int height = 600)
            : base(width, height, OpenTK.Graphics.GraphicsMode.Default,
            // Window title
            WindowConstants.WindowTitle,
            // Window mode
            GameWindowFlags.Default,
            // Screen to use
            DisplayDevice.Default,
            // Target OpenGL version
            4, 0,
            // Make sure that we are only using OpenGL 4
            OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine($"OpenGL version: {GL.GetInteger(GetPName.MajorVersion)}.{GL.GetInteger(GetPName.MinorVersion)}; GLSL version: {GL.GetString(StringName.ShadingLanguageVersion)}");

            GL.ClearColor(Color.CornflowerBlue);

            //setup projection this tutorial is for 3D ill make another about 2D
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 0.5f, 10000.0f);
            WorldMatrix = new Matrix4();
            ModelviewMatrix = new Matrix4();

            CameraPosition = new Vector3(0.5f, 0.5f, 0);

            // Setup the shader
            var shaderManager = new ShaderManager();
            var vertex_source = shaderManager.LoadShader(ShaderType.VertexShader, "test1");

            //during the rasterization process each pixel that will be processed (excluding the glclearcolor)
            //to the viewport will go through this pixel shader
            //initially any processed pixel does not have an actual color
            //you can set it from here but since this example uses vertex coloring
            //the color is passed from the [vertex shader] out vec4 color -> [pixelshader] in vec4 color
            //and then output through frag_color
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

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

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

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            //render
            //this useprogram is redundant and not needed in 'this' example
            //with one shader and one object, but good practice for expanding
            //to multiple objects and shaders
            GL.UseProgram(shader.Program);
            buffer.Bind(shader);
            GL.UseProgram(0);

            SwapBuffers();
        }

        #region Shader.cs

        //This will hold our shader code in a nice clean class
        //this example only uses a shader with position and color
        //but didnt want to leave out the other bits for the shader
        //so you could practice writing a shader on your own :P
        public class Shader
        {
            public string VertexSource { get; private set; }
            public string FragmentSource { get; private set; }

            public int VertexID { get; private set; }
            public int FragmentID { get; private set; }

            public int Program { get; private set; }

            public int PositionLocation { get; set; }
            public int NormalLocation { get; set; }
            public int TexCoordLocation { get; set; }
            public int ColorLocation { get; set; }

            public Shader(ref string vs, ref string fs)
            {
                VertexSource = vs;
                FragmentSource = fs;

                Build();
            }

            private void Build()
            {
                int status_code;
                string info;

                VertexID = GL.CreateShader(ShaderType.VertexShader);
                FragmentID = GL.CreateShader(ShaderType.FragmentShader);

                // Compile vertex shader
                GL.ShaderSource(VertexID, VertexSource);
                GL.CompileShader(VertexID);
                GL.GetShaderInfoLog(VertexID, out info);
                GL.GetShader(VertexID, ShaderParameter.CompileStatus, out status_code);

                if (status_code != 1)
                    throw new ApplicationException(info);

                // Compile fragment shader
                GL.ShaderSource(FragmentID, FragmentSource);
                GL.CompileShader(FragmentID);
                GL.GetShaderInfoLog(FragmentID, out info);
                GL.GetShader(FragmentID, ShaderParameter.CompileStatus, out status_code);

                if (status_code != 1)
                    throw new ApplicationException(info);

                Program = GL.CreateProgram();
                GL.AttachShader(Program, FragmentID);
                GL.AttachShader(Program, VertexID);

                GL.LinkProgram(Program);

                GL.UseProgram(Program);
                //layout dependent locations
                PositionLocation = GL.GetAttribLocation(Program, "vertex_position");
                NormalLocation = GL.GetAttribLocation(Program, "vertex_normal");
                TexCoordLocation = GL.GetAttribLocation(Program, "vertex_texcoord");
                ColorLocation = GL.GetAttribLocation(Program, "vertex_color");

                if (PositionLocation >= 0)
                    GL.BindAttribLocation(Program, PositionLocation, "vertex_position");
                if (NormalLocation >= 0)
                    GL.BindAttribLocation(Program, NormalLocation, "vertex_normal");
                if (TexCoordLocation >= 0)
                    GL.BindAttribLocation(Program, TexCoordLocation, "vertex_texcoord");
                if (ColorLocation >= 0)
                    GL.BindAttribLocation(Program, ColorLocation, "vertex_color");

                GL.UseProgram(0);
            }

            public void Dispose()
            {
                if (Program != 0)
                    GL.DeleteProgram(Program);
                if (FragmentID != 0)
                    GL.DeleteShader(FragmentID);
                if (VertexID != 0)
                    GL.DeleteShader(VertexID);
            }
        }

        #endregion

        #region VertexFloatBuffer.cs

        public enum VertexFormat
        {
            XY,
            XY_COLOR,
            XY_UV,
            XY_UV_COLOR,

            XYZ,
            XYZ_COLOR,
            XYZ_UV,
            XYZ_UV_COLOR,
            XYZ_NORMAL_UV,
            XYZ_NORMAL_UV_COLOR
        }

        //Unlike the last vertex buffer, this one is an update
        //i ran into some problems with speed and found out it was the buffer class
        //so i rewrote to this which is x100 faster
        //if you have a hard time understanding what a VBO is you can lookat
        //http://deathbyalgorithm.blogspot.com/2013/10/opentk-vertex-buffer-object-vbo.html

        public class VertexFloatBuffer
        {
            public VertexFormat Format { get; private set; }
            public int Stride { get; private set; }
            public int AttributeCount { get; private set; }
            public int TriangleCount { get { return index_data.Length / 3; } }
            public int VertexCount { get { return vertex_data.Length / AttributeCount; } }
            public bool IsLoaded { get; private set; }
            public BufferUsageHint UsageHint { get; set; }
            public BeginMode DrawMode { get; set; }

            public int VBO { get { return id_vbo; } }
            public int EBO { get { return id_ebo; } }

            private int id_vbo;
            private int id_ebo;

            private int vertex_position;
            private int index_position;

            protected float[] vertex_data;
            protected uint[] index_data;

            public VertexFloatBuffer(VertexFormat format, int limit = 1024)
            {
                Format = format;
                SetStride();
                UsageHint = BufferUsageHint.StreamDraw;
                DrawMode = BeginMode.Triangles;

                vertex_data = new float[limit * AttributeCount];
                index_data = new uint[limit];
            }

            public void Clear()
            {
                vertex_position = 0;
                index_position = 0;
            }

            public void SetFormat(VertexFormat format)
            {
                Format = format;
                SetStride();
                Clear();
            }

            private void SetStride()
            {
                switch (Format)
                {
                    case VertexFormat.XY:
                        Stride = 8;
                        break;
                    case VertexFormat.XY_COLOR:
                        Stride = 24;
                        break;
                    case VertexFormat.XY_UV:
                        Stride = 16;
                        break;
                    case VertexFormat.XY_UV_COLOR:
                        Stride = 32;
                        break;
                    case VertexFormat.XYZ:
                        Stride = 12;
                        break;
                    case VertexFormat.XYZ_COLOR:
                        Stride = 28;
                        break;
                    case VertexFormat.XYZ_UV:
                        Stride = 20;
                        break;
                    case VertexFormat.XYZ_UV_COLOR:
                        Stride = 36;
                        break;
                    case VertexFormat.XYZ_NORMAL_UV:
                        Stride = 32;
                        break;
                    case VertexFormat.XYZ_NORMAL_UV_COLOR:
                        Stride = 48;
                        break;
                }

                AttributeCount = Stride / sizeof(float);
            }

            public void Set(float[] vertices, uint[] indices)
            {
                Clear();
                vertex_data = vertices;
                index_data = indices;
            }

            /// <summary>
            /// Load vertex buffer into a VBO in OpenGL
            /// :: store in memory
            /// </summary>
            public void Load()
            {
                if (IsLoaded) return;

                //VBO
                GL.GenBuffers(1, out id_vbo);
                GL.BindBuffer(BufferTarget.ArrayBuffer, id_vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertex_position * sizeof(float)), vertex_data, UsageHint);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                GL.GenBuffers(1, out id_ebo);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, id_ebo);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(index_position * sizeof(uint)), index_data, UsageHint);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

                IsLoaded = true;
            }

            /// <summary>
            /// Reload the buffer data without destroying the buffers pointer to OpenGL
            /// </summary>
            public void Reload()
            {
                if (!IsLoaded) return;

                GL.BindBuffer(BufferTarget.ArrayBuffer, id_vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertex_position * sizeof(float)), vertex_data, UsageHint);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, id_ebo);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(index_position * sizeof(uint)), index_data, UsageHint);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            }

            /// <summary>
            /// Unload vertex buffer from OpenGL
            /// :: release memory
            /// </summary>
            public void Unload()
            {
                if (!IsLoaded) return;

                GL.BindBuffer(BufferTarget.ArrayBuffer, id_vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertex_position * sizeof(float)), IntPtr.Zero, UsageHint);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, id_ebo);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(index_position * sizeof(uint)), IntPtr.Zero, UsageHint);

                GL.DeleteBuffers(1, ref id_vbo);
                GL.DeleteBuffers(1, ref id_ebo);

                IsLoaded = false;
            }

            public void Bind(Shader shader)
            {
                if (!IsLoaded) return;

                GL.BindBuffer(BufferTarget.ArrayBuffer, id_vbo);

                switch (Format)
                {
                    case VertexFormat.XY:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 2, VertexAttribPointerType.Float, false, Stride, 0);
                        break;
                    case VertexFormat.XY_COLOR:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.EnableVertexAttribArray(shader.ColorLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 2, VertexAttribPointerType.Float, false, Stride, 0);
                        GL.VertexAttribPointer(shader.ColorLocation, 4, VertexAttribPointerType.Float, false, Stride, 8);
                        break;
                    case VertexFormat.XY_UV:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.EnableVertexAttribArray(shader.TexCoordLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 2, VertexAttribPointerType.Float, false, Stride, 0);
                        GL.VertexAttribPointer(shader.TexCoordLocation, 2, VertexAttribPointerType.Float, false, Stride, 8);
                        break;
                    case VertexFormat.XY_UV_COLOR:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.EnableVertexAttribArray(shader.TexCoordLocation);
                        GL.EnableVertexAttribArray(shader.ColorLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 2, VertexAttribPointerType.Float, false, Stride, 0);
                        GL.VertexAttribPointer(shader.TexCoordLocation, 2, VertexAttribPointerType.Float, false, Stride, 8);
                        GL.VertexAttribPointer(shader.ColorLocation, 4, VertexAttribPointerType.Float, false, Stride, 16);
                        break;
                    case VertexFormat.XYZ:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 3, VertexAttribPointerType.Float, false, Stride, 0);
                        break;
                    case VertexFormat.XYZ_COLOR:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.EnableVertexAttribArray(shader.ColorLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 3, VertexAttribPointerType.Float, false, Stride, 0);
                        GL.VertexAttribPointer(shader.ColorLocation, 4, VertexAttribPointerType.Float, false, Stride, 12);
                        break;
                    case VertexFormat.XYZ_UV:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.EnableVertexAttribArray(shader.TexCoordLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 3, VertexAttribPointerType.Float, false, Stride, 0);
                        GL.VertexAttribPointer(shader.TexCoordLocation, 2, VertexAttribPointerType.Float, false, Stride, 12);
                        break;
                    case VertexFormat.XYZ_UV_COLOR:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.EnableVertexAttribArray(shader.TexCoordLocation);
                        GL.EnableVertexAttribArray(shader.ColorLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 3, VertexAttribPointerType.Float, false, Stride, 0);
                        GL.VertexAttribPointer(shader.TexCoordLocation, 2, VertexAttribPointerType.Float, false, Stride, 12);
                        GL.VertexAttribPointer(shader.ColorLocation, 4, VertexAttribPointerType.Float, false, Stride, 20);
                        break;
                    case VertexFormat.XYZ_NORMAL_UV:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.EnableVertexAttribArray(shader.NormalLocation);
                        GL.EnableVertexAttribArray(shader.ColorLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 3, VertexAttribPointerType.Float, false, Stride, 0);
                        GL.VertexAttribPointer(shader.NormalLocation, 3, VertexAttribPointerType.Float, false, Stride, 12);
                        GL.VertexAttribPointer(shader.TexCoordLocation, 2, VertexAttribPointerType.Float, false, Stride, 24);
                        break;
                    case VertexFormat.XYZ_NORMAL_UV_COLOR:
                        GL.EnableVertexAttribArray(shader.PositionLocation);
                        GL.EnableVertexAttribArray(shader.NormalLocation);
                        GL.EnableVertexAttribArray(shader.TexCoordLocation);
                        GL.EnableVertexAttribArray(shader.ColorLocation);
                        GL.VertexAttribPointer(shader.PositionLocation, 3, VertexAttribPointerType.Float, false, Stride, 0);
                        GL.VertexAttribPointer(shader.NormalLocation, 3, VertexAttribPointerType.Float, false, Stride, 12);
                        GL.VertexAttribPointer(shader.TexCoordLocation, 2, VertexAttribPointerType.Float, false, Stride, 24);
                        GL.VertexAttribPointer(shader.ColorLocation, 4, VertexAttribPointerType.Float, false, Stride, 32);
                        break;
                }

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, id_ebo);
                GL.DrawElements(DrawMode, index_position, DrawElementsType.UnsignedInt, 0);

                GL.DisableVertexAttribArray(0);
                GL.DisableVertexAttribArray(1);
                GL.DisableVertexAttribArray(2);
                GL.DisableVertexAttribArray(3);
            }

            public void Dispose()
            {
                Unload();
                Clear();
                vertex_data = null;
                index_data = null;
            }

            /// <summary>
            /// Add indices in order of vertices length,
            /// this is if you dont want to set indices and just index from vertex-index
            /// </summary>
            public void IndexFromLength()
            {
                int count = vertex_position / AttributeCount;
                index_position = 0;
                for (uint i = 0; i < count; i++)
                {
                    index_data[index_position++] = i;
                }
            }

            public void AddIndex(uint indexA, uint indexB, uint indexC)
            {
                index_data[index_position++] = indexA;
                index_data[index_position++] = indexB;
                index_data[index_position++] = indexC;
            }

            public void AddIndices(uint[] indices)
            {
                for (int i = 0; i < indices.Length; i++)
                {
                    index_data[index_position++] = indices[i];
                }
            }

            public void AddVertex(float x, float y)
            {
                if (Format != VertexFormat.XY)
                    throw new FormatException("vertex must be of the same format type as buffer");

                vertex_data[vertex_position++] = x;
                vertex_data[vertex_position++] = y;
            }

            public void AddVertex(float x, float y, float r, float g, float b, float a)
            {
                if (Format != VertexFormat.XY_COLOR)
                    throw new FormatException("vertex must be of the same format type as buffer");

                vertex_data[vertex_position++] = x;
                vertex_data[vertex_position++] = y;
                vertex_data[vertex_position++] = r;
                vertex_data[vertex_position++] = g;
                vertex_data[vertex_position++] = b;
                vertex_data[vertex_position++] = a;
            }

            public void AddVertex(float x, float y, float z)
            {
                if (Format != VertexFormat.XYZ)
                    throw new FormatException("vertex must be of the same format type as buffer");

                vertex_data[vertex_position++] = x;
                vertex_data[vertex_position++] = y;
                vertex_data[vertex_position++] = z;
            }

            public void AddVertex(float x, float y, float z, float r, float g, float b, float a)
            {
                if (Format != VertexFormat.XYZ_COLOR)
                    throw new FormatException("vertex must be of the same format type as buffer");

                vertex_data[vertex_position++] = x;
                vertex_data[vertex_position++] = y;
                vertex_data[vertex_position++] = z;
                vertex_data[vertex_position++] = r;
                vertex_data[vertex_position++] = g;
                vertex_data[vertex_position++] = b;
                vertex_data[vertex_position++] = a;
            }

            public void AddVertex(float x, float y, float z, float u, float v)
            {
                if (Format != VertexFormat.XYZ_UV)
                    throw new FormatException("vertex must be of the same format type as buffer");

                vertex_data[vertex_position++] = x;
                vertex_data[vertex_position++] = y;
                vertex_data[vertex_position++] = z;
                vertex_data[vertex_position++] = u;
                vertex_data[vertex_position++] = v;
            }

            public void AddVertex(float x, float y, float z, float u, float v, float r, float g, float b, float a)
            {
                if (Format != VertexFormat.XYZ_UV_COLOR)
                    throw new FormatException("vertex must be of the same format type as buffer");

                vertex_data[vertex_position++] = x;
                vertex_data[vertex_position++] = y;
                vertex_data[vertex_position++] = z;
                vertex_data[vertex_position++] = u;
                vertex_data[vertex_position++] = v;
                vertex_data[vertex_position++] = r;
                vertex_data[vertex_position++] = g;
                vertex_data[vertex_position++] = b;
                vertex_data[vertex_position++] = a;
            }

            public void AddVertex(float x, float y, float z, float nx, float ny, float nz, float u, float v)
            {
                if (Format != VertexFormat.XYZ_NORMAL_UV)
                    throw new FormatException("vertex must be of the same format type as buffer");

                vertex_data[vertex_position++] = x;
                vertex_data[vertex_position++] = y;
                vertex_data[vertex_position++] = z;
                vertex_data[vertex_position++] = nx;
                vertex_data[vertex_position++] = ny;
                vertex_data[vertex_position++] = nz;
                vertex_data[vertex_position++] = u;
                vertex_data[vertex_position++] = v;
            }

            public void AddVertex(float x, float y, float z, float nx, float ny, float nz, float u, float v, float r, float g, float b, float a)
            {
                if (Format != VertexFormat.XYZ_NORMAL_UV_COLOR)
                    throw new FormatException("vertex must be of the same format type as buffer");

                vertex_data[vertex_position++] = x;
                vertex_data[vertex_position++] = y;
                vertex_data[vertex_position++] = z;
                vertex_data[vertex_position++] = nx;
                vertex_data[vertex_position++] = ny;
                vertex_data[vertex_position++] = nz;
                vertex_data[vertex_position++] = u;
                vertex_data[vertex_position++] = v;
                vertex_data[vertex_position++] = r;
                vertex_data[vertex_position++] = g;
                vertex_data[vertex_position++] = b;
                vertex_data[vertex_position++] = a;
            }
        }

        #endregion
    }*/

    class Program
    {
        static void Main(string[] args)
        {
            var window = new EngineWindow();

            GraphicsManager.SetClearColor(Color4.Blue);

            var state = new GameState(GraphicsManager.RenderMode.Perspective, false);

            //push the state to the state stack - top state is the state that runs
            StateHandler.Push(state);

            //var sound = Assets.GetSound("Main loop.wav");
            //sound.Play();

            window.Run(30.0f);
        }
    }

    public class GameState : State
    {

        public GameState(GraphicsManager.RenderMode renderMode, bool overlay)
            : base(renderMode, overlay)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            //add a camera control for the state
            FpsCamera camera = new FpsCamera(new Vector3(0, 0, 0));
            this.SetCamera(camera);

            //add some entities to the state

            ModelEntity ent2 = new ModelEntity(0, 0, 0, "couch1.obj");
            this.AddEntity(ent2);
            ent2.SetScale(0.2f);

            SpriteEntity sprite = new SpriteEntity(20, 0, -10, "tileset.png");
            this.AddEntity(sprite);

        }

        public override void OnKeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs args)
        {
            base.OnKeyDown(sender, args);
            if (args.Key == OpenTK.Input.Key.Escape)
            {
                StateHandler.Push(new GuiState(GraphicsManager.RenderMode.Ortho, true));
            }
        }
    }

    public class GuiState : State
    {

        public GuiState(GraphicsManager.RenderMode renderMode, bool overlay)
            : base(renderMode, overlay)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            //create a bunch of UI elements and link them to the state
            ScrollPanel panel = new ScrollPanel(0, 0, 0, 400, 640, this);
            panel.SetBackgroundColor(Color4.Red);
            ScrollPanel panel2 = new ScrollPanel(420, 10, 0, 350, 500, this);

            panel.SetContentDimensions(800, 800);
            panel2.SetContentDimensions(400, 400);

            Button button = new Button(10, 50, 0, 100, 100, "button.png", "button", this);
            TextField field = new TextField(10, 10, 0, 100, this);
            ListBox listBox = new ListBox(120, 10, 0, 200, 150, 10, this);

            DropDownBox dropDown = new DropDownBox(50, 420, 0, 100, this);

            TextBox textBox = new TextBox(10, 10, 0, 400, 400, this);
            RadioButton radio = new RadioButton(200, 420, 0, this);

            panel.AddControl(panel2);

            panel.AddControl(dropDown);
            panel.AddControl(textBox);
            panel.AddControl(radio);

            panel2.AddControl(button);
            panel2.AddControl(field);
            panel2.AddControl(listBox);

            this.AddControl(panel);

        }

        public override void OnKeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs args)
        {
            base.OnKeyDown(sender, args);
            if (args.Key == OpenTK.Input.Key.Escape)
            {
                StateHandler.Pop();
            }
        }

    }
}

