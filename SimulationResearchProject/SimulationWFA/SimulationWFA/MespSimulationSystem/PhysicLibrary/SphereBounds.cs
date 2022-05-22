using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using PhysicLibrary;

namespace PhysicLibrary
{
    public class SphereBounds : Bounds
    {
        public float radius;

        public SphereBounds()
        {
            boundType = BoundType.Sphere;
        }

        public override bool IsIntersectWith(Vector3 point)
        {
            return Vector3.Distance(Center, point) < radius;
        }

        public override bool IsIntersectWith(Bounds bound)
        {
            if(bound.boundType == BoundType.Sphere)
            {
                SphereBounds sphereBound = bound as SphereBounds;

                var distanceVec = Center - sphereBound.Center;
                if (distanceVec.Length() >= radius + sphereBound.radius) return false;
                return true;
            }

            return false;
        }

        public override bool IsIntersectWith(Ray ray, float distance, out Vector3 hitPoint, bool isInfinite = false)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBounds()
        {
            
        }
    }
}
