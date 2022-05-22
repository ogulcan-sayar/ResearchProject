using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenderLibrary.Graphics.RenderData;

namespace RenderLibrary.Graphics.PreparedModels
{
    public class DirectionArrowMesh : Mesh
    {
        public int numbOfVertices;

        public DirectionArrowMesh()
        {
            numbOfVertices = 9;
            float[] vertices = new float[] {

                //positions   // normals  // texture
                0, 0.2f, 0,      0,0,-1,    0,0,
                1, 0.2f, 0,     0,0,-1,     0,0,
                0, -0.2f,0,     0,0,-1,     0,0,

                1, 0.2f, 0,     0,0,-1,     0,0,
                0, -0.2f,0,     0,0,-1,     0,0,
                1, -0.2f,0,     0,0,-1,     0,0,

                1,.5f,0,          0,0,-1,     0,0,
                2,0,0,          0,0,-1,     0,0,
                1,-.5f,0,         0,0,-1,     0,0,

            };

            GeneralVertexData.SetVertices(vertices, out var position, out var normal, out var texCoord, numbOfVertices);

            var indices = new int[numbOfVertices];

            for (int i = 0; i < numbOfVertices; i++)
            {
                indices[i] = i;
            }

            SetVerticesPos(position);
            SetVerticesNormal(normal);
            SetVerticesTexCoord(texCoord);
            SetIndices(indices);
        }

    }
}
