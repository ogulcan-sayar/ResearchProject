using System;
using System.Collections.Generic;
using System.Numerics;

namespace MESPSimulation.Graphics.Model
{
    public class GeneralVertexData {
        
        public static void SetVertices(List<float> vertices,out Vector3[] vertexPos,out Vector3[] vertexNormal, out Vector2[] texCoord, int verticesSize)
        {
            SetVertices(vertices.ToArray(),out vertexPos,out vertexNormal,out texCoord,verticesSize);
        }
        
        public static void SetVertices(float[] vertices,out Vector3[] vertexPos,out Vector3[] vertexNormal, out Vector2[] texCoord, int verticesSize)
        {
            vertexPos = new Vector3[verticesSize];
            vertexNormal = new Vector3[verticesSize];
            texCoord = new Vector2[verticesSize];

            int v = 0;

            for (int i = 0; i < verticesSize; i++)
            {
                vertexPos[v] = new Vector3(
                    vertices[i *8 + 0],
                    vertices[i * 8 + 1],
                    vertices[i * 8 + 2]
                );

                vertexNormal[v] = new Vector3(
                    vertices[i * 8 + 3],
                    vertices[i * 8 + 4],
                    vertices[i * 8 + 5]

                );

                texCoord[v] = new Vector2(
                    vertices[i * 8 + 6],
                    vertices[i * 8 + 7]
                );
                v++;
            }
           
        }
    };
}