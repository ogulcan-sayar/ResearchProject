using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.Animations;
using RenderLibrary.Graphics;
using RenderLibrary.Shaders;
using SimulationSystem.ECSComponents;
using SimulationSystem.SharedData;
using SimulationSystem.TimeUtils;

namespace SimulationSystem 
{
    public class AnimationSystem : Dalak.Ecs.System
    {
        Filter<AnimatorComp> animatorFilter = null;

        public override void Awake()
        {

        }

        public override void Update()
        {
            foreach(var a in animatorFilter)
            {
                ref var animatorComp = ref animatorFilter.Get1(a);
                animatorComp.animator.Update(Time.deltaTime);
            }
            
        }

    }
}
