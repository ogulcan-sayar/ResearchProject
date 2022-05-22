#include "Graphics/Rendering/Texture.h"




Texture::Texture(std::string dir, std::string path, aiTextureType type) 
	: dir(dir), path(path), type(type) {
	Generate();
}


void Texture::Generate()
{
	glGenTextures(1, &id);
}

void Texture::Load(bool flip)
{
	stbi_set_flip_vertically_on_load(flip);

	int width, height, nChannels;
	unsigned char* data = stbi_load((dir +  "/" + path).c_str(), &width, &height, &nChannels, 0);

	GLenum colorMode = GL_RGB;

	switch (nChannels)
	{
	case 1:
		colorMode = GL_RED;
		break;
	case 2:
		colorMode = GL_RG;
		break;
	case 3:
		colorMode = GL_RGB;
		break;
	case 4:
		colorMode = GL_RGBA;
		break;
	}

	if (data)
	{
		glBindTexture(GL_TEXTURE_2D, id);
		glTexImage2D(GL_TEXTURE_2D, 0, colorMode, width, height, 0, colorMode, GL_UNSIGNED_BYTE, data);
		glGenerateMipmap(GL_TEXTURE_2D);


		SetWrapParameters(GL_REPEAT, GL_REPEAT);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	}
	else
	{
		std::cerr << "Image not loaded at " << (dir + "/" + path).c_str() << std::endl;
	}

	stbi_image_free(data);
}




void Texture::SetWrapParameters(int wrapSParameter, int wrapTParameter) {
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, wrapSParameter);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, wrapTParameter);
}

void Texture::Bind()
{
	glBindTexture(GL_TEXTURE_2D, id);
}
