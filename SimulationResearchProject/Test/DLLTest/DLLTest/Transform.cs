using System;
using System.Numerics;
using MESPSimulation.DLL;

namespace DLLTest
{
    public class Transform
    {
        private IntPtr transformAdress;
        private Vector3 refPosition;
        private Vector3 refSize;
        private Vector3 refRotation;

        public Vector3 position
        {
            get
            {
                return refPosition;
            }
            set
            {
                refPosition = value;
                SetPosition(refPosition);
            }
        }
        
        public Vector3 size
        {
            get
            {
                return refSize;
            }
            set
            {
                refSize = value;
                SetSize(refSize);
            }
        }
        
        public Vector3 rotation
        {
            get
            {
                return refRotation;
            }
            set
            {
                refRotation = value;
                SetRotation(refRotation);
            }
        }

        public Transform()
        {
            transformAdress = RenderProgramDLL.NewTransform();
            position = Vector3.Zero;
            size = Vector3.Zero;
            rotation = Vector3.Zero;
        }
        
        public Transform(Vector3 position, Vector3 size, Vector3 rotation)
        {
            transformAdress = RenderProgramDLL.NewTransform();
            this.position = position;
            this.size = size;
            this.rotation = rotation;
        }

        private void SetPosition(Vector3 newPos)
        {
            float[] posF = new[] {newPos.X, newPos.Y, newPos.Z};
            RenderProgramDLL.SetTransformPosition(transformAdress,posF);
        }
        
        private void SetSize(Vector3 newSize)
        {
            float[] posF = new[] {newSize.X, newSize.Y, newSize.Z};
            RenderProgramDLL.SetTransformSize(transformAdress,posF);
        }
        
        private void SetRotation(Vector3 newRot)
        {
            float[] posF = new[] {newRot.X, newRot.Y, newRot.Z};
            RenderProgramDLL.SetTransformRotation(transformAdress,posF);
        }

        public IntPtr GetAdress()
        {
            return transformAdress;
        }
    }
}