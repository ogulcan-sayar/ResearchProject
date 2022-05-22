using System.Numerics;
using Dalak.Ecs;
using SimulationSystem.ECS.Entegration;

namespace TheSimulation.SerializedComponent
{
    public class TransformSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector3 pos;
        public Vector3 scale;
        public Vector3 rotation;
        
        public override void AddComponent(Entity entity, World world)
        {
            throw new System.NotImplementedException();
        }
    }
}