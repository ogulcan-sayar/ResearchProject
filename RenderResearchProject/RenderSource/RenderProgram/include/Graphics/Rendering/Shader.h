#ifndef SHADER_H
#define SHADER_H


#include "GLAD/glad.h"
#include "string"
#include "fstream"
#include "sstream"
#include "iostream"

#include "ASSIMP/scene.h"

#include "GLM/glm.hpp"
#include "GLM/gtc/type_ptr.hpp"

class Shader {

public:
	unsigned int id;

	Shader();
	Shader(const char* vertexShaderPath, const char* fragShaderPath);

	void Generate(const char* vertexShaderPath, const char* fragShaderPath);
	void Activate();

	//utility functions
	std::string LoadShaderSrc(const char* filePath);
	GLuint CompileShader(const char* filePath, GLenum type);

	//uniform functions
	void SetMat4(const std::string& name, glm::mat4 val);
	void SetInt(const std::string& name, int value);
	void SetFloat(const std::string& name, float value);
	void Set3Float(const std::string& name, glm::vec3 v);
	void Set3Float(const std::string& name, float value, float value1, float value2);
	void Set4Float(const std::string& name, float value, float value1, float value2, float value3);
	void Set4Float(const std::string& name, aiColor4D color);
	void Set4Float(const std::string& name, glm::vec4 v);
	void SetBool(const std::string& name, bool value);
};

#endif