using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using GameBuilder.Infrastructure;

namespace GameBuilder
{
    public class Game : GameWindow
    {
        private readonly GameManager _gameManager;

        public Game(int width, int height)
            : base(width, height)
        {
            _gameManager = new GameManager();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.CornflowerBlue);

            _gameManager.Draw();

            this.SwapBuffers();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
    }
}
