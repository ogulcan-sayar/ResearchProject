using System.Numerics;
using Dalak.Ecs;
using SimulationSystem.Components;
using static RenderLibrary.Graphics.Rendering.Lights;

namespace TheSimulation.SerializedComponent
{
    public class SpotLightSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector4 ambient;
        public Vector4 diffuse;
        public Vector4 specular;

        public float cutOff;
        public float outerCutOff;

        public override void AddComponent(Entity entity, World world)
        {
            SpotLight spotLight = new SpotLight() {
                ambient = ambient,
                diffuse = diffuse,
                specular = specular,
                cutOff = cutOff,
                outerCutOff= outerCutOff,
            };

            entity.AddComponent<SpotLightComp>() = new SpotLightComp() {

                spotLight = spotLight,
            };
        }

        public override string GetName()
        {
            return "Spot Light Serialized";
        }
    }
}
