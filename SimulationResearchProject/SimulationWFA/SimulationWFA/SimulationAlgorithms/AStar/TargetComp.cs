using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class TargetSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<TargetComp>();
        }

        public override string GetName()
        {
            return "Target Serialized";
        }
    }
    public struct TargetComp
    {
        public Vector3 initialPos;
    }
}
