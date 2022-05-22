using System;
using System.Drawing.Imaging;
using System.Numerics;
using RenderLibrary.DLL;
using RenderLibrary.OpenGLCustomFunctions;

namespace RenderLibrary.IO
{
    public class Screen
    {
        public IntPtr screenAdress;

        public Vector4 clearColor = new Vector4(.2f, .2f, .5f, 1.0f);

        public void Create(int widht, int height)
        {
            screenAdress = RenderProgramDLL.CreateScreen(widht,height);
        }

        public void SetParameters()
        {
            RenderProgramDLL.ScreenSetParameters(screenAdress);
        }

        public void SetClearColor(Vector4 clearColor)
        {
            this.clearColor = clearColor;
            RenderProgramDLL.SetClearColor(screenAdress, new float[]{ clearColor.X,clearColor.Y,clearColor.Z,clearColor.W});
        }

        public bool ShouldClose()
        {
            return RenderProgramDLL.ScreenShouldClose(screenAdress);
        }

        public void ProcessWindowInput()
        {
            RenderProgramDLL.ScreenProcessInput(screenAdress);
        }

        public void Update()
        {
            RenderProgramDLL.ScreenUpdate(screenAdress);
            OpenGLFunctions.GLClear(OpenGLEnum.GL_COLOR_BUFFER_BIT | OpenGLEnum.GL_DEPTH_BUFFER_BIT | OpenGLEnum.GL_STENCIL_BUFFER_BIT);
        }

        public void NewFrame()
        {
            RenderProgramDLL.ScreenNewFrame(screenAdress);
        }

        public float GetWidth()
        {
            return RenderProgramDLL.ScreenGetWidth(screenAdress);
        }

        public float GetHeight()
        {
            return RenderProgramDLL.ScreenGetHeight(screenAdress);
        }

        public void Terminate()
        {
            RenderProgramDLL.ScreenTerminate(screenAdress);
        }

    }
}
