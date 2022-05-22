using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MESPSimulationSystem.Math;
using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Shaders
{
    public static class ShaderPool
    {
        public enum ShaderType
        {
            LitShader, UnlitShader, TransTestShader, OutlineBorderShader, InfiniteGridShader, TextRenderShader, LineRenderShader, SkyboxShader
        }

        public static Shader litShader = new Shader(SimPath.ShadersPath + "/object.vs", SimPath.ShadersPath + "/lit.fs", ShaderType.LitShader);
        public static Shader unlitShader = new Shader(SimPath.ShadersPath + "/object.vs", SimPath.ShadersPath + "/unlit.fs", ShaderType.UnlitShader);
        public static Shader transTestShader = new Shader(SimPath.ShadersPath + "/object.vs", SimPath.ShadersPath + "/TransTestShader/transparent.fs", ShaderType.TransTestShader);
        public static Shader outlineBorderShader = new Shader(SimPath.ShadersPath + "/object.vs", SimPath.ShadersPath + "/OutlineShader/Outline.fs", ShaderType.OutlineBorderShader);
        public static Shader infiniteGridShader = new Shader(SimPath.ShadersPath + "/InfiniteGridShader/infiniteGrid.vs", SimPath.ShadersPath + "/InfiniteGridShader/infiniteGrid.fs", ShaderType.InfiniteGridShader);
        public static Shader textRenderShader = new Shader(SimPath.ShadersPath + "/TextRenderShader/textRender.vs", SimPath.ShadersPath + "/TextRenderShader/textRender.fs", ShaderType.TextRenderShader);
        public static Shader lineRenderShader = new Shader(SimPath.ShadersPath + "/LineRendererShader/lineShader.vs", SimPath.ShadersPath + "/LineRendererShader/lineShader.fs", ShaderType.LineRenderShader);
        public static Shader skyboxShader = new Shader(SimPath.ShadersPath + "/skyboxShader/skybox.vs", SimPath.ShadersPath + "/skyboxShader/skybox.fs", ShaderType.SkyboxShader);

        public static Shader[] allShaders = { litShader, unlitShader, transTestShader, outlineBorderShader, infiniteGridShader, lineRenderShader, skyboxShader };
        public static Shader[] allLitShader = { litShader };

        public static Shader GetShaderByType(ShaderType shaderType)
        {
            switch (shaderType)
            {
                case ShaderType.LitShader: return litShader;
                case ShaderType.UnlitShader: return unlitShader;
                case ShaderType.TransTestShader: return transTestShader;
                case ShaderType.OutlineBorderShader: return outlineBorderShader;
                case ShaderType.InfiniteGridShader: return infiniteGridShader;
                case ShaderType.LineRenderShader: return lineRenderShader;
                case ShaderType.SkyboxShader: return skyboxShader;
            }

            return null;
        }

        public static void SetupDefaultShadersToRender(Mat4 view, Mat4 projection)
        {
            for (int i = 0; i < allShaders.Length; i++)
            {
                allShaders[i].Activate();
                allShaders[i].SetMat4("view", view);
                allShaders[i].SetMat4("projection", projection);
            }
        }

        public static void SetupShaderToRender(Shader shader,Mat4 view, Mat4 projection)
        {
            shader.Activate();
            shader.SetMat4("view", view);
            shader.SetMat4("projection", projection);
        }


    }
}
