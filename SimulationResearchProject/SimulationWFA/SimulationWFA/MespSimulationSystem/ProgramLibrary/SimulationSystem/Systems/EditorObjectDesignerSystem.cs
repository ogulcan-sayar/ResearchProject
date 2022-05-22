using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.OpenGLCustomFunctions;
using RenderLibrary.Shaders;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem.Systems
{
    public class EditorObjectDesignerSystem : Dalak.Ecs.System
    {
        private Filter<TransformPositionArrowComp, TransformComp> directionalArrowFilter = null;
        private Filter<OutlineBorderRenderComp> selectedObjectFilter = null;
        private Filter<CameraComp, TransformComp> cameraFilter = null;

        public override void PostRender()
        {
            OpenGLFunctions.GLDisable(OpenGLEnum.GL_DEPTH_TEST);

            ref var cameraComp = ref cameraFilter.Get1(0);
            ref var transformCameraComp = ref cameraFilter.Get2(0);

            ShaderPool.SetupDefaultShadersToRender(cameraComp.view, cameraComp.projection);

            foreach (var m in directionalArrowFilter)
            {
                var selectedObjectTrans = selectedObjectFilter.GetEntity(0).GetComponent<TransformComp>().transform;

                ref var transformComp = ref directionalArrowFilter.Get2(m);
                ref var meshRendererComp = ref directionalArrowFilter.Get1(m);

                transformComp.transform.position = selectedObjectTrans.position;

                if (meshRendererComp.SetMeshRenderer() == false) continue;

                meshRendererComp.material.GetShader().Activate();
                meshRendererComp.material.GetShader().SetInt("animate", 0); // TODO: bunun yeri bura değil
                meshRendererComp.meshRenderer.Render(transformComp.transform, meshRendererComp.material);
            }

            OpenGLFunctions.GLEnable(OpenGLEnum.GL_DEPTH_TEST);
        }
    }
}
