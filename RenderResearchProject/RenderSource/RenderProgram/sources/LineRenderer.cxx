#include "LineRenderer.h"


void LineRenderer::Setup()
{
	glGenVertexArrays(1, &VAO);
	glGenBuffers(1, &VBO);
	glBindVertexArray(VAO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(float) * 6, NULL, GL_DYNAMIC_DRAW);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), 0);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindVertexArray(0);
}

void LineRenderer::SetNewLinePosition(glm::vec3 from, glm::vec3 to)
{
	this->from = from;
	this->to = to;
}

void LineRenderer::SetNewColor(glm::vec3 color)
{
	this->color = color;
}

void LineRenderer::Render(Shader& s, float lineWidth)
{
	s.Activate();
	s.Set3Float("color", color.x, color.y, color.z);
	glBindVertexArray(VAO);

	float vertices[2][3] = {
			{ from.x,from.y,from.z },
			{ to.x,to.y,to.z },
	};
	glLineWidth(lineWidth);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferSubData(GL_ARRAY_BUFFER, 0, sizeof(vertices), vertices);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	// render quad
	glDrawArrays(GL_LINES, 0, 2);

	glBindVertexArray(0);
}

void LineRenderer::CleanUp()
{
	glDeleteVertexArrays(1, &VAO);
	glDeleteBuffers(1, &VBO);
}
