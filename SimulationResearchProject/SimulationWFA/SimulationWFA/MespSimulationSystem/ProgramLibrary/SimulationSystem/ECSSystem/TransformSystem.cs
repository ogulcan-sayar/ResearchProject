using System;
using System.Numerics;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using RenderLibrary.Transform;
using SimulationSystem.Components;

namespace SimulationSystem.Systems
{
    public class TransformSystem : Dalak.Ecs.System
    {
        readonly Filter<TransformComp> transformFilter = null;

        Vector3 direction = new Vector3();
        public override void Update()
        {
            foreach(var t in transformFilter)
            {
                ref var transformComp = ref transformFilter.Get1(t);
                var radianY = MathFunctions.ConvertToRadians(transformComp.transform.rotation.Y);
                var radianX = MathFunctions.ConvertToRadians(transformComp.transform.rotation.X);
                radianY = Math.Round(radianY,2);
                radianX = Math.Round(radianX,2);
                direction.X = (float)Math.Cos(radianY) * (float)Math.Cos(radianX);
                direction.Y = (float)Math.Sin(radianX);
                direction.Z = (float)Math.Sin(radianY) * (float)Math.Cos(radianX);
                
                transformComp.transform.forward = Vector3.Normalize(direction);
                transformComp.transform.right = Vector3.Normalize(Vector3.Cross(transformComp.transform.forward, Transform.WorldUp));
                transformComp.transform.up = Vector3.Normalize(Vector3.Cross(transformComp.transform.right, transformComp.transform.forward));

            }
        }
    }
}
