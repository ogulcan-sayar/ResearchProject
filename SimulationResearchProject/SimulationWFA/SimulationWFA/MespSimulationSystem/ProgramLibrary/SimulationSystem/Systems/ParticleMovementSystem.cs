using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.Timer;

namespace SimulationSystem.ECSSystems
{
    public class ParticleMovementSystem : Dalak.Ecs.System
    {
        readonly Filter<ParticleComp,TransformComp> particleFilter = null;

        public override void Awake()
        {
            foreach (var r in particleFilter)
            {
                ref ParticleComp particleComp = ref particleFilter.Get1(r);
                ref TransformComp transformComp = ref particleFilter.Get2(r);

                particleComp.particle.position = transformComp.transform.position;
                particleComp.particle.rotation =transformComp.transform.rotation;
            }
        }


        public override void FixedUpdate()
        {
            foreach(var r in particleFilter)
            {
                ref ParticleComp particleComp = ref particleFilter.Get1(r);
                ref TransformComp transformComp = ref particleFilter.Get2(r);
                particleComp.particle.Integrate(Time.fixedDeltaTime);
            }
        }

        public override void Update()
        {
            foreach (var r in particleFilter)
            {
                ref ParticleComp particleComp = ref particleFilter.Get1(r);
                ref TransformComp transformComp = ref particleFilter.Get2(r);

                transformComp.transform.position = particleComp.particle.position;
                transformComp.transform.rotation = particleComp.particle.rotation;
            }
        }

    }
}
