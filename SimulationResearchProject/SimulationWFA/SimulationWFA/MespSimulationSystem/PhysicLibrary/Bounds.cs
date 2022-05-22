using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicLibrary
{
    public enum BoundType { Box, Sphere }
    public abstract class Bounds
    {
        private Vector3 center;
        public Vector3 offset;

        public Vector3 Center {
            get {
                return center;
            }
            set {
                UpdateCenter(value);
                UpdateBounds();
            }
        }
        public BoundType boundType;

        public void UpdateCenter(Vector3 newCenter)
        {
            center = newCenter+ offset;
        }

        public abstract void UpdateBounds();

        public abstract bool IsIntersectWith(Vector3 point);
        public abstract bool IsIntersectWith(Bounds bound);
        public abstract bool IsIntersectWith(Ray ray,float distance, out Vector3 hitPoint, bool isInfinite = false);

    }
}
