using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using ProgramLibrary;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.OpenGLCustomFunctions;
using RenderLibrary.Shaders;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class TextRendererSystem : Dalak.Ecs.System
    {
        Filter<TextRendererComp> textRendererFilter = null;

        TextRenderer textRenderer;
        Mat4 ortographicProjection;
        Texture fontDataTexture;

        public override void Awake()
        {
            fontDataTexture = new Texture(SimPath.GetAssetPath+"/Fonts", "ArialDefultFont.bmp",0);
            ortographicProjection = CameraComp.GetOrthographic();
        }

        public override void PostRender()
        {
            ShaderPool.textRenderShader.Activate();
            ShaderPool.textRenderShader.SetMat4("projection", ortographicProjection);
            OpenGLFunctions.GLDepthFunc(OpenGLEnum.GL_LEQUAL);
            foreach (var t in textRendererFilter)
            {
                
                ref var textRendererComp = ref textRendererFilter.Get1(t);

                if(textRendererComp.textRenderer == null)
                {
                    textRendererComp.textRenderer = textRenderer = new TextRenderer();
                    textRendererComp.textRenderer.LoadFont(fontDataTexture, 256, 256, 32, 32, 32);
                    textRendererComp.textRenderer.Setup();
                }

                textRendererComp.textRenderer.RenderText(ShaderPool.textRenderShader, textRendererComp.text, textRendererComp.UIPosition, textRendererComp.scale, textRendererComp.color);
            }
            OpenGLFunctions.GLDepthFunc(OpenGLEnum.GL_LESS);

        }
    }
}
