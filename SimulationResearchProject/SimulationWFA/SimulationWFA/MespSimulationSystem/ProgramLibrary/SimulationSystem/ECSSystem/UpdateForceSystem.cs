using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem.ECSSystems
{
    public class UpdateForceSystem : Dalak.Ecs.System
    {
        readonly Filter<ParticleComp> particleFilter = null;

        public override void FixedUpdate()
        {
            foreach (var r in particleFilter)
            {
                ref ParticleComp particleComp = ref particleFilter.Get1(r);

                GravityForceGenerator.ApplyGravity(particleComp.particle);
                DragForceGenerator.UpdateDragForce(particleComp.particle);
            }
        }
    }
}
