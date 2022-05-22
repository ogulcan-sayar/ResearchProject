using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class UnitSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public float speed;
        public float turnSpeed;
        public float turnDst;
        public float stoppingDst;
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<UnitComp>() = new UnitComp() {
                speed = speed,
                turnSpeed = turnSpeed,
                turnDst = turnDst,
                stoppingDst = stoppingDst
            };
        }

        public override string GetName()
        {
            return "Unit Serialized";
        }
    }
    public struct UnitComp
    {
        public Vector3 startPos;
        public float speed;
        public float turnSpeed;
        public float turnDst;
        public float stoppingDst;
    }
}
