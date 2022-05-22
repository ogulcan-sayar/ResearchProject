using System.Numerics;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem.ECSSystems
{
    public class PhysicTestSystem : Dalak.Ecs.System
    {
        readonly Filter<CanMoveTestTag,ParticleComp> canMoveableFilter = null;

        public float force = 50;

        public override void Update()
        {
            foreach(var c in canMoveableFilter)
            {
                ref var rigidComp = ref canMoveableFilter.Get2(c);

                if (Input.GetKeyDown(KeyCode.I))
                {
                    rigidComp.particle.AddForce(new Vector3(0, 0, 1) * 50);
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    rigidComp.particle.AddForce(new Vector3(0, 0, -1) * 50);
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    rigidComp.particle.AddForce(new Vector3(1, 0, 0) * 50);
                }
                else if (Input.GetKeyDown(KeyCode.L))
                {
                    rigidComp.particle.AddForce(new Vector3(-1, 0, 0) * 50);
                }
            }

        }
    }
}
