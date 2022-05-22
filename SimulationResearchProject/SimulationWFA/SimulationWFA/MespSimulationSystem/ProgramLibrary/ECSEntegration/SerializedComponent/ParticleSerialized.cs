using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using SimulationSystem.ECSComponents;

namespace ECSEntegration.SerializedComponent
{
    public class ParticleSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector3 velocity;
        public float mass = 1;

        public float drag;

        public bool useGravity;

        public override void AddComponent(Entity entity, World world)
        {
            Particle rb = new Particle();
            rb.velocity = velocity;
            rb.SetMass(mass);
            rb.useGravity = useGravity;
            rb.drag = drag;

            entity.AddComponent<ParticleComp>() = new ParticleComp {
                particle = rb,
            };
        }

        public override string GetName()
        {
            return "Particle System Serialized";
        }
    }
}
