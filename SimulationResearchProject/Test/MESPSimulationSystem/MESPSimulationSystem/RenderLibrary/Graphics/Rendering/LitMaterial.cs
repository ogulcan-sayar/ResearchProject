using System.Numerics;
using RenderLibrary.DLL;

namespace RenderLibrary.Graphics.Rendering
{
    public class LitMaterial : Material{
        public LitMaterial()
        {
            materialAdress = RenderProgramDLL.NewLitMaterial();
        }

        public LitMaterial(Vector3 ambient, Vector3 diffuse, Vector3 specular, float shininess)
        {
            SetAmbient(ambient);
            SetDiffuse(diffuse);
            SetDiffuse(diffuse);
            SetSpecular(specular);
            SetShininess(shininess);
        }

        
        //static instances of common materials
        public static LitMaterial emerald = new LitMaterial(new Vector3(0.0215f, 0.1745f, 0.0215f), new Vector3(0.07568f, 0.61424f, 0.07568f), new Vector3(0.633f, 0.727811f, 0.633f), 0.6f);
        public static LitMaterial jade = new LitMaterial(new Vector3(0.135f, 0.2225f, 0.1575f), new Vector3(0.54f, 0.89f, 0.63f), new Vector3(0.316228f, 0.316228f, 0.316228f), 0.1f);
        public static LitMaterial obsidian = new LitMaterial(new Vector3(0.05375f, 0.05f, 0.06625f), new Vector3(0.18275f, 0.17f, 0.22525f), new Vector3(0.332741f, 0.328634f, 0.346435f), 0.3f);
        public static LitMaterial pearl = new LitMaterial(new Vector3(0.25f, 0.20725f, 0.20725f), new Vector3(1, 0.829f, 0.829f), new Vector3(0.296648f, 0.296648f, 0.296648f), 0.088f);
        public static LitMaterial ruby = new LitMaterial(new Vector3(0.1745f, 0.01175f, 0.01175f), new Vector3(0.61424f, 0.04136f, 0.04136f), new Vector3(0.727811f, 0.626959f, 0.626959f), 0.6f);
        public static LitMaterial turquoise = new LitMaterial(new Vector3(0.1f, 0.18725f, 0.1745f), new Vector3(0.396f, 0.74151f, 0.69102f), new Vector3(0.297254f, 0.30829f, 0.306678f), 0.1f);
        public static LitMaterial brass = new LitMaterial(new Vector3(0.329412f, 0.223529f, 0.027451f), new Vector3(0.780392f, 0.568627f, 0.113725f), new Vector3(0.992157f, 0.941176f, 0.807843f), 0.21794872f);
        public static LitMaterial bronze = new LitMaterial(new Vector3(0.2125f, 0.1275f, 0.054f), new Vector3(0.714f, 0.4284f, 0.18144f), new Vector3(0.393548f, 0.271906f, 0.166721f), 0.2f);
        public static LitMaterial chrome = new LitMaterial(new Vector3(0.25f, 0.25f, 0.25f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.774597f, 0.774597f, 0.774597f), 0.6f);
        public static LitMaterial copper = new LitMaterial(new Vector3(0.19125f, 0.0735f, 0.0225f), new Vector3(0.7038f, 0.27048f, 0.0828f), new Vector3(0.256777f, 0.137622f, 0.086014f), 0.1f);
        public static LitMaterial gold = new LitMaterial(new Vector3(0.24725f, 0.1995f, 0.0745f), new Vector3(0.75164f, 0.60648f, 0.22648f), new Vector3(0.628281f, 0.555802f, 0.366065f), 0.4f);
        public static LitMaterial silver = new LitMaterial(new Vector3(0.19225f, 0.19225f, 0.19225f), new Vector3(0.50754f, 0.50754f, 0.50754f), new Vector3(0.508273f, 0.508273f, 0.508273f), 0.4f);
        public static LitMaterial black_plastic = new LitMaterial(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.50f, 0.50f, 0.50f), .25f);
        public static LitMaterial cyan_plastic = new LitMaterial(new Vector3(0.0f, 0.1f, 0.06f), new Vector3(0.0f, 0.50980392f, 0.50980392f), new Vector3(0.50196078f, 0.50196078f, 0.50196078f), .25f);
        public static LitMaterial green_plastic = new LitMaterial(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.1f, 0.35f, 0.1f), new Vector3(0.45f, 0.55f, 0.45f), .25f);
        public static LitMaterial red_plastic = new LitMaterial(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.5f, 0.0f, 0.0f), new Vector3(0.7f, 0.6f, 0.6f), .25f);
        public static LitMaterial white_plastic = new LitMaterial(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.55f, 0.55f, 0.55f), new Vector3(0.70f, 0.70f, 0.70f), .25f);
        public static LitMaterial yellow_plastic = new LitMaterial(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.5f, 0.5f, 0.0f), new Vector3(0.60f, 0.60f, 0.50f), .25f);
        public static LitMaterial black_rubber = new LitMaterial(new Vector3(0.02f, 0.02f, 0.02f), new Vector3(0.01f, 0.01f, 0.01f), new Vector3(0.4f, 0.4f, 0.4f), .078125f);
        public static LitMaterial cyan_rubber = new LitMaterial(new Vector3(0.0f, 0.05f, 0.05f), new Vector3(0.4f, 0.5f, 0.5f), new Vector3(0.04f, 0.7f, 0.7f), .078125f);
        public static LitMaterial green_rubber = new LitMaterial(new Vector3(0.0f, 0.05f, 0.0f), new Vector3(0.4f, 0.5f, 0.4f), new Vector3(0.04f, 0.7f, 0.04f), .078125f);
        public static LitMaterial red_rubber = new LitMaterial(new Vector3(0.05f, 0.0f, 0.0f), new Vector3(0.5f, 0.4f, 0.4f), new Vector3(0.7f, 0.04f, 0.04f), .078125f);
        public static LitMaterial white_rubber = new LitMaterial(new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.7f, 0.7f, 0.7f), .078125f);
        public static LitMaterial yellow_rubber = new LitMaterial(new Vector3(0.05f, 0.05f, 0.0f), new Vector3(0.5f, 0.5f, 0.4f), new Vector3(0.7f, 0.7f, 0.04f), .078125f);

        // function to mix two materials with a proportion
        /*public static LitMaterial Mix(LitMaterial m1, LitMaterial m2, float mix = 0.5f)
        {
            
            return new LitMaterial(
                m1.ambient * mix + m2.ambient * (1 - mix),
                m1.diffuse * mix + m2.diffuse * (1 - mix),
                m1.specular * mix + m2.specular * (1 - mix),
                m1.shininess * mix + m2.shininess * (1 - mix));
        }*/

        public void SetAmbient(Vector3 ambient)
        {
            float[] ambientF = new[] {ambient.X, ambient.Y, ambient.Z};
            RenderProgramDLL.SetAmbientToMaterial(materialAdress, ambientF);
        }
        
        public void SetDiffuse(Vector3 diffuse)
        {
            float[] diffuseF = new[] {diffuse.X, diffuse.Y, diffuse.Z};
            RenderProgramDLL.SetDiffuseToMaterial(materialAdress, diffuseF);
        }
        
        public void SetSpecular(Vector3 specular)
        {
            float[] specularF = new[] {specular.X, specular.Y, specular.Z};
            RenderProgramDLL.SetSpecularToMaterial(materialAdress, specularF);
        }
        
        public void SetShininess(float shininess)
        {
            RenderProgramDLL.SetShininessToMaterial(materialAdress,shininess);
        }

        public void AddTexture(Texture texture)
        {
            RenderProgramDLL.AddTextureToMaterial(materialAdress,texture.textureAdress);
        }
        
        //TODO: material get fonksiyonlarını ekle
        
        /*public Vector3 GetAmbient()
        {
            //TODO: yorum satırlarını doldur
            //RenderDLL.SetMaterialAmbient()
            //return ambient
        }
        
        public Vector3 GetDiffuse()
        {
            //TODO: yorum satırlarını doldur
            //RenderDLL.SetMaterialDiffuse()
            //return diffuse
        }
        
        public Vector3 GetSpecular()
        {
            //TODO: yorum satırlarını doldur
            //RenderDLL.SetMaterialAmbient()
            //return diffuse
        }
        
        public float GetShininess()
        {
            //TODO: yorum satırlarını doldur
            //RenderDLL.SetMaterialShininess()
            //return shininess
        }

        public void GetTexture()
        {
            //TODO: yorum satırlarını doldur
            //RenderDLL.AddMaterialTexture();
            //return texture
        }*/
        
    }

}