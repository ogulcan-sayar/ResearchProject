using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using MespEvents;
using RenderLibrary.Graphics;
using RenderLibrary.OpenGLCustomFunctions;
using RenderLibrary.Shaders;

namespace SimulationSystem
{
    public struct DrawLineEvent : IEvent
    {
        public Vector3 from;
        public Vector3 to;
        public Vector3 color;
    }  

    public class MespEditorDebugSystem : Dalak.Ecs.System
    {
        public static MespEventManager eventManager = new MespEventManager();

        public const int LineRendererCount = 10000;
        public LineRenderer[] lineRendererArray = new LineRenderer[LineRendererCount];


        public override void Awake()
        {
            for (int i = 0; i < LineRendererCount; i++)
            {
                lineRendererArray[i] = new LineRenderer();
                lineRendererArray[i].Setup();
            }
        }


        public override void PostRender()
        {
            ListenEditorDebugEvents(world);
        }

        private void ListenEditorDebugEvents(World world)
        {
            OpenGLFunctions.GLDisable(OpenGLEnum.GL_DEPTH_TEST);
            var drawLineData = eventManager.ListenEvents<DrawLineEvent>();
            int lineRendererCounter = 0;
            foreach(var d in drawLineData)
            {
                lineRendererArray[lineRendererCounter].SetNewColor(d.color);
                lineRendererArray[lineRendererCounter].SetNewPositions(d.from,d.to);
                lineRendererArray[lineRendererCounter].LineRender(ShaderPool.lineRenderShader, 3);
                lineRendererCounter++;
                if (lineRendererCounter >= LineRendererCount) break;
            }
            OpenGLFunctions.GLEnable(OpenGLEnum.GL_DEPTH_TEST);

        }

    }
}
