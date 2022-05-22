using System.Numerics;
using RenderLibrary.DLL;
using SimulationWFA.MespUtils;
using static RenderLibrary.Shaders.ShaderPool;

namespace RenderLibrary.Graphics.Rendering
{
    public class LitMaterial : Material
    {

        public LitMaterial()
        {
            materialAdress = RenderProgramDLL.NewLitMaterial();
            materialType = MaterialType.LitMaterial;

        }

        public LitMaterial(Vector4 ambient, Vector4 diffuse, Vector4 specular, float shininess)
        {
            materialType = MaterialType.LitMaterial;
            materialAdress = RenderProgramDLL.NewLitMaterial();
            SetAmbient(ambient);
            SetDiffuse(diffuse);
            SetSpecular(specular);
            SetShininess(shininess);
        }

        
        //static instances of common materials
        public static LitMaterial emerald = new LitMaterial(new Vector4(0.0215f, 0.1745f, 0.0215f,1), new Vector4(0.07568f, 0.61424f, 0.07568f,1), new Vector4(0.633f, 0.727811f, 0.633f,1), 0.6f);
        public static LitMaterial jade = new LitMaterial(new Vector4(0.135f, 0.2225f, 0.1575f,1), new Vector4(0.54f, 0.89f, 0.63f,1), new Vector4(0.316228f, 0.316228f, 0.316228f,1), 0.1f);
        public static LitMaterial obsidian = new LitMaterial(new Vector4(0.05375f, 0.05f, 0.06625f,1), new Vector4(0.18275f, 0.17f, 0.22525f,1), new Vector4(0.332741f, 0.328634f, 0.346435f,1), 0.3f);
        public static LitMaterial pearl = new LitMaterial(new Vector4(0.25f, 0.20725f, 0.20725f,1), new Vector4(1, 0.829f, 0.829f,1), new Vector4(0.296648f, 0.296648f, 0.296648f,1), 0.088f);
        public static LitMaterial ruby = new LitMaterial(new Vector4(0.1745f, 0.01175f, 0.01175f,1), new Vector4(0.61424f, 0.04136f, 0.04136f,1), new Vector4(0.727811f, 0.626959f, 0.626959f,1), 0.6f);
        public static LitMaterial turquoise = new LitMaterial(new Vector4(0.1f, 0.18725f, 0.1745f,1), new Vector4(0.396f, 0.74151f, 0.69102f,1), new Vector4(0.297254f, 0.30829f, 0.306678f,1), 0.1f);
        public static LitMaterial brass = new LitMaterial(new Vector4(0.329412f, 0.223529f, 0.027451f,1), new Vector4(0.780392f, 0.568627f, 0.113725f,1), new Vector4(0.992157f, 0.941176f, 0.807843f,1), 0.21794872f);
        public static LitMaterial bronze = new LitMaterial(new Vector4(0.2125f, 0.1275f, 0.054f,1), new Vector4(0.714f, 0.4284f, 0.18144f,1), new Vector4(0.393548f, 0.271906f, 0.166721f,1), 0.2f);
        public static LitMaterial chrome = new LitMaterial(new Vector4(0.25f, 0.25f, 0.25f,1), new Vector4(0.4f, 0.4f, 0.4f,1), new Vector4(0.774597f, 0.774597f, 0.774597f,1), 0.6f);
        public static LitMaterial copper = new LitMaterial(new Vector4(0.19125f, 0.0735f, 0.0225f,1), new Vector4(0.7038f, 0.27048f, 0.0828f,1), new Vector4(0.256777f, 0.137622f, 0.086014f,1), 0.1f);
        public static LitMaterial gold = new LitMaterial(new Vector4(0.24725f, 0.1995f, 0.0745f,1), new Vector4(0.75164f, 0.60648f, 0.22648f,1), new Vector4(0.628281f, 0.555802f, 0.366065f,1), 0.4f);
        public static LitMaterial silver = new LitMaterial(new Vector4(0.19225f, 0.19225f, 0.19225f,1), new Vector4(0.50754f, 0.50754f, 0.50754f,1), new Vector4(0.508273f, 0.508273f, 0.508273f,1), 0.4f);
        public static LitMaterial black_plastic = new LitMaterial(new Vector4(0.0f, 0.0f, 0.0f,1), new Vector4(0.01f, 0.01f, 0.01f,1), new Vector4(0.50f, 0.50f, 0.50f,1), .25f);
        public static LitMaterial cyan_plastic = new LitMaterial(new Vector4(0.0f, 0.1f, 0.06f,1), new Vector4(0.0f, 0.50980392f, 0.50980392f,1), new Vector4(0.50196078f, 0.50196078f, 0.50196078f,1), .25f);
        public static LitMaterial green_plastic = new LitMaterial(new Vector4(0.0f, 0.0f, 0.0f,1), new Vector4(0.1f, 0.35f, 0.1f,1), new Vector4(0.45f, 0.55f, 0.45f,1), .25f);
        public static LitMaterial red_plastic = new LitMaterial(new Vector4(0.0f, 0.0f, 0.0f,1), new Vector4(0.5f, 0.0f, 0.0f,1), new Vector4(0.7f, 0.6f, 0.6f,1), .25f);
        public static LitMaterial white_plastic = new LitMaterial(new Vector4(0.0f, 0.0f, 0.0f,1), new Vector4(0.55f, 0.55f, 0.55f,1), new Vector4(0.70f, 0.70f, 0.70f,1), .25f);
        public static LitMaterial yellow_plastic = new LitMaterial(new Vector4(0.0f, 0.0f, 0.0f,1), new Vector4(0.5f, 0.5f, 0.0f,1), new Vector4(0.60f, 0.60f, 0.50f,1), .25f);
        public static LitMaterial black_rubber = new LitMaterial(new Vector4(0.02f, 0.02f, 0.02f,1), new Vector4(0.01f, 0.01f, 0.01f,1), new Vector4(0.4f, 0.4f, 0.4f,1), .078125f);
        public static LitMaterial cyan_rubber = new LitMaterial(new Vector4(0.0f, 0.05f, 0.05f,1), new Vector4(0.4f, 0.5f, 0.5f,1), new Vector4(0.04f, 0.7f, 0.7f,1), .078125f);
        public static LitMaterial green_rubber = new LitMaterial(new Vector4(0.0f, 0.05f, 0.0f,1), new Vector4(0.4f, 0.5f, 0.4f,1), new Vector4(0.04f, 0.7f, 0.04f,1), .078125f);
        public static LitMaterial red_rubber = new LitMaterial(new Vector4(0.05f, 0.0f, 0.0f,1), new Vector4(0.5f, 0.4f, 0.4f,1), new Vector4(0.7f, 0.04f, 0.04f,1), .078125f);
        public static LitMaterial white_rubber = new LitMaterial(new Vector4(0.05f, 0.05f, 0.05f,1), new Vector4(0.5f, 0.5f, 0.5f,1), new Vector4(0.7f, 0.7f, 0.7f,1), .078125f);
        public static LitMaterial yellow_rubber = new LitMaterial(new Vector4(0.05f, 0.05f, 0.0f,1), new Vector4(0.5f, 0.5f, 0.4f,1), new Vector4(0.7f, 0.7f, 0.04f,1), .078125f);

