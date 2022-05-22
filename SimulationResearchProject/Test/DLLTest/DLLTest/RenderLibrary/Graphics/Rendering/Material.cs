using System;
using MESPSimulation.DLL;
using MESPSimulation.Graphics.Rendering;

namespace DLLTest
{
    public class Material
    {
        protected IntPtr materialAdress;

        public void SetShader(Shader shader)
        {
            RenderProgramDLL.SetShaderToMaterial(materialAdress,shader.GetShaderAdress());
        }
        
        public void SetAdress(IntPtr materialAdress)
        {
            this.materialAdress = materialAdress;
        }

        public IntPtr GetAdress()
        {
            return materialAdress;
        }
    }
}