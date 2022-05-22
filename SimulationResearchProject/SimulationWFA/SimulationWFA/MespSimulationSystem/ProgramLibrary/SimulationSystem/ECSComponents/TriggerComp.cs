using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;

namespace SimulationSystem.ECSComponents
{
    public struct TriggerComp
    {
        public List<Entity> collidedEntityList;
        public List<Entity> collidedThisFrame;
    }
}
