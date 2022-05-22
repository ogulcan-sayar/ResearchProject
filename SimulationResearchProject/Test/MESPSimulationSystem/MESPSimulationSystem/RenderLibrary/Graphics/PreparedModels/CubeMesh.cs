

using RenderLibrary.Graphics.RenderData;

namespace RenderLibrary.Graphics.PreparedModels
{
	public class CubeMesh : Mesh
	{
		public int numbOfVertices;
		
		public CubeMesh() : base()
		{
			numbOfVertices = 36;
			float[] vertices = new float[]
				{
					//positions					normal				textcoords
					-0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f,
					0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 1.0f, 0.0f,
					0.5f, 0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 1.0f, 1.0f,
					0.5f, 0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 1.0f, 1.0f,
					-0.5f, 0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 0.0f, 1.0f,
					-0.5f, -0.5f, -0.5f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f,

					-0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
					0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f,
					0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
					0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
					-0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
					-0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f,

					-0.5f, 0.5f, 0.5f, -1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
					-0.5f, 0.5f, -0.5f, -1.0f, 0.0f, 0.0f, 1.0f, 1.0f,
					-0.5f, -0.5f, -0.5f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
					-0.5f, -0.5f, -0.5f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
					-0.5f, -0.5f, 0.5f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
					-0.5f, 0.5f, 0.5f, -1.0f, 0.0f, 0.0f, 1.0f, 0.0f,

					0.5f, 0.5f, 0.5f, +1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
					0.5f, 0.5f, -0.5f, +1.0f, 0.0f, 0.0f, 1.0f, 1.0f,
					0.5f, -0.5f, -0.5f, +1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
					0.5f, -0.5f, -0.5f, +1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
					0.5f, -0.5f, 0.5f, +1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
					0.5f, 0.5f, 0.5f, +1.0f, 0.0f, 0.0f, 1.0f, 0.0f,

					-0.5f, -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, 0.0f, 1.0f,
					0.5f, -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, 1.0f, 1.0f,
					0.5f, -0.5f, 0.5f, 0.0f, -1.0f, 0.0f, 1.0f, 0.0f,
					0.5f, -0.5f, 0.5f, 0.0f, -1.0f, 0.0f, 1.0f, 0.0f,
					-0.5f, -0.5f, 0.5f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f,
					-0.5f, -0.5f, -0.5f, 0.0f, -1.0f, 0.0f, 0.0f, 1.0f,

					-0.5f, 0.5f, -0.5f, 0.0f, +1.0f, 0.0f, 0.0f, 1.0f,
					0.5f, 0.5f, -0.5f, 0.0f, +1.0f, 0.0f, 1.0f, 1.0f,
					0.5f, 0.5f, 0.5f, 0.0f, +1.0f, 0.0f, 1.0f, 0.0f,
					0.5f, 0.5f, 0.5f, 0.0f, +1.0f, 0.0f, 1.0f, 0.0f,
					-0.5f, 0.5f, 0.5f, 0.0f, +1.0f, 0.0f, 0.0f, 0.0f,
					-0.5f, 0.5f, -0.5f, 0.0f, +1.0f, 0.0f, 0.0f, 1.0f
				};

			GeneralVertexData.SetVertices(vertices, out var position, out var normal, out var texCoord,numbOfVertices);
			
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