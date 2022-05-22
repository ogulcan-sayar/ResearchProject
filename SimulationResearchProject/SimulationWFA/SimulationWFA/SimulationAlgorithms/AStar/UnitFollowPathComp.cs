using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public struct UnitFollowPathComp
    {
        public Path path;
        public int pathIndex;
        public float speedPercent;
        public List<Vector3> positionList;
    }
}
