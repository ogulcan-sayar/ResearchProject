#ifndef TEXTURE_H
#define TEXTURE_H

#include "iostream"
#include "STB/stb_image.h"
#include "GLAD/glad.h"

#include "ASSIMP//scene.h"

class Texture {
public:

	unsigned int id;
	aiTextureType type;
	std::string dir;
	std::string path;


	Texture(std::string dir, std::string path, aiTextureType type);

	void Generate();
	void Load(bool flip);

	void SetWrapParameters(int wrapSParameter, int wrapTParameter);

	void Bind();

};

#endif