#ifndef SCREEN_H
#define SCREEN_h

#include "GLAD/glad.h"
#include "GLFW/glfw3.h"
#include "GLM/glm.hpp"

#include "IO/KeyboardInput.h"
#include "IO/MouseInput.h"

class Screen {
public:
	unsigned int SCR_WIDTH;
	unsigned int SCR_HEIGHT;

	glm::vec4 clearColor;

	static void FramebufferSizeCallback(GLFWwindow* window, int width, int height);
	Screen(int _SCR_WIDTH, int _SCR_HEIGHT);

	void ConfigureGLFW();
	bool Initialize();
	bool CheckGladInitialization();
	void SetParameters();
	void Terminate();

	//main loop
	void Update();
	void NewFrame();

	// window closing accessor and modifier
	bool ShouldClose();
	void SetShouldClose(bool shouldClose);

	//input
	void ProcessInput();


private:
	GLFWwindow* window;

};

#endif 
