#ifndef CUBEMAPS_H
#define CUBEMAPS_H

#include "iostream"
#include "STB/stb_image.h"
#include "GLAD/glad.h"

#include <vector>
#include <Graphics/Objects/Mesh.h>

class Cubemaps {

public:
	unsigned int cubemapTexture;
	Mesh mesh;

	unsigned int VAO,VBO, EBO;

	void LoadCubemaps(std::vector<std::string> faces);

	void Render();

};



#endif