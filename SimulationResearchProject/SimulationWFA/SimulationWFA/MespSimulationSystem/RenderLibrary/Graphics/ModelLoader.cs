using System;
using RenderLibrary.DLL;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Graphics
{

    public static class ModelLoader
    {

        public static  Model LoadModel(string path)
        {
             return new Model(RenderProgramDLL.LoadModel(path));
        }
    }
}