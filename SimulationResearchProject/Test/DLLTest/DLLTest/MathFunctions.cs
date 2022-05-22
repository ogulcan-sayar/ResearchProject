using System;
using System.Numerics;
using MESPSimulation.DLL;

namespace MESPLibrary.MESPMath
{
    public static class MathFunctions
    {
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static Vector3 Rotate(Mat4 modelMatrix, float degree, Vector3 axisOfRot, Vector3 direction)
        {
            float[] returnArr =  {direction.X, direction.Y, direction.Z};
            RenderProgramDLL.Rotate(modelMatrix.matrixAdress, degree,
                new[] {axisOfRot.X, axisOfRot.Y, axisOfRot.Z}, returnArr);
            
            return new Vector3(returnArr[0],returnArr[1],returnArr[2]);
        }
    }
}