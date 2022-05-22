#include "..\..\..\include\Graphics\Rendering\TextRenderer.h"

TextRenderer::TextRenderer()
{
	spaceBetweenLetters = 0;
}

void TextRenderer::LoadFont(Texture* m_texture, int widthRes, int heightRes, int cellHeight, int cellWidth, int initialASCII)
{
	texture = m_texture;

	float yOffset = (float)cellHeight / (float)heightRes;
	float xOffset = (float)cellWidth / (float)widthRes;

	int gridCountX = (widthRes / cellWidth);
	int gridCountY = (heightRes / cellHeight);

	float posY = 1.0f;
	float posX = 0.0f;
	int currentASCII = initialASCII;

	for (int y = 0; y < gridCountY; y++) {
		for (int x = 0; x < gridCountX; x++)
		{
			Characters character = Characters();
			character.xPositions = glm::vec2(posX, posX + xOffset);
			character.yPositions = glm::vec2(posY - yOffset, posY);
			characterMap.insert(std::pair<int, Characters>(currentASCII, character));

			posX += xOffset;
			currentASCII++;
		}
		posY -= yOffset;
		posX = 0.0f;
	}


}

void TextRenderer::SetupTextQuad()
{
	glGenVertexArrays(1, &VAO);
	glGenBuffers(1, &VBO);
	glBindVertexArray(VAO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(float) * 6 * 4, NULL, GL_DYNAMIC_DRAW);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 4, GL_FLOAT, GL_FALSE, 4 * sizeof(float), 0);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindVertexArray(0);
}

void TextRenderer::RenderText(Shader& s, std::string text, float x, float y, float scale, glm::vec3 color)
{
	s.Activate();
	s.Set3Float("textColor", color.x, color.y, color.z);
	glActiveTexture(GL_TEXTURE0);
	glBindVertexArray(VAO);

	// iterate through all characters
	std::string::const_iterator c;
	for (c = text.begin(); c != text.end(); c++)
	{
		Characters ch = characterMap[*c];

		float xpos = x + 1 * scale;
		float ypos = y - 1 * scale;

		float w = scale;
		float h = scale;
		// update VBO for each character
		float vertices[6][4] = {
			{ xpos + w, ypos,       ch.xPositions.y, ch.yPositions.x },
			{ xpos,     ypos + h,   ch.xPositions.x, ch.yPositions.y },
			{ xpos,     ypos,       ch.xPositions.x, ch.yPositions.x },
			

			{ xpos,     ypos + h,   ch.xPositions.x, ch.yPositions.y },
			{ xpos + w, ypos,       ch.xPositions.y, ch.yPositions.x },
			{ xpos + w, ypos + h,   ch.xPositions.y, ch.yPositions.y }

			/*{xpos,     ypos + h,   ch.xPositions.x, ch.yPositions.x},
			{ xpos,     ypos,       ch.xPositions.x, ch.yPositions.y },
			{ xpos + w, ypos,       ch.xPositions.y, ch.yPositions.y },

			{ xpos,     ypos + h,   ch.xPositions.x, ch.yPositions.x },
			{ xpos + w, ypos,       ch.xPositions.y, ch.yPositions.y },
			{ xpos + w, ypos + h,   ch.xPositions.y, ch.yPositions.x }*/
		};
		// render glyph texture over quad
		glBindTexture(GL_TEXTURE_2D, texture->id);
		// update content of VBO memory
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		glBufferSubData(GL_ARRAY_BUFFER, 0, sizeof(vertices), vertices);
		glBindBuffer(GL_ARRAY_BUFFER, 0);
		// render quad
		glDrawArrays(GL_TRIANGLES, 0, 6);
		// now advance cursors for next glyph (note that advance is number of 1/64 pixels)
		x += scale; // bitshift by 6 to get value in pixels (2^6 = 64)
	}
	glBindVertexArray(0);
	glBindTexture(GL_TEXTURE_2D, 0);
}

