using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;

namespace ECSEntegration.SerializedComponent
{
    public class TriggerSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<TriggerComp>() = new TriggerComp() { 
            
                collidedEntityList = new List<Entity>(),
                collidedThisFrame = new List<Entity>(),
            };
        }

        public override string GetName()
        {
            return "Trigger Serialized";
        }
    }
}
