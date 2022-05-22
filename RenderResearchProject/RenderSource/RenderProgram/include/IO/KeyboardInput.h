#ifndef KEYBOARD_H
#define KEYBOARD_H

#include "GLAD/glad.h"
#include <GLFW/glfw3.h>

class KeyboardInput {
public:
	//key state callback
	static void KeyCallback(GLFWwindow* window, int key, int scancode, int action, int mods);

	// accessors

	static bool Key(int key);
	static bool KeyChanged(int key);
	static bool KeyWentUp(int key);
	static bool KeyWentDown(int key);

private:
	static bool keys[];
	static bool keysChanged[];
};


#endif
