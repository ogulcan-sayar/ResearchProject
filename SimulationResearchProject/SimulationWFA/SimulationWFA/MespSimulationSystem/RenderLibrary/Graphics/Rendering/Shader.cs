using System;
using System.Numerics;
using MESPSimulationSystem.Math;
using RenderLibrary.DLL;
using static RenderLibrary.Shaders.ShaderPool;

namespace RenderLibrary.Graphics.Rendering
{
    public class Shader
    {
        private IntPtr shaderAdress;
        public ShaderType shaderType;

        public Shader(IntPtr shaderAdress)
        {
            this.shaderAdress = shaderAdress;
        }
        
        public Shader(string vertexShaderPath,string fragmentShaderPath, ShaderType shaderType)
        {
            this.shaderType = shaderType;
            shaderAdress = RenderProgramDLL.NewShader(vertexShaderPath, fragmentShaderPath);
        }

        public void Activate()
        {
            RenderProgramDLL.ShaderActivate(shaderAdress);
        }
        
        public void SetMat4(string name, Mat4 mat4)
        {
            RenderProgramDLL.ShaderSetMat4(shaderAdress,name,mat4.matrixAdress);
        }
        
        public void SetInt(string name, int value)
        {
            RenderProgramDLL.ShaderSetInt(shaderAdress,name,value);
        }
        
        public void SetFloat(string name, float value)
        {
            RenderProgramDLL.ShaderSetFloat(shaderAdress,name,value);
        }
        
        public void Set3Float(string name, Vector3 vector3)
        {
            RenderProgramDLL.ShaderSet3Float(shaderAdress,name,vector3.X,vector3.Y,vector3.Z);
        }
        
        public void Set3Float(string name, float value, float value1, float value2)
        {
            RenderProgramDLL.ShaderSet3Float(shaderAdress,name,value,value1,value2);
        }
        
        public void Set4Float(string name, Vector4 vector4)
        {
            RenderProgramDLL.ShaderSet4Float(shaderAdress,name,vector4.X,vector4.Y,vector4.Z,vector4.W);
        }
        
        public void Set4Float(string name, float value, float value1, float value2,
            float value3)
        {
            RenderProgramDLL.ShaderSet4Float(shaderAdress,name,value,value1,value2,value3);
        }
        
        public void SetBool(string name, bool value)
        {
            RenderProgramDLL.ShaderSetBool(shaderAdress,name,value);
        }
        
        public IntPtr GetShaderAdress()
        {
            return shaderAdress;
        }
    }
}
