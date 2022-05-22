using System;
using System.Numerics;
using Dalak.Ecs;
using PhysicLibrary;
using ProgramLibrary;
using SimulationSystem.ECSComponents;

namespace SimulationSystem.Systems
{
    public class SimulationPhysicEntegrationSystem : Dalak.Ecs.System
    {
        readonly Filter<ColliderComp> colliderFilter = null;

        public override void Awake()
        {
            Physics.colliderEntityList.Clear();

            foreach (var c in colliderFilter)
            {
                var entity = colliderFilter.GetEntity(c);
                Physics.colliderEntityList.Add(entity);

                ref var colliderComp = ref colliderFilter.Get1(c);
                colliderComp.collider.physicsLayer = PhysicsLayer.defaultLayer;
            }
        }

        public override void FixedUpdate()
        {
            Physics.colliderEntityList.Clear();

            foreach (var c in colliderFilter)
            {
                var entity = colliderFilter.GetEntity(c);
                Physics.colliderEntityList.Add(entity);
            }
        }

    }
}
