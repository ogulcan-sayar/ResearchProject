using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RenderLibrary.DLL;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Graphics
{
    public class LineRenderer
    {
        private IntPtr lineRendererAdress;

        private Vector3 refFrom;
        private Vector3 refTo;
        private Vector3 refColor;

        public Vector3 from {
            get {
                return refFrom;
            }
            set {
                SetNewPositions(value, refTo);
                refFrom = value;
            }
        }

        public Vector3 to {
            get {
                return refTo;
            }
            set {
                SetNewPositions(refFrom, value);
                refTo = value;
            }
        }

        public Vector3 color {
            get {
                return refColor;
            }
            set {
                SetNewColor(value);
                refColor = value;
            }
        }

        public LineRenderer()
        {
            lineRendererAdress = RenderProgramDLL.NewLineRenderer();
        }

        public void Setup()
        {
            RenderProgramDLL.LineRendererSetup(lineRendererAdress);
        }

        public void SetNewPositions(Vector3 from, Vector3 to)
        {
            float[] fromArr = { from.X, from.Y, from.Z };
            float[] toArr = { to.X, to.Y, to.Z };
            RenderProgramDLL.LineRendererSetNewPosition(lineRendererAdress, fromArr, toArr);
        }

        public void SetNewColor(Vector3 color)
        {
            float[] colorArr = { color.X, color.Y, color.Z };
            RenderProgramDLL.LineRendererSetNewColor(lineRendererAdress, colorArr);
        }
            
        public void LineRender(Shader shader, float lineWidth)
        {
            RenderProgramDLL.LineRender(lineRendererAdress, shader.GetShaderAdress(), lineWidth);
        }

        public void CleanUp()
        {
            RenderProgramDLL.LineRendererCleanUp(lineRendererAdress);
        }

    }
}
