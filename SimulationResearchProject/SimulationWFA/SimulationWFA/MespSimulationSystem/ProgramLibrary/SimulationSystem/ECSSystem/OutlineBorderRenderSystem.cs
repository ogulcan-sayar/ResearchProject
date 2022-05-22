using Dalak.Ecs;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using RenderLibrary.OpenGLCustomFunctions;
using MESPSimulationSystem.Math;
using SimulationSystem.SharedData;
using System.Numerics;
using System;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;

namespace SimulationSystem
{
    public class OutlineBorderRenderSystem : Dalak.Ecs.System
    {
        readonly Filter<MeshRendererComp,TransformComp, OutlineBorderRenderComp> renderFilter = null;

        private Filter<CameraComp> cameraFilter = null;

        UnlitMaterial outlineMaterial;

        public override void Awake()
        {
            outlineMaterial = new UnlitMaterial();
            outlineMaterial.SetShader(ShaderPool.outlineBorderShader);
        }

        public override void Render()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);

            ShaderPool.SetupDefaultShadersToRender(cameraComp.view, cameraComp.projection);
            
            foreach (var m in renderFilter)
            {
                ref var transformComp = ref renderFilter.Get2(m);
                ref var meshRendererComp = ref renderFilter.Get1(m);

                if (meshRendererComp.SetMeshRenderer() == false) continue;

                OpenGLFunctions.GLStencilOp(OpenGLEnum.GL_KEEP, OpenGLEnum.GL_KEEP, OpenGLEnum.GL_REPLACE);
                OpenGLFunctions.GLClear(OpenGLEnum.GL_STENCIL_BUFFER_BIT);
                OpenGLFunctions.GLStencilFunc(OpenGLEnum.GL_ALWAYS, 1, 1);
                OpenGLFunctions.GLStencilMask(1);

                meshRendererComp.meshRenderer.Render(transformComp.transform, meshRendererComp.material);

                OpenGLFunctions.GLStencilMask(0);
                OpenGLFunctions.GLStencilFunc(OpenGLEnum.GL_NOTEQUAL, 1, 1);
                OpenGLFunctions.GLDisable(OpenGLEnum.GL_DEPTH_TEST);

                var tempTransfrom = transformComp.transform;
                tempTransfrom.scale += new Vector3(0.1f,.1f,.1f);

                meshRendererComp.meshRenderer.Render(transformComp.transform, outlineMaterial);

                tempTransfrom.scale -= new Vector3(0.1f, .1f, .1f);

                OpenGLFunctions.GLStencilMask(1);
                OpenGLFunctions.GLStencilFunc(OpenGLEnum.GL_ALWAYS, 1, 1);
                OpenGLFunctions.GLEnable(OpenGLEnum.GL_DEPTH_TEST);
                OpenGLFunctions.GLStencilOp(OpenGLEnum.GL_KEEP, OpenGLEnum.GL_KEEP, OpenGLEnum.GL_REPLACE);
            }

            



        }

    }
}
