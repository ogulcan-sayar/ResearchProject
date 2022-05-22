using RenderLibrary.DLL;
using SimulationSystem.Systems;

namespace RenderLibrary.IO
{
    class Input
    {
        public static bool GetKeyDown(KeyCode keycode)
        {
            if (InputSystem.keyDownMask.IsSet((int)keycode))
            {
                return true;
            }
            else if (RenderProgramDLL.GetKeyDown((int)keycode))
            {
                InputSystem.keyDownMask.SetBit((int)keycode);
            }

            return InputSystem.keyDownMask.IsSet((int)keycode);
        }

        public static bool GetKeyUp(KeyCode keycode)
        {
            if (InputSystem.keyUpMask.IsSet((int)keycode))
            {
                return true;
            }
            else if (RenderProgramDLL.GetKeyUp((int)keycode))
            {
                InputSystem.keyUpMask.SetBit((int)keycode);
            }

            return InputSystem.keyUpMask.IsSet((int)keycode);
        }

        public static bool GetKey(KeyCode keycode)
        {
            return RenderProgramDLL.GetKey((int)keycode);
        }

        public static bool GetMouseKeyDown(int key)
        {
            if (InputSystem.mouseKeyDownMask.IsSet(key))
            {
                return true;
            }
            else if (RenderProgramDLL.GetMouseKeyDown(key))
            {
                InputSystem.mouseKeyDownMask.SetBit(key);
            }

            return InputSystem.mouseKeyDownMask.IsSet(key);
        }

        public static bool GetMouseKeyUp(int key)
        {
            if (InputSystem.mouseKeyUpMask.IsSet(key))
            {
                return true;
            }
            else if (RenderProgramDLL.GetMouseKeyUp(key))
            {
                InputSystem.mouseKeyUpMask.SetBit(key);
            }

            return InputSystem.mouseKeyUpMask.IsSet(key);
        }

        public static bool GetMouseKey(int key)
        {
            return RenderProgramDLL.GetMouseKey(key);
        }

        public static double GetMouseDx()
        {
            return RenderProgramDLL.GetDx();
        }

        public static double GetMouseDy()
        {
            return RenderProgramDLL.GetDy();
        }

        public static double GetMousePosX()
        {
            return RenderProgramDLL.GetMouseX();
        }

        public static double GetMousePosY()
        {
            return RenderProgramDLL.GetMouseY();
        }

        public static double GetMouseScrolDx()
        {
            return RenderProgramDLL.GetScrollDx();
        }

        public static double GetMouseScrolDy()
        {
            return RenderProgramDLL.GetScrollDy();
        }
    }

    public enum KeyCode
    {
        KeyPad0 = 48,
        KeyPad1 = 49,
        KeyPad2 = 50,
        KeyPad3 = 51,
        KeyPad4 = 52,
        KeyPad5 = 53,
        KeyPad6 = 54,
        KeyPad7 = 55,
        KeyPad8 = 56,
        KeyPad9 = 57,
        A = 65,
 	    B = 66,
 	    C = 67,
 	    D = 68,
 	    E = 69,
 	    F = 70,
 	    G = 71,
 	    H = 72,
 	    I = 73,
 	    J = 74,
 	    K = 75,
 	    L = 76,
 	    M = 77,
 	    N = 78,
 	    O = 79,
 	    P = 80,
 	    Q = 81,
 	    R = 82, 
 	    S = 83,
 	    T = 84,
 	    U = 85,
 	    V = 86,
 	    W = 87,
 	    X = 88,
 	    Y = 89,
 	    Z = 90,
        Space = 32,
        LeftShift = 340,
    }
}
