using System;
using GameBuilder.Library.Graphics;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GameBuilder.Library
{
    public class EngineWindow : GameWindow
    {
        AudioContext AC;

        public EngineWindow(string windowTitle, int width = 1200, int height = 800, GameWindowFlags wameWindowFlags = GameWindowFlags.Default)
            : base(width, height, new GraphicsMode(32, 24, 8, 4), windowTitle, wameWindowFlags, DisplayDevice.Default, 4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            Global.Window = this;
            Console.WriteLine($"OpenGL version: {GL.GetInteger(GetPName.MajorVersion)}.{GL.GetInteger(GetPName.MinorVersion)}; GLSL version: {GL.GetString(StringName.ShadingLanguageVersion)}");

            //AC = new AudioContext();
        }

        protected override void OnLoad(EventArgs e)
        {
            GraphicsManager.Initialize();

            MouseDown += (sender, args) => StateHandler.OnMouseDown(sender, args);
            MouseUp += (sender, args) => StateHandler.OnMouseUp(sender, args);
            MouseMove += (sender, args) => StateHandler.OnMouseMove(sender, args);
            MouseWheel += (sender, args) => StateHandler.OnMouseWheel(sender, args);
            KeyDown += (sender, args) => StateHandler.OnKeyDown(sender, args);
            KeyUp += (sender, args) => StateHandler.OnKeyUp(sender, args);
            KeyPress += (sender, args) => StateHandler.OnKeyPress(sender, args);

            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GraphicsManager.SetProjection(this.ClientRectangle);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Input.Update();
            StateHandler.UpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GraphicsManager.Clear();

            StateHandler.RenderFrame(e);

            this.SwapBuffers();
        }

        protected override void OnUnload(EventArgs e)
        {
            Assets.Delete();
            GraphicsManager.Destroy();
            base.OnUnload(e);
        }
    }
}
