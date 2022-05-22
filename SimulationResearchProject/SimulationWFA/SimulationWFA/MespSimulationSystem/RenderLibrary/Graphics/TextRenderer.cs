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
    public class TextRenderer
    {
        IntPtr textRendererAdress;

        public TextRenderer()
        {
            textRendererAdress = RenderProgramDLL.NewTextRenderer();
        }

        public void LoadFont(Texture texture, int widthRes, int heightRes, int cellHeight, int cellWidth, int initialASCII)
        {
            RenderProgramDLL.LoadFontToTextRenderer(textRendererAdress, texture.GetTextureAdress(), widthRes, heightRes, cellHeight, cellWidth, initialASCII);
        }

        public void Setup()
        {
            RenderProgramDLL.SetupTextQuad(textRendererAdress);
        }

        public void RenderText(Shader shader,string text,Vector2 position,float scale,Vector3 color)
        {
            float[] colorF = new float[3] { color.X,color.Y,color.Z };

            RenderProgramDLL.RenderText(textRendererAdress, shader.GetShaderAdress(), text, position.X, position.Y, scale, colorF);
        }
    }
}
