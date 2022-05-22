using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MespSimulationSystem.Math
{
    public static class VectorExtensions
    {
        public static void Normalize(this Vector3 vect)
        {
            vect = vect / vect.Length();
        }

        public static Vector3 normalized(this Vector3 vect)
        {
            return vect / vect.Length();
        }

        public static Vector2 normalized(this Vector2 vect)
        {
            return vect / vect.Length();
        }

        public static float magnitude(this Vector3 vect)
        {
            return (float)System.Math.Sqrt((vect.X * vect.X + vect.Y * vect.Y + vect.Z * vect.Z));
        }

        public static Vector3 Round(this Vector3 vect, int digit)
        {
            return new Vector3((float)System.Math.Round(vect.X, digit),
                (float)System.Math.Round(vect.Y, digit),
                (float)System.Math.Round(vect.Z, digit)
                );
        }

    }
}
