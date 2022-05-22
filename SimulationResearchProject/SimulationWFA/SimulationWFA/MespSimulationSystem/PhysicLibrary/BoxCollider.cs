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
    public enum BoxColliderNormalDirection
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        FORWARD,
        BACKWARD,
    };

    public class BoxCollider : Collider
    {

        public static Vector3[] potentialNormals = new Vector3[] {

            new Vector3(0,1,0),
            new Vector3(1,0,0),
            new Vector3(0,-1,0),
            new Vector3(-1,0,0),
            new Vector3(0,0,1),
            new Vector3(0,0,-1),

        };

        static Vector3[] faces = {
            new Vector3 ( -1 , 0 , 0),
            new Vector3 ( 1 , 0 , 0),
            new Vector3 ( 0 , -1 , 0),
            new Vector3 ( 0 , 1 , 0) ,
            new Vector3 ( 0 , 0 , -1),
            new Vector3 ( 0 , 0 , 1)
        };

        public BoxCollider()
        {
            bound = new BoxBounds();
        }

        public override bool IsIntersectWith(Bounds otherBound, out Contact contact)
        {
            contact = new Contact();
            var result = this.bound.IsIntersectWith(otherBound);

            if (!result) return result;

            if (bound.boundType == BoundType.Box)
            {
                BoxBounds otherboxBound = otherBound as BoxBounds;
                BoxBounds thisBound = bound as BoxBounds;

                Vector3 maxA = otherboxBound.Center + otherboxBound.Size/2;
                Vector3 minA = otherboxBound.Center - otherboxBound.Size/2;

                Vector3 maxB = thisBound.Center + thisBound.Size/2;
                Vector3 minB = thisBound.Center - thisBound.Size/2;

                float[] distances =
                {
                    (maxB.X - minA.X) ,
                    (maxA.X - minB.X) ,
                    (maxB.Y - minA.Y) ,
                    (maxA.Y - minB.Y) ,
                    (maxB.Z - minA.Z) ,
                    (maxA.Z - minB.Z)
                };

                float penetration = float.MaxValue;
                Vector3 bestAxis = new Vector3();

                for (int i = 0; i < 6; i++)
                {
                    if (distances[i] < penetration)
                    {
                        penetration = distances[i];
                        bestAxis = faces[i];
                    }
                }

                contact.contactNormal = bestAxis;
                contact.penetration = penetration;

                return true;
            }



            return result;
        }
         
        public override bool IsIntersectWith(Ray ray,float distance, out Vector3 hitPoint, bool isInfinite = false)
        {
            return bound.IsIntersectWith(ray,distance,out hitPoint, isInfinite);
        }

        BoxColliderNormalDirection GetNormal(Vector3 target)
        {
            float max = 0.0f;
            int best_match = -1;
            for (int i = 0; i < 6; i++)
            {
                float dot_product = Vector3.Dot(target.normalized(), potentialNormals[i]);
                if (dot_product > max)
                {
                    max = dot_product;
                    best_match = i;
                }
            }
            return (BoxColliderNormalDirection)best_match;
        }

        public override void Update(Vector3 centerPos)
        {
            bound.UpdateCenter(centerPos);
            bound.UpdateBounds();
        }

        public override void DrawGizmos()
        {
            var color = new Vector3(0, 1, 0);
            MespDebug.DrawWireBox((bound as BoxBounds), color);
        }

        
    }
}
