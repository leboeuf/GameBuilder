﻿using GameBuilder.Library.Utilities;
using OpenTK.Graphics.OpenGL;

namespace GameBuilder.Library.Graphics
{
    public class Shader
    {
        private ShaderUtility _shaderUtility;
        private static int current = 0; 

        public Shader(string vertShader, string fragShader)
        {
            _shaderUtility = new ShaderUtility(vertShader, fragShader);
        }

        public int GetID()
        {
            return _shaderUtility.ShaderProgramID;
        }

        public int GetAttribLocation(string variable)
        {
            return GL.GetAttribLocation(_shaderUtility.ShaderProgramID, variable);
        }

        public int GetUniformLocation(string variable)
        {
            return GL.GetUniformLocation(_shaderUtility.ShaderProgramID, variable);
        }

        public virtual void Bind()
        {
            GL.UseProgram(_shaderUtility.ShaderProgramID);
        }

        public void Delete()
        {
            _shaderUtility.Delete();
        }

    }
}
