using System;
using RenderLibrary.DLL;


namespace RenderLibrary.IO
{
    class Screen
    {
        public IntPtr screenAdress;

        public void Create(int widht, int height)
        {
            screenAdress = RenderProgramDLL.CreateScreen(widht,height);
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
        }

        public void NewFrame()
        {
            RenderProgramDLL.ScreenNewFrame(screenAdress);
        }

        public void Terminate()
        {
            RenderProgramDLL.ScreenTerminate(screenAdress);
        }

    }
}
