using System;
using System.Numerics;
using MESPSimulationSystem.Math;

namespace RenderLibrary.Graphics.Rendering
{
    public class Lights
    {
        public struct PointLight {

	        public Vector3 position;

            // attenuation constants
            public const float k0 = 1.0f;
            public const float k1 = 0.07f;
            public const float k2 = 0.032f;
	
            public Vector4 ambient;
            public Vector4 diffuse;
            public Vector4 specular;

            public void Render(Shader shader, int idx)
            {
                	string name = "pointLight[" + idx.ToString() + "]";
                
                	shader.Set3Float(name + ".position", position);
                	shader.SetFloat(name + ".k0", k0);
                	shader.SetFloat(name + ".k1", k1);
                	shader.SetFloat(name + ".k2", k2);
                	shader.Set4Float(name +  ".ambient", ambient);
                	shader.Set4Float(name + ".diffuse", diffuse);
                	shader.Set4Float(name + ".specular", specular);
            }
        };
        
        public struct DirectionalLight {
	
	        public Vector3 direction;
	        public Vector4 ambient;
	        public Vector4 diffuse;
	        public Vector4 specular;

	        public void Render(Shader shader)
	        {
		        string name = "directionalLight";

		        shader.Set3Float(name + ".direction", direction);
		        shader.Set4Float(name + ".ambient", ambient);
		        shader.Set4Float(name + ".diffuse", diffuse);
		        shader.Set4Float(name + ".specular", specular);
	        }
        };


        public struct SpotLight {

	        public Vector3 position;
	        public Vector3 direction;
	        public float cutOff;
	        public float outerCutOff;

            private float thetaCutOff;
            private float thetaOuterCutOff;

	        // attenuation constants
	        public const float k0 = 1.0f;
	        public const float k1 = 0.07f;
	        public const float k2 = 0.032f;

	        public Vector4 ambient;
	        public Vector4 diffuse;
	        public Vector4 specular;

	        public void Render(Shader shader, int idx)
            {
                if (thetaCutOff == 0 || thetaOuterCutOff == 0)
                {
                    thetaCutOff = (float) Math.Cos(MathFunctions.ConvertToRadians(cutOff));
                    thetaOuterCutOff = (float) Math.Cos(MathFunctions.ConvertToRadians(outerCutOff));
                }
               

		        string name = "spotLight[" + idx.ToString() + "]";

		        shader.Set3Float(name + ".position", position);
		        shader.Set3Float(name + ".direction", direction);

		        shader.SetFloat(name + ".k0", k0);
		        shader.SetFloat(name + ".k1", k1);
		        shader.SetFloat(name + ".k2", k2);
		        shader.SetFloat(name + ".cutOff", thetaCutOff);
		        shader.SetFloat(name + ".outerCutOff", thetaOuterCutOff);

		        shader.Set4Float(name + ".ambient", ambient);
		        shader.Set4Float(name + ".diffuse", diffuse);
		        shader.Set4Float(name + ".specular", specular);
	        }
        };

    }
}