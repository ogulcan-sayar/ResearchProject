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

        public override void Awake()
        {
            Texture fontDataTexture = new Texture(SimPath.GetAssetPath+"/Fonts", "ArialDefultFont.bmp",0);

            foreach(var t in textRendererFilter)
            {
                ref var textRendererComp = ref textRendererFilter.Get1(t);
                textRendererComp.textRenderer = textRenderer = new TextRenderer();
                textRendererComp.textRenderer.LoadFont(fontDataTexture, 256, 256, 32, 32, 32);
                textRendererComp.textRenderer.Setup();
            }
            ortographicProjection = CameraComp.GetOrthographic();
        }

        public override void PostRender()
        {
            ShaderPool.textRenderShader.Activate();
            ShaderPool.textRenderShader.SetMat4("projection", ortographicProjection);

            foreach(var t in textRendererFilter)
            {
                ref var textRendererComp = ref textRendererFilter.Get1(t);
                textRendererComp.textRenderer.RenderText(ShaderPool.textRenderShader, textRendererComp.text, textRendererComp.UIPosition, textRendererComp.scale, textRendererComp.color);
            }

            
        }
    }
}
