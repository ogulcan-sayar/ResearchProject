using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;

namespace SimulationSystem.Systems
{
    public static class InputSystem
    {
        public static BitSet keyDownMask;
        public static BitSet keyUpMask;
        public static BitSet mouseKeyUpMask;
        public static BitSet mouseKeyDownMask;

        public static void Initialize()
        {
            keyDownMask = new BitSet();
            keyUpMask = new BitSet();
            mouseKeyDownMask = new BitSet();
            mouseKeyUpMask = new BitSet();
        }

        public static void ClearAll()
        {
            keyDownMask.ClearAll();
            keyUpMask.ClearAll();
            mouseKeyDownMask.ClearAll();
            mouseKeyUpMask.ClearAll();
        }
    }
}
