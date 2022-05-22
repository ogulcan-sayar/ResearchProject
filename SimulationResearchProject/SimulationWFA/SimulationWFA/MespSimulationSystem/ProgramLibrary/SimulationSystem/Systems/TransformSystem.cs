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

                direction.X = (float)Math.Cos(MathFunctions.ConvertToRadians(transformComp.transform.rotation.Y)) * (float)Math.Cos(MathFunctions.ConvertToRadians(transformComp.transform.rotation.X));
                direction.Y = (float)Math.Sin(MathFunctions.ConvertToRadians(transformComp.transform.rotation.X));
                direction.Z = (float)Math.Sin(MathFunctions.ConvertToRadians(transformComp.transform.rotation.Y)) * (float)Math.Cos(MathFunctions.ConvertToRadians(transformComp.transform.rotation.X));
                
                transformComp.transform.forward = Vector3.Normalize(direction);
                transformComp.transform.right = Vector3.Normalize(Vector3.Cross(transformComp.transform.forward, Transform.WorldUp));
                transformComp.transform.up = Vector3.Normalize(Vector3.Cross(transformComp.transform.right, transformComp.transform.forward));

            }
        }
    }
}