        // function to mix two materials with a proportion
        /*public static LitMaterial Mix(LitMaterial m1, LitMaterial m2, float mix = 0.5f)
        {
            
            return new LitMaterial(
                m1.ambient * mix + m2.ambient * (1 - mix),
                m1.diffuse * mix + m2.diffuse * (1 - mix),
                m1.specular * mix + m2.specular * (1 - mix),
                m1.shininess * mix + m2.shininess * (1 - mix));
        }*/

        public void SetAmbient(Vector4 ambient)
        {
            float[] ambientF = new[] {ambient.X, ambient.Y, ambient.Z,ambient.W};
            RenderProgramDLL.SetAmbientToMaterial(materialAdress, ambientF);
        }
        
        public void SetDiffuse(Vector4 diffuse)
        {
            float[] diffuseF = new[] {diffuse.X, diffuse.Y, diffuse.Z, diffuse.W };
            RenderProgramDLL.SetDiffuseToMaterial(materialAdress, diffuseF);
        }
        
        public void SetSpecular(Vector4 specular)
        {
            float[] specularF = new[] {specular.X, specular.Y, specular.Z, specular.W };
            RenderProgramDLL.SetSpecularToMaterial(materialAdress, specularF);
        }
        
        public void SetShininess(float shininess)
        {
            RenderProgramDLL.SetShininessToMaterial(materialAdress,shininess);
        }

