using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.Components;
using SimulationSystem.TimeUtils;

namespace SimulationSystem.Systems
{
    public class RotationTestSystem : Dalak.Ecs.System
    {
        readonly Filter<DirectionalLightComp, TransformComp> dirLightFilter = null;
        readonly Filter<MeshRendererComp, TransformComp>.Exclude<PointLightComp> trolFilter = null;

        float speed = 360f;

        public override void Update()
        {
            foreach(var d in dirLightFilter)
            {
                ref var transformComp = ref dirLightFilter.Get2(d);

                var rotation = transformComp.transform.rotation;
                rotation.X += speed * Time.deltaTime;
                transformComp.transform.rotation = rotation;
            }

            /*foreach(var t in trolFilter)
            {
                ref var transformComp = ref trolFilter.Get2(t);

                var rotation = transformComp.transform.rotation;
                rotation.X += speed * Time.deltaTime;
                //rotation.Y += speed*2 * Time.deltaTime;
                transformComp.transform.rotation = rotation;
            }*/
        }
    }
}
