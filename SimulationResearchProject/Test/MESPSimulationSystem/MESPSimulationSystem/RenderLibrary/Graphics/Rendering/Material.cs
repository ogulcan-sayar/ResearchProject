using System;
using RenderLibrary.DLL;

namespace RenderLibrary.Graphics.Rendering
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