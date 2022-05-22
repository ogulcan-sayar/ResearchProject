#ifndef LAMP_H
#define LAMP_H

#include "Cube.h"
#include "Graphics/Rendering/Light.h"

class Lamp : public Cube {

public:

	//light strenth values
	PointLight pointLight;

	Lamp() {}

	Lamp(glm::vec4 ambient,
		glm::vec4 diffuse,
		glm::vec4 specular,
		float k0,
		float k1,
		float k2,
		glm::vec3 pos)
		:	pointLight({ pos, k0, k1, k2, ambient, diffuse, specular }) {}
};

#endif