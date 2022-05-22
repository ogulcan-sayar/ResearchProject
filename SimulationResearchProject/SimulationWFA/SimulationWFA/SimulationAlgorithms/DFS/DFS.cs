using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms
{
    public class DFS : ShortestPathAlgorithm
    {
        public override Vector3[] FindPath(Vector3 startPos, Vector3 targetPos, Grid grid)
        {
            Vector3[] waypoints = new Vector3[0];
            bool pathSuccess = false;

            this.grid = grid;
            startNode = grid.NodeFromWorldPoint(startPos);
            targetNode = grid.NodeFromWorldPoint(targetPos);

            Stack<Node> exploringGrid = new Stack<Node>();
            List<Node> exploredGrid = new List<Node>();
            exploringGrid.Push(startNode);
            exploredGrid.Add(startNode);

            while (exploringGrid.Count > 0)
            {
                Node currentNode = exploringGrid.Pop();

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (neighbour == targetNode)
                    {
                        neighbour.parent = currentNode;
                        pathSuccess = true;
                        break;
                    }

                    if (neighbour.walkable && !exploredGrid.Contains(neighbour))
                    {
                        neighbour.parent = currentNode;
                        exploringGrid.Push(neighbour);
                        exploredGrid.Add(neighbour);
                    }
                }

                if (pathSuccess)
                {
                    break;
                }

            }

            waypoints = RetracePath(startNode, targetNode);
            return waypoints;
        }

        public override void VisualizePathSearch(ref VisualizeData visualizeData, out bool finished)
        {
            if (visualizeData.searchedGrid == null)
            {
                visualizeData.searchedGrid = new List<Node>();

                visualizeData.exploringGridStack = new Stack<Node>();
                visualizeData.exploringGridStack.Push(startNode);
                visualizeData.searchedGrid.Add(startNode);
            }

            finished = false;

            while (visualizeData.exploringGridStack.Count > 0)
            {
                Node currentNode = visualizeData.exploringGridStack.Pop();
                visualizeData.currentSearchingNode = currentNode;

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (neighbour == targetNode)
                    {
                        neighbour.parent = currentNode;
                        finished = true;
                        break;
                    }

                    if (neighbour.walkable && !visualizeData.searchedGrid.Contains(neighbour))
                    {
                        neighbour.parent = currentNode;
                        visualizeData.exploringGridStack.Push(neighbour);
                        visualizeData.searchedGrid.Add(neighbour);
                    }
                }

                if (finished)
                {
                    break;
                }

                return;
            }

            finished = true;
            return;
        }
    }
}