        public void AddTexture(Texture texture)
        {
            this.texture = texture;
            RenderProgramDLL.AddTextureToMaterial(materialAdress,texture.GetTextureAdress());
        }
        
        public Vector4 GetAmbient()
        {
            float[] colorF = new float[4];
            RenderProgramDLL.GetAmbientFromMaterial(materialAdress, colorF);
            return new Vector4(colorF[0], colorF[1], colorF[2], colorF[3]);
        }
        
        public Vector4 GetDiffuse()
        {
            float[] colorF = new float[4];
            RenderProgramDLL.GetDiffuseFromMaterial(materialAdress, colorF);
            return new Vector4(colorF[0], colorF[1], colorF[2], colorF[3]);
        }
        
        public Vector4 GetSpecular()
        {
            float[] colorF = new float[4];
            RenderProgramDLL.GetSpecularFromMaterial(materialAdress, colorF);
            return new Vector4(colorF[0], colorF[1], colorF[2], colorF[3]);
        }
        
        public float GetShininess()
        {
            return RenderProgramDLL.GetShininessFromMaterial(materialAdress);
        }

        public Texture GetTexture()
        {
            return texture;
        }

        public override object MaterialSerializate()
        {
            AssetSerializationData data = new AssetSerializationData();

            data.SetInt("MaterialType", (int)materialType);
            data.SetInt("ShaderType", (int)shaderType);
            data.SetBool("Transparent", transparent);
            data.SetString("TextureFileID", "Empty");

            var ambinet = GetAmbient();
            var ambientList = data.GetFloatList("Ambient");
            ambientList.Add(ambinet.X);
            ambientList.Add(ambinet.Y);
            ambientList.Add(ambinet.Z);
            ambientList.Add(ambinet.W);

            var diffuse = GetDiffuse();
            var diffuseList = data.GetFloatList("Diffuse");
            diffuseList.Add(diffuse.X);
            diffuseList.Add(diffuse.Y);
            diffuseList.Add(diffuse.Z);
            diffuseList.Add(diffuse.W);

            var specular = GetSpecular();
            var specularList = data.GetFloatList("Specular");
            specularList.Add(specular.X);
            specularList.Add(specular.Y);
            specularList.Add(specular.Z);
            specularList.Add(specular.W);

            data.SetFloat("Shininess", GetShininess());

            return data;
        }

        public override object MaterialDeSerializate(AssetSerializationData data)
        {
            SetShader(GetShaderByType((ShaderType)data.GetInt("ShaderType", -1)));

            var ambientList = data.GetFloatList("Ambient");
            Vector4 ambient = new Vector4(ambientList[0], ambientList[1], ambientList[2], ambientList[3]);
            SetAmbient(ambient);

            var diffuseList = data.GetFloatList("Diffuse");
            Vector4 diffuse = new Vector4(diffuseList[0], diffuseList[1], diffuseList[2], diffuseList[3]);
            SetDiffuse(diffuse);

            var specularList = data.GetFloatList("Specular");
            Vector4 specular = new Vector4(specularList[0], specularList[1], specularList[2], specularList[3]);
            SetSpecular(specular);

            SetShininess(data.GetFloat("Shininess",0));

            SetTransparent(data.GetBool("Transparent", false));

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