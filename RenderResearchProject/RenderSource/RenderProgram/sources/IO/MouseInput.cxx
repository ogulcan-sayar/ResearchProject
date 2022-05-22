#include "IO/MouseInput.h"

double MouseInput::x = 0;
double MouseInput::y = 0;

double MouseInput::lastX = 0;
double MouseInput::lastY = 0;

double MouseInput::dx = 0;
double MouseInput::dy = 0;

double MouseInput::scrollDx = 0;
double MouseInput::scrollDy = 0;

bool MouseInput::firstMouse = true;

bool MouseInput::buttons[GLFW_MOUSE_BUTTON_LAST] = { 0 };
bool MouseInput::buttonsChanged[GLFW_MOUSE_BUTTON_LAST] = { 0 };

void MouseInput::CursorPosCallback(GLFWwindow* window, double _x, double _y)
{
	x = _x;
	y = _y;

	if (firstMouse) {
		lastX = x;
		lastY = y;
		firstMouse = false;
	}

	dx = x - lastX;
	dy = lastY - y;
	lastX = x;
	lastY = y;
}

void MouseInput::MouseButtonCallback(GLFWwindow* window, int button, int action, int mods)
{
	if (action != GLFW_RELEASE)
	{
		if (!buttons[button]) {
			buttons[button] = true;
		}
	}
	else {
		buttons[button] = false;
	}

	buttonsChanged[button] = action != GLFW_REPEAT;
}

void MouseInput::MouseWheelCallback(GLFWwindow* window, double dx, double dy)
{
	scrollDx = dx;
	scrollDy = dy;
}

double MouseInput::GetMouseX()
{
	return x;
}

double MouseInput::GetMouseY()
{
	return y;
}

double MouseInput::GetDx()
{
	double _dx = dx;
	dx = 0;
	return _dx;
}

double MouseInput::GetDy()
{
	double _dy = dy;
	dy = 0;
	return _dy;
}

double MouseInput::GetScrollDx()
{
	double dx = scrollDx;
	scrollDx = 0;
	return dx;
}

double MouseInput::GetScrollDy()
{
	double dy = scrollDy;
	scrollDy = 0;
	return dy;
}

bool MouseInput::Button(int button)
{
	return buttons[button];
}

bool MouseInput::ButtonChanged(int button)
{
	bool ret = buttonsChanged[button];
	buttonsChanged[button] = false;
	return ret;
}

bool MouseInput::ButtonWentUp(int button)
{
	return !buttons[button] && ButtonChanged(button);
}

bool MouseInput::ButtonWentDown(int button)
{
	return buttons[button] && ButtonChanged(button);
}
