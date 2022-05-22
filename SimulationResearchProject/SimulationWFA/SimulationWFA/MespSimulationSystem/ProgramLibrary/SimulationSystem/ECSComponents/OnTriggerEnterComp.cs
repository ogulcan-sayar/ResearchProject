using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;

namespace SimulationSystem.ECSComponents
{
    public struct OnTriggerEnterComp
    {
        public List<Entity> collidedEntityList;
    }
}
