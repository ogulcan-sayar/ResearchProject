using System;
using RenderLibrary.IO;
using SimulationSystem.TimeUtils;

namespace SimulationSystem
{
    public class ClearColorTestSystem : Dalak.Ecs.System
    {
        Screen screen = null;

        public override void Update()
        {
            screen.clearColor.Y = (float)Math.Abs(Math.Sin(Time.currentFrame));
            screen.clearColor.Z = (float)Math.Abs(Math.Cos(Time.currentFrame * 2f));
            screen.SetClearColor(screen.clearColor);
        }
    }
}
