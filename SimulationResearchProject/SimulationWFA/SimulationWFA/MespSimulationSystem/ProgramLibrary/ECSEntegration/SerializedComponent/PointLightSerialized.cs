using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.Components;
using static RenderLibrary.Graphics.Rendering.Lights;

namespace TheSimulation.SerializedComponent
{
    public class  PointLightSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector4 ambient;
        public Vector4 diffuse;
        public Vector4 specular;

        public override void AddComponent(Entity entity, World world)
        {
            PointLight pointLight = new PointLight() {
                ambient = ambient,
                diffuse = diffuse,
                specular = specular
            };

            entity.AddComponent<PointLightComp>() = new PointLightComp() {
                pointLight = pointLight,
            };
        }


        public override string GetName()
        {
            return "Point Light Serialized";
        }
    }
}
