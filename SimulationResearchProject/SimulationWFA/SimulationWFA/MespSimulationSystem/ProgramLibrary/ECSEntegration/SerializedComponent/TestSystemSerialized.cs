using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;

namespace SimulationWFA.MespSimulationSystem.ProgramLibrary.ECSEntegration.SerializedComponent
{
    public class TestSystemSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<TestSystemComp>();
        }

        public override string GetName()
        {
            return "TestSystemSerialized";
        }
    }
}
