using System;
using RenderLibrary.DLL;

namespace RenderLibrary.Graphics.Rendering
{
    public class Texture
    {
        public IntPtr textureAdress;

        public Texture(string directory, string name, TextureType textureType)
        {
            RenderProgramDLL.NewTexture(directory, name, (int) textureType);
        }

        public void Load(bool flip)
        {
            RenderProgramDLL.TextureLoad(textureAdress, flip);
        }

        public enum TextureType
        {
            Ambient=0,
            Diffuse=1,
            Specular=2
        }
    }
}