#ifndef MESH_H
#define MESH_H

#include "GLAD/glad.h"
#include <GLFW/glfw3.h>

#include <vector>
#include <GLM/glm.hpp>

#include "Graphics/Rendering/Shader.h"
#include "Graphics/Rendering/Texture.h"


#define MAX_BONE_INFLUENCE 4
#define MAX_BONE 100

struct Vertex {
	glm::vec3 pos;
	glm::vec3 normal;
	glm::vec2 texCoord;

	//bone indexes which will influence this vertex
	int m_BoneIDs[MAX_BONE_INFLUENCE];
	//weights from each bone
	float m_Weights[MAX_BONE_INFLUENCE];

	static std::vector<struct Vertex> SetVertices(float* vertices, int numOfVertices);
};
typedef struct Vertex Vertex;


class Mesh {
public:
	int verticesSize;
	std::vector<Vertex> vertices;
	std::vector<unsigned int> indices;

	Mesh();
	Mesh(std::vector<Vertex> vertices, std::vector<unsigned int>indices);
};

#endif