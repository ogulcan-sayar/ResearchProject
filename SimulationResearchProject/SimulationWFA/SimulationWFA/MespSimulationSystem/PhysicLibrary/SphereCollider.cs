using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;
using ProgramLibrary;

namespace PhysicLibrary
{
    public class SphereCollider : Collider
    {
        public SphereCollider()
        {
            bound = new SphereBounds();
        }

        public override void DrawGizmos()
        {
            MespDebug.DrawWireSphere(bound.Center, (bound as SphereBounds).radius, 32);
        }

        public override bool IsIntersectWith(Bounds bound, out Contact contact)
        {
            contact = new Contact();
            var result = this.bound.IsIntersectWith(bound);

            if (!result) return result;

            if (bound.boundType == BoundType.Sphere)
            {
                SphereBounds otherBound = bound as SphereBounds;

                var distanceVec = this.bound.Center - bound.Center;
                contact.contactNormal = distanceVec.normalized();

                contact.penetration = (bound as SphereBounds).radius + otherBound.radius - distanceVec.Length();
                return true;
            }

           

            return result;
        }

        public override bool IsIntersectWith(Ray ray, float distance,out Vector3 hitPoint, bool isInfinite = false)
        {
            hitPoint = new Vector3();
            return true;
        }

        public override void Update(Vector3 centerPos)
        {
            bound.UpdateCenter(centerPos);
        }
    }
}
