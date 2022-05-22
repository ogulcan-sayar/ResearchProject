using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using ProgramLibrary;
using RenderLibrary.IO;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class CollisionDetectionSystem : Dalak.Ecs.System
    {
        readonly Filter<ParticleComp, ColliderComp>.Exclude<TriggerComp> bothRigidFilter = null;
        readonly Filter<ColliderComp>.Exclude<ParticleComp, TriggerComp> onlyColliderFilter = null;

        List<Contact> contactList = new List<Contact>();

        public override void Awake()
        {
            var contactHolderEntity = world.NewEntity();
            contactHolderEntity.AddComponent<ContactHolderComp>() = new ContactHolderComp() {

                contactList = contactList,
            };
        }

        public override void FixedUpdate()
        {
            contactList.Clear();

            foreach(var s in bothRigidFilter)
            {
                ref var particleComp = ref bothRigidFilter.Get1(s);
                ref var colliderComp = ref bothRigidFilter.Get2(s);                

                foreach (var o in onlyColliderFilter)
                {
                    ref var otherColliderComp = ref onlyColliderFilter.Get1(o);

                    if (colliderComp.collider.IsIntersectWith(otherColliderComp.collider.bound,out var contact))
                    {
                        contact.particles[0] = particleComp.particle;
                        contact.particles[1] = null;
                        contact.restitution = Math.Min(colliderComp.collider.restitution, otherColliderComp.collider.restitution);
                        contactList.Add(contact);
                    }
                }
            }

            for(int i = 0; i < bothRigidFilter.NumberOfEntities-1; i++)
            {
                ref var particleComp = ref bothRigidFilter.Get1(i);
                ref var colliderComp = ref bothRigidFilter.Get2(i);

                for(int j=i+1; j < bothRigidFilter.NumberOfEntities; j++)
                {
                    ref var otherParticleComp = ref bothRigidFilter.Get1(j);
                    ref var otherColliderComp = ref bothRigidFilter.Get2(j);


                    if (colliderComp.collider.IsIntersectWith(otherColliderComp.collider.bound, out var contact))
                    {
                        contact.particles[0] = particleComp.particle;
                        contact.particles[1] = otherParticleComp.particle;

                        contact.restitution = Math.Min(colliderComp.collider.restitution, otherColliderComp.collider.restitution);
                        contactList.Add(contact);
                    }
                }
            }
        }

    }
}
