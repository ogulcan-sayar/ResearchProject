using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;
using SimulationWFA.MespUtils;

namespace SimulationSystem
{
    public class TriggerDetectionSystem : Dalak.Ecs.System
    {
        readonly Filter<ParticleComp, ColliderComp, TriggerComp> rigidTriggerFilter = null;
        readonly Filter<ColliderComp>.Exclude<ParticleComp, TriggerComp> onlyColliderFilter = null;

        readonly Filter<ParticleComp, ColliderComp>.Exclude<TriggerComp> rigidNonTriggerFilter = null;
        readonly Filter<TriggerComp, ColliderComp>.Exclude<ParticleComp> onlyTriggerFilter = null;

        readonly Filter<TriggerComp> triggerFilter = null;


        List<Entity> newAddedEntityList;
        List<Entity> exitEntityList;

        public override void Awake()
        {
            newAddedEntityList = new List<Entity>();
            exitEntityList = new List<Entity>();
        }

        public override void FixedUpdate()
        {
            foreach (var r in rigidTriggerFilter)
            {
                ref var particleComp = ref rigidTriggerFilter.Get1(r);
                ref var colliderComp = ref rigidTriggerFilter.Get2(r);
                ref var triggerComp = ref rigidTriggerFilter.Get3(r);

                foreach (var o in onlyColliderFilter)
                {
                    ref var otherColliderComp = ref onlyColliderFilter.Get1(o);

                    if (colliderComp.collider.IsIntersectWith(otherColliderComp.collider.bound, out var contact))
                    {
                        triggerComp.collidedThisFrame.Add(onlyColliderFilter.GetEntity(o));
                    }
                }
            }

            foreach (var r in rigidNonTriggerFilter)
            {
                ref var particleComp = ref rigidNonTriggerFilter.Get1(r);
                ref var colliderComp = ref rigidNonTriggerFilter.Get2(r);
                

                foreach (var o in onlyTriggerFilter)
                {
                    ref var otherColliderComp = ref onlyTriggerFilter.Get2(o);
                    ref var triggerComp = ref onlyTriggerFilter.Get1(o);

                    if (colliderComp.collider.IsIntersectWith(otherColliderComp.collider.bound, out var contact))
                    {
                        triggerComp.collidedThisFrame.Add(rigidNonTriggerFilter.GetEntity(r));
                    }
                }
            }

            foreach (var r in rigidTriggerFilter)
            {
                ref var particleComp = ref rigidTriggerFilter.Get1(r);
                ref var colliderComp = ref rigidTriggerFilter.Get2(r);
                ref var triggerComp = ref rigidTriggerFilter.Get3(r);

                foreach (var o in rigidNonTriggerFilter)
                {

                    ref var otherparticleComp = ref rigidNonTriggerFilter.Get1(o);
                    ref var othercolliderComp = ref rigidNonTriggerFilter.Get2(o);

                    if (colliderComp.collider.IsIntersectWith(othercolliderComp.collider.bound, out var contact))
                    {
                        triggerComp.collidedThisFrame.Add(rigidNonTriggerFilter.GetEntity(o));
                    }
                }
            }

            foreach(var t in triggerFilter)
            {
                newAddedEntityList.Clear();
                exitEntityList.Clear();

                ref var triggerComp = ref triggerFilter.Get1(t);
                var triggerEntity = triggerFilter.GetEntity(t);

                if (triggerEntity.HasComponent<OnTriggerEnterComp>())
                {
                    triggerEntity.RemoveComponent<OnTriggerEnterComp>();
                }

                if (triggerEntity.HasComponent<OnExitTriggerComp>())
                {
                    triggerEntity.RemoveComponent<OnExitTriggerComp>();
                }

                for (int i = 0; i < triggerComp.collidedThisFrame.Count; i++)
                {
                    if(!triggerComp.collidedEntityList.Contains(triggerComp.collidedThisFrame[i]))
                    {
                        newAddedEntityList.Add(triggerComp.collidedThisFrame[i]);
                    }
                }

                for (int i = 0; i < triggerComp.collidedEntityList.Count; i++)
                {
                    if (!triggerComp.collidedThisFrame.Contains(triggerComp.collidedEntityList[i]))
                    {
                        exitEntityList.Add(triggerComp.collidedEntityList[i]);
                    }
                }

                if (newAddedEntityList.Count != 0)
                {
                    triggerEntity.AddComponent<OnTriggerEnterComp>() = new OnTriggerEnterComp() {
                        collidedEntityList = newAddedEntityList,
                    };

                    triggerComp.collidedEntityList.CopyFrom(newAddedEntityList);

                }

                if (exitEntityList.Count != 0)
                {
                    triggerEntity.AddComponent<OnExitTriggerComp>() = new OnExitTriggerComp() {
                        collidedEntityList = exitEntityList,
                    };

                    for(int i = 0; i < exitEntityList.Count; i++)
                    {
                        triggerComp.collidedEntityList.Remove(exitEntityList[i]);
                    }
                }


                triggerComp.collidedThisFrame.Clear();

            }
        }  
    }
}
