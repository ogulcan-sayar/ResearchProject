#ifndef	LINERENDERER_H
#define	LINERENDERER_H

#include <GLM/glm.hpp>
#include <GLAD/glad.h>
#include <GLM/glm.hpp>
#include "Graphics/Rendering/Shader.h"

class LineRenderer {

public:
	unsigned int VAO, VBO;

	glm::vec3 from;
	glm::vec3 to;
	glm::vec3 color;

	void Setup();
	void SetNewLinePosition(glm::vec3 from, glm::vec3 to);
	void SetNewColor(glm::vec3 color);
	void Render(Shader& s, float lineWidth);
	void CleanUp();
};

#endif