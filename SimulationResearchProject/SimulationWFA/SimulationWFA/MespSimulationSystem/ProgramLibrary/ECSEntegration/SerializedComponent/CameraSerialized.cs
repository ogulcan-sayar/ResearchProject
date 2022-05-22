using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.Components;

namespace TheSimulation.SerializedComponent
{
    class CameraSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public float speed;
        public float zoom;
        public float near;
        public float far;

        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<CameraComp>() = new CameraComp() {
                speed = speed,
                zoom = zoom,
                near = near,
                far = far,
            };
        }

        public override string GetName()
        {
            return "Camera Serialized";
        }
    }
}
