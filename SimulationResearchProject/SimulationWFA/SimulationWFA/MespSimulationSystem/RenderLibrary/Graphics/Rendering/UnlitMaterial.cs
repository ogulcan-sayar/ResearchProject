using System;
using System.Numerics;
using RenderLibrary.DLL;
using RenderLibrary.Shaders;
using SimulationWFA.DataManagement;
using SimulationWFA.MespUtils;
using static RenderLibrary.Shaders.ShaderPool;

namespace RenderLibrary.Graphics.Rendering
{
    public class UnlitMaterial : Material
    {
        public UnlitMaterial()
        {
            materialAdress = RenderProgramDLL.NewUnlitMaterial();
            materialType = MaterialType.UnlitMaterial;
            RenderProgramDLL.SetColorToMaterial(materialAdress, new float[]{1,1,1,1});
        }

        public void SetColor(Vector4 color)
        {
            float[] colorF = new[] {color.X, color.Y, color.Z, color.W};
            RenderProgramDLL.SetColorToMaterial(materialAdress,colorF);
        }

        public void AddTexture(Texture texture)
        {
            this.texture = texture;
            RenderProgramDLL.AddTextureToUnlitMaterial(materialAdress, texture.GetTextureAdress());
        }

        public Texture GetTexture()
        {
            return texture;
        }


        public Vector4 GetColor()
        {
            float[] colorF = new float[4];
            RenderProgramDLL.GetColorFromMaterial(materialAdress, colorF);
            return new Vector4(colorF[0], colorF[1], colorF[2], colorF[3]);
        }

        public override object MaterialSerializate()
        {
            AssetSerializationData data = new AssetSerializationData();

            data.SetInt("MaterialType", (int)materialType);
            data.SetInt("ShaderType",(int)shaderType);

            var color = GetColor();
            var colorList = data.GetFloatList("Color");
            colorList.Add(color.X);
            colorList.Add(color.Y);
            colorList.Add(color.Z);
            colorList.Add(color.W);

            data.SetString("TextureFileID", "Empty");

            data.SetBool("Transparent", transparent);
            return data;
        }

        public override object MaterialDeSerializate(AssetSerializationData data)
        {
            SetShader(GetShaderByType((ShaderType)data.GetInt("ShaderType",-1)));

            var colorList = data.GetFloatList("Color");
            Vector4 color = new Vector4(colorList[0], colorList[1], colorList[2], colorList[3]);
            SetColor(color);
            SetTransparent(data.GetBool("Transparent",false));

            var textureID = data.GetString("TextureFileID", "Empty");
            if (textureID != "Empty")
            {
                var texName = AssetOrganizer.GetTexturePathByFileID(textureID);
                if (texName == null) return null;
                AddTexture(AssetUtils.LoadFromAsset<Texture>(texName));
            }
            


            return this;
        }
    }
}