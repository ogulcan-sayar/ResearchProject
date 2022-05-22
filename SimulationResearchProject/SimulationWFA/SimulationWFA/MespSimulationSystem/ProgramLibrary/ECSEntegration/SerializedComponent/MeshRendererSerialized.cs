using System;
using Dalak.Ecs;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using SimulationSystem.Components;
using SimulationWFA.MespUtils;

namespace TheSimulation.SerializedComponent
{
    [Serializable]
    public class MeshRendererSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public string meshPath;
        public string materialPath;

        public override void AddComponent(Entity entity, World world)
        {
            var material = AssetUtils.LoadFromAsset<Material>(materialPath);
            var mesh = AssetUtils.LoadFromAsset<Mesh>(meshPath);

            entity.AddComponent<MeshRendererComp>() = new MeshRendererComp() {material = material, mesh = mesh,};
        }

        public override string GetName()
        {
            return "Mesh Renderer Serialized";
        }
    }
}

