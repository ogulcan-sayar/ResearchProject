using System;
using RenderLibrary.DLL;

namespace MESPSimulationSystem.Math
{
    public class Mat4
    {
        public IntPtr matrixAdress;

        public Mat4()
        {
            
        }
        
        public Mat4(float value)
        {
            matrixAdress = RenderProgramDLL.ReturnMat4(value);
        }

        
    }
    
}