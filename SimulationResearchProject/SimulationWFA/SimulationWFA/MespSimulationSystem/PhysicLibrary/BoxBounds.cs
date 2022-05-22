using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;

namespace PhysicLibrary
{
    public class BoxBounds : Bounds
    {
        // explanation : Vector2.x means minX, Vector2.y means maxX;

        private Vector3 size = Vector3.One;

        public Vector3 Size {
            get {
                return size;
            }
            set {
                size = value;
                UpdateBounds();
            }
        }

        public Vector2 xPoints;
        public Vector2 yPoints;
        public Vector2 zPoints;

        public BoxBounds()
        {
            this.boundType = BoundType.Box;
        }

        public override void UpdateBounds()
        {
            xPoints.X = Center.X - size.X/2;
            xPoints.Y = Center.X + size.X/2;
                                         
            yPoints.X = Center.Y - size.Y/2;
            yPoints.Y = Center.Y + size.Y/2;
                                         
            zPoints.X = Center.Z - size.Z/2;
            zPoints.Y = Center.Z + size.Z/2;
        }

        //AABB
        public override bool IsIntersectWith(Vector3 point)
        {
            return (point.X >= xPoints.X && point.X <= xPoints.Y) &&
                    (point.Y >= yPoints.X && point.Y <= yPoints.Y) &&
                    (point.Z >= zPoints.X && point.Z <= zPoints.Y);
        }

        public override bool IsIntersectWith(Bounds bound)
        {
            if(bound.boundType == BoundType.Box)
            {
                BoxBounds otherBoxBound = bound as BoxBounds;
                return (otherBoxBound.xPoints.X <= xPoints.Y && otherBoxBound.xPoints.Y >= xPoints.X) &&
                        (otherBoxBound.yPoints.X <= yPoints.Y && otherBoxBound.yPoints.Y >= yPoints.X) &&
                        (otherBoxBound.zPoints.X <= zPoints.Y && otherBoxBound.zPoints.Y >= zPoints.X);
            }

            return false;
        }

        public override bool IsIntersectWith(Ray ray,float distance, out Vector3 hitPoint, bool isInfinite =false)
        {
            hitPoint = new Vector3(-9999, -9999, -9999);
            ray.direction.Normalize();

            float t1 = (xPoints.X - ray.origin.X) / ray.direction.X;
            float t2 = (xPoints.Y - ray.origin.X) / ray.direction.X;
            float t3 = (yPoints.X - ray.origin.Y) / ray.direction.Y;
            float t4 = (yPoints.Y - ray.origin.Y) / ray.direction.Y;
            float t5 = (zPoints.X - ray.origin.Z) / ray.direction.Z;
            float t6 = (zPoints.Y - ray.origin.Z) / ray.direction.Z;

            float tmin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));
            float tmax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

            if (tmax < 0)
            {
                return false;
            }

            // if tmin > tmax, ray doesn't intersect AABB
            if (tmin > tmax)
            {
                return false;
            }

            if (tmin < 0f)
            {
                hitPoint = ray.origin + ray.direction * tmax;
                return true;
            }

            if(tmin<= distance || isInfinite)
            {
                hitPoint = ray.origin + ray.direction * tmin;
                return true;
            }

            return false;

        }
    }
}
