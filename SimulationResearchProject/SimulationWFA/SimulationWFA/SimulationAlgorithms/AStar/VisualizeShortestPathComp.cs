using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static SimulationWFA.SimulationAlgorithms.ShortestPathAlgorithm;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public struct VisualizeShortestPathComp
    {
        public ShortestPathAlgorithm shortestPathAlgorithm;
        public VisualizeData visualizeData;
        public Grid grid;
        public bool finished;
        public Vector3[] waypoints;

        public int backTracingLimit;
    }
}
