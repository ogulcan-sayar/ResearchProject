#ifndef  MOUSEINPUT_H
#define MOUSEINPUT_H

#include <GLAD/glad.h>
#include <GLFW/glfw3.h>


class MouseInput {
public:
	static void CursorPosCallback(GLFWwindow* window, double x, double y);
	static void MouseButtonCallback(GLFWwindow* window, int button, int action, int mods);
	static void MouseWheelCallback(GLFWwindow* window, double dx, double dy);
	
	static double GetMouseX();
	static double GetMouseY();

	static double GetDx();
	static double GetDy();

	static double GetScrollDx();
	static double GetScrollDy();

	static bool Button(int button);
	static bool ButtonChanged(int button);
	static bool ButtonWentUp(int button);
	static bool ButtonWentDown(int button);

private:
	static double x;
	static double y;

	static double lastX;
	static double lastY;

	static double dx;
	static double dy;

	static double scrollDx;
	static double scrollDy;

	static bool firstMouse;

	static bool buttons[];
	static bool buttonsChanged[];

};


#endif // ! MOUSEýNPUT_H
