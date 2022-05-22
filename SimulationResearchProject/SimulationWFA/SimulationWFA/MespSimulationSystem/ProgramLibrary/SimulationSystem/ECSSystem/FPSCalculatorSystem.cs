using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;
using SimulationSystem.TimeUtils;

namespace SimulationSystem.Systems
{
    public class FPSCalculatorSystem : Dalak.Ecs.System
    {
        Filter<FPSDisplayerComp,TextRendererComp> fpsDisplayerFilter = null;

        public override void Awake()
        {
            foreach (var f in fpsDisplayerFilter)
            {
                ref var textRendererComp = ref fpsDisplayerFilter.Get2(f);

               
            }
        }

        public override void Update()
        {
            foreach(var f in fpsDisplayerFilter)
            {
                ref var fpsDisplayComp = ref fpsDisplayerFilter.Get1(f);
                ref var textRendererComp = ref fpsDisplayerFilter.Get2(f);

                fpsDisplayComp.frame++;
                fpsDisplayComp.totalDuration += Time.deltaTime;

                if(fpsDisplayComp.totalDuration >= FPSDisplayerComp.sampleDuration)
                {
                    var fps = fpsDisplayComp.frame / fpsDisplayComp.totalDuration;
                    textRendererComp.text = "FPS: " + fps.ToString("0.00");
                    fpsDisplayComp.frame = 0;
                    fpsDisplayComp.totalDuration = 0;
                }

                
            }
        }


    }
}
