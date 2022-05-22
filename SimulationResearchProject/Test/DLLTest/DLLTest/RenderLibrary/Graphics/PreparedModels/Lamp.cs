using System.Numerics;
using MESPSimulation.Graphics.Rendering;

namespace MESPSimulation.Graphics.Objects
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