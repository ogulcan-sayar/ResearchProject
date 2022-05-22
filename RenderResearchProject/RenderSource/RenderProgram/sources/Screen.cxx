#include <iostream>
#include "Screen.h"
#include "IO/KeyboardInput.h"
#include "IO/MouseInput.h"
#include "OpenGLFunctions/OpenGLFunctions.h"



void Screen::FramebufferSizeCallback(GLFWwindow* window, int width, int height)
{
	glViewport(0, 0, width, height);
}

Screen::Screen(int _SCR_WIDTH, int _SCR_HEIGHT)
	: window(nullptr) {
	
	SCR_WIDTH = _SCR_WIDTH;
	SCR_HEIGHT = _SCR_HEIGHT;
}



void Screen::ConfigureGLFW()
{
	if (!glfwInit()) {
		std::cout << "GLFW not initialization" << std::endl;
		return;
	}
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
	glfwWindowHint(GLFW_STENCIL_BITS, 8);
	glfwWindowHint(GLFW_DEPTH_BITS, 24);
}



bool Screen::Initialize()
{
	window = glfwCreateWindow(SCR_WIDTH, SCR_HEIGHT, "MESP Simulator", NULL, NULL);

	if (!window)
	{
		std::cerr << "Window not created" << std::endl;
		glfwTerminate();
		return false;
	}

	glfwMakeContextCurrent(window);
	return true;
}

bool Screen::CheckGladInitialization()
{
	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
	{
		std::cout << "Failed to initialize GLAD" << std::endl;
		return true;
	}
	return false;
}

void Screen::SetParameters()
{
	glViewport(0, 0, SCR_WIDTH, SCR_HEIGHT);

	glfwSetFramebufferSizeCallback(window, Screen::FramebufferSizeCallback); //büyültüp küçültüldüðünde renderin boyutunu ayarlar
	glfwSetCursorPosCallback(window, MouseInput::CursorPosCallback);
	glfwSetMouseButtonCallback(window, MouseInput::MouseButtonCallback);
	glfwSetScrollCallback(window, MouseInput::MouseWheelCallback);
	glfwSetKeyCallback(window, KeyboardInput::KeyCallback);

	//glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);
}

void Screen::Terminate()
{
	glfwTerminate();
}

void Screen::Update()
{
	glClearColor(clearColor.r, clearColor.g, clearColor.b, clearColor.a);
}

void Screen::NewFrame()
{
	// glfw: swap buffers and poll IO events (keys pressed/released, mouse moved etc.)
	glfwSwapBuffers(window);
	glfwPollEvents();
}

bool Screen::ShouldClose()
{
	return glfwWindowShouldClose(window);
}

void Screen::SetShouldClose(bool shouldClose)
{
	glfwSetWindowShouldClose(window, shouldClose);
}

void Screen::ProcessInput()
{
		if (KeyboardInput::Key(GLFW_KEY_ESCAPE)) {
			SetShouldClose(true);
		}
}
