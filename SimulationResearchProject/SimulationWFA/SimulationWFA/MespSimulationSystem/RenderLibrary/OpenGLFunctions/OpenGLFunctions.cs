using RenderLibrary.DLL;

namespace RenderLibrary.OpenGLCustomFunctions
{
    public static class OpenGLFunctions
    {
        public static void GLEnable(OpenGLEnum glEnum)
        {
            RenderProgramDLL.OpenGLEnable((int) glEnum);
        }

        public static void GLDisable(OpenGLEnum glEnum)
        {
            RenderProgramDLL.OpenGLDisable((int)glEnum);
        }

        public static void GLClear(OpenGLEnum gLEnum)
        {
            RenderProgramDLL.OpenGLClear((int)gLEnum);
        }

        public static void GLStencilMask(int mask)
        {
            RenderProgramDLL.OpenGLStencilMask(mask);
        }

        public static void GLStencilFunc(OpenGLEnum func, int refVal, int mask)
        {
            RenderProgramDLL.OpenGLStencilFunc((int)func, refVal, mask);
        }

        public static void GLStencilOp(OpenGLEnum sfail, OpenGLEnum dpfail, OpenGLEnum dppass)
        {
            RenderProgramDLL.OpenGLStencilOp((int)sfail, (int)dpfail, (int)dppass);
        }

        public static void GLBlendFunc(OpenGLEnum sfactor, OpenGLEnum dfactor)
        {
            RenderProgramDLL.OpenGLBlendFunc((int)sfactor, (int)dfactor);
        }

        public static void GLDepthFunc(OpenGLEnum func)
        {
            RenderProgramDLL.OpenGLDepthFunc((int)func);
        }
    }

    public enum OpenGLEnum
    {
        GL_DEPTH_BUFFER_BIT = 256,
        GL_STENCIL_BUFFER_BIT = 1024,
        GL_COLOR_BUFFER_BIT = 16384,
        GL_DEPTH_TEST = 2929,
        GL_STENCIL_TEST = 2960,
        GL_BLEND = 0x0BE2,
        GL_CULL_FACE =  0x0B44,
        GL_NEVER = 0x0200,
        GL_LESS = 0x0201,
        GL_EQUAL = 0x0202,
        GL_LEQUAL = 0x0203,
        GL_GREATER = 0x0204,
        GL_NOTEQUAL = 0x0205,
        GL_GEQUAL = 0x0206,
        GL_ALWAYS = 0x0207,
        GL_ZERO = 0,
        GL_ONE = 1,
        GL_KEEP = 0x1E00,
        GL_REPLACE = 0x1E01,
        GL_INCR = 0x1E02,
        GL_DECR = 0x1E03,
        GL_INCR_WRAP = 0x8507,
        GL_DECR_WRAP = 0x8508,
        GL_INVERT = 0x150A,
        GL_SRC_COLOR = 0x0300,
        GL_ONE_MINUS_SRC_COLOR = 0x0301,
        GL_SRC_ALPHA = 0x0302,
        GL_ONE_MINUS_SRC_ALPHA = 0x0303,
        GL_DST_ALPHA = 0x0304,
        GL_ONE_MINUS_DST_ALPHA = 0x0305,
        GL_DST_COLOR = 0x0306,
        GL_ONE_MINUS_DST_COLOR = 0x0307,
        GL_CONSTANT_COLOR = 0x8001,
        GL_ONE_MINUS_CONSTANT_COLOR = 0x8002,
        GL_CONSTANT_ALPHA = 0x8003,
        GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004,
    }
}
