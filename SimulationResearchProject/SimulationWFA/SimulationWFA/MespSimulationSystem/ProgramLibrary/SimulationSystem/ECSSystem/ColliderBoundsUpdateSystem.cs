using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using ProgramLibrary;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class ColliderBoundsUpdateSystem : Dalak.Ecs.System
    {
        Filter<ColliderComp,TransformComp> colliderFilter = null;

        public override void Awake()
        {
            foreach (var b in colliderFilter)
            {
                ref var colliderComp = ref colliderFilter.Get1(b);
                ref var transformComp = ref colliderFilter.Get2(b);

                var entity = colliderFilter.GetEntity(b);

                var center = transformComp.transform.position;
                if (entity.HasComponent<ParticleComp>()) center = entity.GetComponent<ParticleComp>().particle.position;

                colliderComp.collider.Update(center);
            }

        }

        public override void FixedUpdate()
        {
            foreach(var b in colliderFilter)
            {
                ref var colliderComp = ref colliderFilter.Get1(b);
                ref var transformComp = ref colliderFilter.Get2(b);

                var entity = colliderFilter.GetEntity(b);

                var center = transformComp.transform.position;
                if (entity.HasComponent<ParticleComp>()) center = entity.GetComponent<ParticleComp>().particle.position;

                colliderComp.collider.Update(center);
            }
        }


        public override void Update()
        {
            foreach (var b in colliderFilter)
            {
                ref var colliderComp = ref colliderFilter.Get1(b);
                ref var transformComp = ref colliderFilter.Get2(b);

                var entity = colliderFilter.GetEntity(b);

                var center = transformComp.transform.position;
                if (entity.HasComponent<ParticleComp>()) center = entity.GetComponent<ParticleComp>().particle.position;

                colliderComp.collider.Update(center);
            }
        }

        //public override void PostRender()
        //{
        //    foreach (var b in colliderFilter)
        //    {
        //        ref var boxColliderComp = ref colliderFilter.Get1(b);
        //        ref var transformComp = ref colliderFilter.Get2(b);

        //        boxColliderComp.collider.DrawGizmos();
        //    }

        //}
    }


}
