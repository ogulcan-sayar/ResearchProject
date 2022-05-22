using System.Numerics;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.PreparedModels;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.SharedData;

namespace SimulationSystem.Systems
{
    class EditorInfiniteGridSystem : Dalak.Ecs.System
    { 
        private Filter<CameraComp, TransformComp> cameraFilter = null;

        UnlitMaterial infiniteMaterial;
        MeshRenderer meshRenderer;
        Transform transform;

        public override void Awake()
        {
            transform = new Transform() { position = Vector3.Zero, scale = Vector3.One, rotation = Vector3.Zero};
            infiniteMaterial = new UnlitMaterial();
            infiniteMaterial.SetShader(ShaderPool.GetShaderByType(ShaderPool.ShaderType.InfiniteGridShader));
            infiniteMaterial.SetTransparent(true);

            meshRenderer = new MeshRenderer();
            meshRenderer.SetMesh(PlaneMesh());
            meshRenderer.Setup();
        }

        public override void PostRender()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);
            ref var camTransformComp = ref cameraFilter.Get2(0);

            ShaderPool.SetupShaderToRender(ShaderPool.GetShaderByType(ShaderPool.ShaderType.InfiniteGridShader), cameraComp.view, cameraComp.projection);

            ShaderPool.infiniteGridShader.SetFloat("near", cameraComp.near);
            ShaderPool.infiniteGridShader.SetFloat("far", cameraComp.far);

            meshRenderer.Render(transform,infiniteMaterial);
        }

        public Mesh PlaneMesh()
        {
            Mesh infiniteGridMesh = new Mesh();

            Vector3[] gridPlanePos = new Vector3[] {
                new Vector3(1f,1f,0f),
                new Vector3(-1f,-1f,0f),
                new Vector3(-1f,1f,0f),
                new Vector3(-1f,-1f,0f),
                new Vector3(1f,1f,0f),
                new Vector3(1f,-1f,0f),
            };

            var indices = new int[6];

            for (int i = 0; i < 6; i++)
            {
                indices[i] = i;
            }
            infiniteGridMesh.SetVerticesPos(gridPlanePos);
            infiniteGridMesh.SetIndices(indices);
            return infiniteGridMesh;
        }
    }
}
