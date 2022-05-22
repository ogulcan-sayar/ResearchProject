using System.Numerics;
using RenderLibrary.Graphics.Rendering;


namespace RenderLibrary.Graphics.PreparedModels
{
    public class Lamp : CubeMesh
    {
        public Vector3 lightColor;
        public Lights.PointLight pointLight;
        
        public Lamp() : base()
        {}

        public Lamp(Vector3 lightColor,Lights.PointLight pointLight) : base()
        {
            this.lightColor = lightColor;
            this.pointLight = pointLight;
        }
    }
}