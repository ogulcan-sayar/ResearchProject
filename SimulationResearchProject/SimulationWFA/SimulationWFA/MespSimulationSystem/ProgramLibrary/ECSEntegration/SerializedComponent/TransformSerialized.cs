using System;
using System.Numerics;
using Dalak.Ecs;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;

namespace TheSimulation.SerializedComponent
{
    [Serializable]
    public class TransformSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector3 pos;
        public Vector3 scale;
        public Vector3 rotation;
        
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<TransformComp>() = new TransformComp()
            {
                transform = new Transform(pos,scale,rotation),
            };
            
        }

        public override string GetName()
        {
            return "Transform Serialized";
        }
    }
}