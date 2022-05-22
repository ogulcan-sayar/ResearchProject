using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using RenderLibrary.Shaders;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.SharedData;

namespace SimulationSystem.Systems
{
    //  first opeque draw then transparent materials renders
    public class MeshRenderSystem : Dalak.Ecs.System
    {
        private Filter<MeshRendererComp, TransformComp>.Exclude<OutlineBorderRenderComp> meshRendererFilter = null;

        private Filter<SkinnedMeshRendererComp, TransformComp>.Exclude<AnimatorComp> noAnimatorSkinnedMeshFilter = null;
        private Filter<SkinnedMeshRendererComp, TransformComp, AnimatorComp> animatorSkinnedMeshFilter = null;

        private Filter<CameraComp, TransformComp> cameraFilter = null;

        Dictionary<int, float> transparentObjectDist = new Dictionary<int, float>();

        public override void Awake()
        {

        }


        public override void Render()
        {
            ref var cameraComp = ref cameraFilter.Get1(0);
            ref var transformCameraComp = ref cameraFilter.Get2(0);

            ShaderPool.SetupDefaultShadersToRender(cameraComp.view, cameraComp.projection);

            transparentObjectDist.Clear();

            //opaque render
            foreach (var m in meshRendererFilter)
            {
                ref var transformComp = ref meshRendererFilter.Get2(m);
                ref var meshRendererComp = ref meshRendererFilter.Get1(m);


                if (meshRendererComp.SetMeshRenderer() == false) continue;

                if (meshRendererComp.material.transparent)
                {
                    float sqrDist = Vector3.DistanceSquared(transformCameraComp.transform.position, transformComp.transform.position);
                    transparentObjectDist.Add(m, sqrDist);
                    continue;
                }
                meshRendererComp.material.GetShader().Activate();
                meshRendererComp.material.GetShader().SetInt("animate", 0); // TODO: bunun yeri bura değil
                meshRendererComp.meshRenderer.Render(transformComp.transform, meshRendererComp.material);
            }

            //skinned mesh render
            foreach (var s in noAnimatorSkinnedMeshFilter)
            {
                ref var transformComp = ref noAnimatorSkinnedMeshFilter.Get2(s);
                ref var skinnedMeshRenderComp = ref noAnimatorSkinnedMeshFilter.Get1(s);

                for (int i = 0; i < skinnedMeshRenderComp.meshRenderer.Length; i++)
                {
                    var materialShader = skinnedMeshRenderComp.material[i].GetShader();
                    materialShader.Activate();
                    materialShader.SetInt("animate", 0);

                    skinnedMeshRenderComp.meshRenderer[i].Render(transformComp.transform, skinnedMeshRenderComp.material[i]);
                }
            }

            foreach (var s in animatorSkinnedMeshFilter)
            {
                ref var animatorComp = ref animatorSkinnedMeshFilter.Get3(s);
                ref var transformComp = ref animatorSkinnedMeshFilter.Get2(s);
                ref var skinnedMeshRenderComp = ref animatorSkinnedMeshFilter.Get1(s);

                for (int i = 0; i < skinnedMeshRenderComp.meshRenderer.Length; i++)
                {
                    var materialShader = skinnedMeshRenderComp.material[i].GetShader();
                    animatorComp.animator.SetBoneMatrixToShader(materialShader);
                    materialShader.SetInt("animate", 1);

                    skinnedMeshRenderComp.meshRenderer[i].Render(transformComp.transform, skinnedMeshRenderComp.material[i]);
                }
            }

            //transparent render
            foreach (var m in transparentObjectDist.OrderByDescending(pair => pair.Value))
            {
                ref var transformComp = ref meshRendererFilter.Get2(m.Key);
                ref var meshRendererComp = ref meshRendererFilter.Get1(m.Key);
                meshRendererComp.material.GetShader().Activate();
                meshRendererComp.material.GetShader().SetInt("animate", 0); // TODO: bunun yeri bura değil
                meshRendererComp.meshRenderer.Render(transformComp.transform, meshRendererComp.material);
            }
        }

        public override void OnApplicationQuit()
        {
            foreach (var m in meshRendererFilter)
            {
                ref var meshRendererComp = ref meshRendererFilter.Get1(m);
                meshRendererComp.meshRenderer.CleanUp();
            }

            foreach (var s in noAnimatorSkinnedMeshFilter)
            {
                ref var skinnedMeshRenderComp = ref noAnimatorSkinnedMeshFilter.Get1(s);
                for (int i = 0; i < skinnedMeshRenderComp.meshRenderer.Length; i++) skinnedMeshRenderComp.meshRenderer[i].CleanUp();
            }
        }

    }
}