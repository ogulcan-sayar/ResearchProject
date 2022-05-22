using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms.CustomAlgorithm
{
    public class Custom : ShortestPathAlgorithm
    {
        public override Vector3[] FindPath(Vector3 startPos, Vector3 targetPos, Grid grid)
        {
            return null;
        }

        public override void VisualizePathSearch(ref VisualizeData visualizeData, out bool finished)
        {
            finished = true;
        }
    }
}
