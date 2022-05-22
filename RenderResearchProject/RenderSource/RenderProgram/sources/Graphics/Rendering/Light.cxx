#include "Graphics/Rendering/Light.h"

void PointLight::Render(Shader shader, int idx)
{
	std::string name = "pointLight[" + std::to_string(idx) + "]";

	shader.Set3Float(name + ".position", position);
	shader.SetFloat(name + ".k0", k0);
	shader.SetFloat(name + ".k1", k1);
	shader.SetFloat(name + ".k2", k2);
	shader.Set4Float(name +  ".ambient", ambient);
	shader.Set4Float(name + ".diffuse", diffuse);
	shader.Set4Float(name + ".specular", specular);
}

void DirectionalLight::Render(Shader shader)
{
	std::string name = "directionalLight";

	shader.Set3Float(name + ".direction", direction);
	shader.Set4Float(name + ".ambient", ambient);
	shader.Set4Float(name + ".diffuse", diffuse);
	shader.Set4Float(name + ".specular", specular);
}

void SpotLight::Render(Shader shader, int idx)
{
	std::string name = "spotLight[" + std::to_string(idx) + "]";

	shader.Set3Float(name + ".position", position);
	shader.Set3Float(name + ".direction", direction);

	shader.SetFloat(name + ".k0", k0);
	shader.SetFloat(name + ".k1", k1);
	shader.SetFloat(name + ".k2", k2);
	shader.SetFloat(name + ".cutOff", cutOff);
	shader.SetFloat(name + ".outerCutOff", outerCutOff);

	shader.Set4Float(name + ".ambient", ambient);
	shader.Set4Float(name + ".diffuse", diffuse);
	shader.Set4Float(name + ".specular", specular);
}
