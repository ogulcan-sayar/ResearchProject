using System.Numerics;
using MespSimulationSystem.Math;
using RenderLibrary.DLL;

namespace MESPSimulationSystem.Math
{
    public static class MathFunctions
    {
        public static double ConvertToRadians(double angle)
        {
            return (System.Math.PI / 180) * angle;
        }

        public static double RadianToDegree(double radian)
        {
            return (180 /System.Math.PI) * radian;
        }

        public static Vector3 Rotate(Mat4 modelMatrix, float degree, Vector3 axisOfRot, Vector3 direction)
        {
            float[] returnArr = { direction.X, direction.Y, direction.Z };
            RenderProgramDLL.Rotate(modelMatrix.matrixAdress, degree,
                new[] { axisOfRot.X, axisOfRot.Y, axisOfRot.Z }, returnArr);

            return new Vector3(returnArr[0], returnArr[1], returnArr[2]);
        }

        public static Vector3 MoveTowards(Vector3 start, Vector3 end, float movement)
        {
            Vector3 dir = (end - start).normalized();

            return start + movement * dir;

        }

        public static double AngleBetween(Vector3 vector1, Vector3 vector2)
        {
            double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
            double cos = vector1.X * vector2.Y + vector1.Y * vector2.X;

            return System.Math.Atan2(sin, cos) * (180 / System.Math.PI);
        }

        public static int RoundToInt(float value)
        {
            int valueCastInt = (int)value;

            float floatingPoint = valueCastInt + 1 - value;

            if (.5f - floatingPoint >=0)
            {
                return valueCastInt + 1;
            }
            else
            {
                return valueCastInt;
            }

        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }
        public static float Clamp(float value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        public static float InverseLerp(float a, float b, float value)
        {
            if (a != b)
                return Clamp((value - a) / (b - a), 0, 1);
            else
                return 0.0f;
        }

    }
}