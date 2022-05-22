using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms
{
    public struct UnitReturnPathComp
    {
        public List<Vector3> pathPositions;
        public int index;
        public Path path;
    }
}
