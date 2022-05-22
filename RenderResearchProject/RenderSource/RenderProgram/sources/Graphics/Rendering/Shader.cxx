#include "Graphics/Rendering/Shader.h"


Shader::Shader()
{
}

Shader::Shader(const char* vertexShaderPath, const char* fragShaderPath) {

	Generate(vertexShaderPath, fragShaderPath);
}

void Shader::Generate(const char* vertexShaderPath, const char* fragShaderPath)
{
	int success;
	char infoLog[512];

	GLuint vertexShader = CompileShader(vertexShaderPath, GL_VERTEX_SHADER);
	GLuint fragmentShader = CompileShader(fragShaderPath, GL_FRAGMENT_SHADER);

	id = glCreateProgram();

	glAttachShader(id, vertexShader);
	glAttachShader(id, fragmentShader);
	glLinkProgram(id);

	glGetProgramiv(id, GL_LINK_STATUS, &success);
	if (!success) {
		glGetProgramInfoLog(id, 512, NULL, infoLog);
		std::cout << "Linking error:" << std::endl << infoLog << std::endl;
	}

	glDeleteShader(vertexShader);
	glDeleteShader(fragmentShader);
}

//aktif shaderi belirlemek için kullanýlýyor
void Shader::Activate() {
	glUseProgram(id);
}

//utility functions
std::string Shader::LoadShaderSrc(const char* filename) {

	std::ifstream file;
	std::stringstream buf;
	std::string ret = "";

	file.open(filename);
	//std::cout<<std::experimental::filesystem::current_path()<<std::endl;

	if (file.is_open()) {

		buf << file.rdbuf();
		ret = buf.str();
	}
	else {
		std::cout << "Could not open " << filename << std::endl;
	}
	file.close();
	return ret;

}
GLuint Shader::CompileShader(const char* filePath, GLenum type) {
	int success;
	char infoLog[512];

	GLuint ret = glCreateShader(type);
	std::string shaderPath = LoadShaderSrc(filePath);
	const GLchar* shader = shaderPath.c_str();
	glShaderSource(ret, 1, &shader, NULL);
	glCompileShader(ret);

	// catch error
	glGetShaderiv(ret, GL_COMPILE_STATUS, &success);
	if (!success) {
		glGetShaderInfoLog(ret, 512, NULL, infoLog);
		std::cout << "Error with vertex shader comp.:" << std::endl << infoLog << std::endl;
	}

	return ret;
}

//uniform functions (shader içerisine deðer atamak için)
//fonksiyonlar aktif shader için çalýþacak o yüzden activate çaðýrýlýyor
//shader içerisindeki mat4 deðiþkenine deðer atar
void Shader::SetMat4(const std::string& name, glm::mat4 val) {
	glUniformMatrix4fv(glGetUniformLocation(id, name.c_str()), 1, GL_FALSE, glm::value_ptr(val));
}
//shader içerisindeki int deðiþkenine deðer atar
void Shader::SetInt(const std::string& name, int val) {
	glUniform1i(glGetUniformLocation(id, name.c_str()), val);
}

void Shader::SetFloat(const std::string& name, float val) {
	glUniform1f(glGetUniformLocation(id, name.c_str()), val);
}

void Shader::Set3Float(const std::string& name, glm::vec3 v)
{
	Set3Float(name, v.x, v.y, v.z);

}

void Shader::Set3Float(const std::string& name, float value, float value1, float value2)
{
	glUniform3f(glGetUniformLocation(id, name.c_str()), value, value1, value2);
}

void Shader::Set4Float(const std::string& name, float value, float value1, float value2, float value3)
{
	glUniform4f(glGetUniformLocation(id, name.c_str()), value, value1, value2, value3);
}

void Shader::Set4Float(const std::string& name, aiColor4D color)
{
	glUniform4f(glGetUniformLocation(id, name.c_str()), color.r, color.g, color.b, color.a);
}

void Shader::Set4Float(const std::string& name, glm::vec4 v)
{
	glUniform4f(glGetUniformLocation(id, name.c_str()), v.x, v.y, v.z, v.w);
}

void Shader::SetBool(const std::string& name, bool value)
{
	glUniform1i(glGetUniformLocation(id, name.c_str()), (int)value);
}

//Texture Functions
