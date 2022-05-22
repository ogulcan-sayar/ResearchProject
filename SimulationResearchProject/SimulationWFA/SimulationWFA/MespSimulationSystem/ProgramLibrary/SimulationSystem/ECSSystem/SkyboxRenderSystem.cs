using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using RenderLibrary.DLL;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.OpenGLCustomFunctions;
using RenderLibrary.Shaders;
using SimulationSystem.Components;
using SimulationSystem.SharedData;
using SimulationWFA.MespUtils;

namespace SimulationSystem
{
    public class SkyboxRenderSystem : Dalak.Ecs.System
    {
        readonly Filter<CameraComp> cameraFilter = null;
        readonly TextureReferences textureReferences = null;

        public Cubemap skybox;

        public override void Awake()
        {
            skybox = new Cubemap();
            skybox.LoadCubeMap(textureReferences.GetSkyboxPaths());
        }

        public override void Render()
        {
            ref var camComp = ref cameraFilter.Get1(0);

            OpenGLFunctions.GLDepthFunc(OpenGLEnum.GL_LEQUAL);

            Mat4 view =new Mat4();
            view.matrixAdress = RenderProgramDLL.ReturnMat4FromMat4(camComp.view.matrixAdress);

            ShaderPool.skyboxShader.Activate();
            ShaderPool.skyboxShader.SetMat4("view", view);
            ShaderPool.skyboxShader.SetMat4("projection", camComp.projection);

            skybox.RenderCubemap();
            OpenGLFunctions.GLDepthFunc(OpenGLEnum.GL_LESS);
        }

    }
}
