#ifndef TEXTRENDERER_H
#define	TEXTRENDERER_H

#include "Graphics/Rendering/Texture.h"
#include <GLM/glm.hpp>
#include <map>
#include <MeshRenderer.h>

struct Characters {
	glm::vec2 xPositions;
	glm::vec2 yPositions;


};

class TextRenderer {

public:
	unsigned int VAO, VBO;
	
	Texture* texture;
	std::map<int, Characters> characterMap;
	float spaceBetweenLetters;

	TextRenderer();
	void LoadFont(Texture* m_texture, int widthRes, int heightRes, int cellHeight, int cellWidth, int initialASCII);
	void SetupTextQuad();
	void RenderText(Shader& s, std::string text, float x, float y, float scale, glm::vec3 color);
};

#endif