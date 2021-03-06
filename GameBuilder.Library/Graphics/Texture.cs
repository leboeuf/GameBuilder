﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace GameBuilder.Library.Graphics
{
    public class Texture
    {
        private int _textureID, _width, _height;

        public Texture(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                Bitmap TextureBitmap = new Bitmap(filename);
                _width = TextureBitmap.Width;
                _height = TextureBitmap.Height;
                BitmapData TextureData = TextureBitmap.LockBits(new System.Drawing.Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                _textureID = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, _textureID);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, TextureData.Width, TextureData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, TextureData.Scan0);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapNearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                TextureBitmap.UnlockBits(TextureData);
            }
            else
            {
                throw new Exception("Could not find file " + filename);
            }
        }

        public int GetWidth()
        {
            return _width;
        }

        public int GetHeight()
        {
            return _height;
        }

        public void Bind()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, _textureID);
            int uTextureSamplerLocation = GraphicsManager.GetShader().GetUniformLocation("uTextureSampler");
            GL.Uniform1(uTextureSamplerLocation, 0);
            int uTextureFlagLocation = GraphicsManager.GetShader().GetUniformLocation("uTextureFlag");
            GL.Uniform1(uTextureFlagLocation, 1.0f);
        }

        public static void BindNone()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            int uTextureFlagLocation = GraphicsManager.GetShader().GetUniformLocation("uTextureFlag");
            GL.Uniform1(uTextureFlagLocation, 0.0f);
        }

        public void Delete()
        {
            GL.DeleteTexture(_textureID);
        }
    }
}
