using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicLibrary
{
    public class PhysicsLayer
    {

        public int layerId;
        public static PhysicsLayer defaultLayer = new PhysicsLayer(-1);
        public static PhysicsLayer grassLayer = new PhysicsLayer(0);
        public static PhysicsLayer roadLayer = new PhysicsLayer(1);
        public static PhysicsLayer unwalkableLayer = new PhysicsLayer(2);

        public PhysicsLayer(int layerId)
        {
            this.layerId = layerId;
        }

    }
    public abstract class Collider
    {
        public float restitution = 1f;
        public Bounds bound;
        public PhysicsLayer physicsLayer = PhysicsLayer.defaultLayer;

        public abstract bool IsIntersectWith(Bounds bound, out Contact contact);
        public abstract bool IsIntersectWith(Ray ray, float distance, out Vector3 hitPoint, bool isInfinite = false);
        public abstract void Update(Vector3 centerPos);
        public abstract void DrawGizmos();

    }
}
