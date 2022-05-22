using System.Numerics;
using Dalak.Ecs;
using SimulationSystem.Components;
using static RenderLibrary.Graphics.Rendering.Lights;

namespace TheSimulation.SerializedComponent
{
    class DirectionalLightSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector4 ambient;
        public Vector4 diffuse;
        public Vector4 specular;

        public override void AddComponent(Entity entity, World world)
        {
            DirectionalLight dirLight = new DirectionalLight() {
                ambient = ambient,
                diffuse = diffuse,
                specular = specular
            };

            entity.AddComponent<DirectionalLightComp>() = new DirectionalLightComp() {

                directionalLight = dirLight,
            };
        }

        public override string GetName()
        {
            return "Directional Light Serialized";
        }
    }
}
