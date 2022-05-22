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

        public LineRenderer lineRenderer = new LineRenderer();


        public override void Awake()
        {
            lineRenderer.Setup();
        }


        public override void PostRender()
        {
            ListenEditorDebugEvents(world);
        }

        private void ListenEditorDebugEvents(World world)
        {
           // OpenGLFunctions.GLDisable(OpenGLEnum.GL_DEPTH_TEST);
            var drawLineData = eventManager.ListenEvents<DrawLineEvent>();

            foreach(var d in drawLineData)
            {
                lineRenderer.SetNewColor(d.color);
                lineRenderer.SetNewPositions(d.from,d.to);
                lineRenderer.LineRender(ShaderPool.lineRenderShader, 3);
            }

          //  OpenGLFunctions.GLEnable(OpenGLEnum.GL_DEPTH_TEST);

        }

    }
}
