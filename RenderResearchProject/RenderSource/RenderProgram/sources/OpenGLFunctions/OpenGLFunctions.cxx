#include "..\..\include\OpenGLFunctions\OpenGLFunctions.h"


void OpenGLFunctions::GLEnable(int glEnum)
{
	glEnable(glEnum);
}

void OpenGLFunctions::GLDisable(int glEnum)
{
	glDisable(glEnum);
}

void OpenGLFunctions::GLClear(int glEnum)
{
	glClear(glEnum);
}

void OpenGLFunctions::GLStencilMask(int mask)
{
	glStencilMask(mask);
}

void OpenGLFunctions::GLStencilFunc(int func, int ref, int mask)
{
	glStencilFunc(func, ref, mask);
}

void OpenGLFunctions::GLStencilOp(int sfail, int dpfail, int dppass)
{
	glStencilOp(sfail, dpfail, dppass);
}

void OpenGLFunctions::GLBlendFunc(int sfactor, int dfactor)
{
	glBlendFunc(sfactor, dfactor);
}

void OpenGLFunctions::GLDepthFunc(int func)
{
	glDepthFunc(func);
}




