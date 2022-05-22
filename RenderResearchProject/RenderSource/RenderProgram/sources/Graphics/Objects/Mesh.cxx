#include "Graphics/Objects/Mesh.h"


std::vector<Vertex> Vertex::SetVertices(float* vertices, int numOfVertices) {
	std::vector<Vertex> ret(numOfVertices);

	int stride = sizeof(Vertex) / sizeof(float);

	for (int i = 0; i < numOfVertices; i++)
	{
		ret[i].pos = glm::vec3(
			vertices[i * stride + 0],
			vertices[i * stride + 1],
			vertices[i * stride + 2]
		);

		ret[i].normal = glm::vec3(
			vertices[i * stride + 3],
			vertices[i * stride + 4],
			vertices[i * stride + 5]

		);

		ret[i].texCoord = glm::vec2(
			vertices[i * stride + 6],
			vertices[i * stride + 7]
		);
	}

	return ret;
}

Mesh::Mesh() {
	verticesSize = 0;
	vertices = std::vector<Vertex>();
	indices = std::vector<unsigned int>();
}

Mesh::Mesh(std::vector<Vertex> vertices, std::vector<unsigned int> indices)
	: vertices(vertices), indices(indices), verticesSize(vertices.size()) {}

