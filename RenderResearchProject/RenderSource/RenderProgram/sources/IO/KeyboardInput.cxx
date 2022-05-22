#include "IO/KeyboardInput.h"

bool KeyboardInput::keys[GLFW_KEY_LAST] = { 0 };
bool KeyboardInput::keysChanged[GLFW_KEY_LAST] = { 0 };



void KeyboardInput::KeyCallback(GLFWwindow* window, int key, int scancode, int action, int mods)
{
	if (action != GLFW_RELEASE) 
	{
		if (!keys[key]) {
			keys[key] = true;
		}
	}
	else {
		keys[key] = false;
	}

	keysChanged[key] = action != GLFW_REPEAT;
}

bool KeyboardInput::Key(int key)
{
	return keys[key];
}

bool KeyboardInput::KeyChanged(int key)
{
	bool ret = keysChanged[key];
	keysChanged[key] = false;
	return ret;
}

bool KeyboardInput::KeyWentUp(int key)
{
	return !keys[key] && KeyChanged(key);
}

bool KeyboardInput::KeyWentDown(int key)
{
	return keys[key] && KeyChanged(key);
}
