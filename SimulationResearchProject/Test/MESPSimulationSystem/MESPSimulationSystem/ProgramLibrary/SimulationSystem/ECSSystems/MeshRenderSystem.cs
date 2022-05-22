using Dalak.Ecs;
using RenderLibrary.Graphics;
using RenderLibrary.Transform;
using SimulationSystem.Components;

namespace SimulationSystem.Systems
{
    public class MeshRenderSystem : Dalak.Ecs.System
    {
        private Filter<MeshRendererComp, TransformComp> meshRendererFilter = null;
        private MeshRenderer[] meshRenderers;
        
        private int meshRendererCount;
        public override void Awake()
        {
            UpdateMeshRenders();
        }

        public override void Render()
        {
            for (int i = 0; i < meshRendererCount; i++)
            {
                var transformComp = meshRendererFilter.Get2(i);
                meshRenderers[i].Render(transformComp.transform);
            }
        }


        public void UpdateMeshRenders()
        {
            meshRendererCount = meshRendererFilter.NumberOfEntities;
            meshRenderers = new MeshRenderer[meshRendererCount];
            for (int i = 0; i < meshRendererCount; i++)
            {
                ref var meshComp = ref meshRendererFilter.Get1(i);
                meshRenderers[i] = new MeshRenderer();
                meshRenderers[i].SetMesh(meshComp.mesh);
                meshRenderers[i].SetMaterial(meshComp.material);
                meshRenderers[i].Setup();
            }
        }
    }
}