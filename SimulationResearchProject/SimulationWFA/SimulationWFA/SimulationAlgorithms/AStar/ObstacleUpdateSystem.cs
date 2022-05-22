using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    class ObstacleUpdateSystem : Dalak.Ecs.System
    {
        readonly Filter<ObstacleComp, ColliderComp,TransformComp> obstacleFilter = null;
        readonly Filter<GridComp, TransformComp> gridFilter = null;
        readonly Filter<UnitComp, ColliderComp> unitFollowFilter = null;

        public override void Awake()
        {
            foreach(var o in obstacleFilter)
            {
                ref var obstacleComp = ref obstacleFilter.Get1(o);
                ref var colliderComp = ref obstacleFilter.Get2(o);
                ref var transformComp = ref obstacleFilter.Get3(o);
                colliderComp.collider.physicsLayer = PhysicLibrary.PhysicsLayer.unwalkableLayer;
                UpdateObstacle(transformComp.transform, ref obstacleComp,ref colliderComp);
            }

            foreach(var u in unitFollowFilter)
            {
                ref var colliderComp = ref unitFollowFilter.Get2(u);
                (colliderComp.collider.bound as BoxBounds).offset = new System.Numerics.Vector3(0, -0.4f, 0);
            }
        }

        public override void Update()
        {
            bool changed = false;
            foreach (var o in obstacleFilter)
            {
                ref var obstacleComp = ref obstacleFilter.Get1(o);
                ref var colliderComp = ref obstacleFilter.Get2(o);
                ref var transformComp = ref obstacleFilter.Get3(o);
               
                if(IsTransformChanged(transformComp.transform,ref obstacleComp, ref colliderComp))
                {
                    UpdateObstacle(transformComp.transform, ref obstacleComp,ref colliderComp);
                    colliderComp.collider.physicsLayer = PhysicLibrary.PhysicsLayer.unwalkableLayer;
                    changed = true;
                }   
            }

            if (changed)
            {
                foreach(var g in gridFilter)
                {
                    ref var gridComp = ref gridFilter.Get1(g);
                    ref var transformComp = ref gridFilter.Get2(g);

                    BoxBounds unitBoxBound = unitFollowFilter.Get2(0).collider.bound as BoxBounds;
                    gridComp.grid.CreateGrid(transformComp.transform.position, unitBoxBound);
                }
            }

        }

        public bool IsTransformChanged(Transform transform, ref ObstacleComp obstacleComp, ref ColliderComp colliderComp)
        {
            if (transform.position != obstacleComp.position) return true;
            if (transform.rotation != obstacleComp.rotate) return true;
            if (transform.scale != obstacleComp.scale) return true;
            if ((colliderComp.collider.bound as BoxBounds).Size != obstacleComp.colliderSize) return true;
            return false;
        }

        public void UpdateObstacle(Transform transform, ref ObstacleComp obstacleComp, ref ColliderComp colliderComp)
        {
            obstacleComp.position = transform.position;
            obstacleComp.rotate = transform.rotation;
            obstacleComp.scale = transform.scale;
            obstacleComp.colliderSize = (colliderComp.collider.bound as BoxBounds).Size;
        }

    }
}
