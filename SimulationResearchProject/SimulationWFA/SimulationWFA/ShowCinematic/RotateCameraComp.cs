using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimulationWFA.ShowCinematic
{
    public struct RotateCameraComp
    {
        public float rotationSpeed;
        public float currentRotation;
        public Vector3 oldPos;
        public Vector3 oldRotate;
    }
}
