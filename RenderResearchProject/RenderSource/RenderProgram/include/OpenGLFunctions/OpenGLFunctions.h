#include "GLAD/glad.h"

class OpenGLFunctions {

public:

	static void GLEnable(int glEnum);
	static void GLDisable(int glEnum);
	static void GLClear(int glEnum);
	static void GLStencilMask(int mask);
	static void GLStencilFunc(int func, int ref, int mask);
	static void GLStencilOp(int sfail, int dpfail, int dppass);
	static void GLBlendFunc(int sfactor, int dfactor);
	static void GLDepthFunc(int func);

};