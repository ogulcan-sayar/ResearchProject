using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationSystem.ECSComponents
{
    struct FPSDisplayerComp
    {
        public const float sampleDuration = 1f;
        public int frame;
        public float totalDuration;
    }
}
