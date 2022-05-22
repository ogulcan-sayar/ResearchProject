using System;
using RenderLibrary.DLL;
using SimulationWFA.MespUtils;
using static RenderLibrary.Shaders.ShaderPool;

namespace RenderLibrary.Graphics.Rendering
{
    public enum MaterialType
    {
        LitMaterial,UnlitMaterial
    }


    public class Material : IAssetSerializator
    {
        protected IntPtr materialAdress;
        public ShaderType shaderType;
        public MaterialType materialType;
        public Texture texture;
        public bool transparent = false;

        public void SetShader(Shader shader)
        {
            shaderType = shader.shaderType;
            RenderProgramDLL.SetShaderToMaterial(materialAdress,shader.GetShaderAdress());
        }

        public Shader GetShader()
        {
            return new Shader(RenderProgramDLL.GetShaderFromMaterial(materialAdress));
        }
        
        public void SetAdress(IntPtr materialAdress)
        {
            this.materialAdress = materialAdress;
        }

        public IntPtr GetAdress()
        {
            return materialAdress;
        }

        public void SetTransparent(bool isTransparent)
        {
            transparent = isTransparent;
            RenderProgramDLL.SetTransparent(materialAdress, isTransparent);
        }

        public object Serializate()
        {
            return MaterialSerializate();
        }

        public object Deserializate(AssetSerializationData data)
        {
            materialType = (MaterialType)data.GetInt("MaterialType", -1);

            if(materialType == MaterialType.LitMaterial)
            {
                return new LitMaterial().MaterialDeSerializate(data);
            }
            else
            {
                return new UnlitMaterial().MaterialDeSerializate(data);
            }
        }

        public virtual object MaterialSerializate()
        {
            return null;
        }

        public virtual object MaterialDeSerializate(AssetSerializationData data)
        {
            return null;
        }
    }
}