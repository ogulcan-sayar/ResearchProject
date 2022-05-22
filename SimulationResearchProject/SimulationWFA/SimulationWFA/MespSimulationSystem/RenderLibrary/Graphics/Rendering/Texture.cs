using System;
using System.IO;
using ProgramLibrary;
using RenderLibrary.DLL;
using SimulationWFA.MespUtils;

namespace RenderLibrary.Graphics.Rendering
{
    public class Texture : IAssetSerializator
    {
        protected IntPtr textureAdress;
        public string name;
        public TextureWrapType wrapSParameter;
        public TextureWrapType wrapTParameter;
        public TextureMapType textureMappingType;

        public Texture() { }

        public Texture(IntPtr textureAdress)
        {
            this.textureAdress = textureAdress;
        }

        public Texture(string directory, string name, TextureMapType textureType)
        {
            this.name = name;
            textureMappingType = textureType;
            textureAdress = RenderProgramDLL.NewTexture(directory, name, (int) textureType);
            Load(true);
        }

        private void Load(bool flip)
        {
            RenderProgramDLL.TextureLoad(textureAdress, flip);
        }

        public void SetWrapParameters(TextureWrapType wrapSParameter, TextureWrapType wrapTParameter)
        {
            this.wrapSParameter = wrapSParameter;
            this.wrapTParameter = wrapTParameter;
            RenderProgramDLL.TextureSetWrapParameters(textureAdress,(int)wrapSParameter,  (int)wrapTParameter);
        }

        public enum TextureMapType
        {
            Ambient=0,
            Diffuse=1,
            Specular=2
        }

        public enum TextureWrapType
        {
            GL_REPEAT = 0x2901,
            GL_CLAMP_TO_EDGE = 0x812F,
        }

        public IntPtr GetTextureAdress()
        {
            return textureAdress;
        }

        public string GetPath()
        {
            return name;
        }

        public object Serializate()
        {
            AssetSerializationData data = new AssetSerializationData();
            data.SetInt("TextureMappingType",(int)textureMappingType);
            data.SetInt("WrapSParameter",(int)wrapSParameter);
            data.SetInt("WrapTParameter", (int)wrapTParameter);
            data.SetString("ImageName", name);
            return data;
        }

        public object Deserializate(AssetSerializationData data)
        {
            Texture texture = new Texture(SimPath.ImagesPath, data.GetString("ImageName",""), (TextureMapType)data.GetInt("TextureMappingType",0));
            texture.SetWrapParameters((TextureWrapType)data.GetInt("WrapSParameter",-1), (TextureWrapType)data.GetInt("WrapTParameter", -1));
            return texture;
        }
    }
}