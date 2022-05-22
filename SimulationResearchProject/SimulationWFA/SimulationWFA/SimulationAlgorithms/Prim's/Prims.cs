using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms
{
    public class Prims : ShortestPathAlgorithm
    {
        public override Vector3[] FindPath(Vector3 startPos, Vector3 targetPos, Grid grid)
        {

            Vector3[] waypoints = new Vector3[0];
            bool pathSuccess = false;

            this.grid = grid;
            startNode = grid.NodeFromWorldPoint(startPos);
            targetNode = grid.NodeFromWorldPoint(targetPos);

            List<Node> exploringGrid = new List<Node>();
            exploringGrid.Add(startNode);

            while (exploringGrid.Count > 0 && exploringGrid.Count < grid.gridSizeX * grid.gridSizeY)
            {
                float minDistance = float.MaxValue;
                Node tempNode = null;
                Node currentNode = null;

                for (int i = 0; i < exploringGrid.Count; i++)
                {
                    currentNode = exploringGrid[i];
                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (exploringGrid.Contains(neighbour) && exploringGrid.Contains(neighbour.parent)) continue;

                        if (neighbour == targetNode)
                        {
                            tempNode = neighbour;
                            break;
                        }

                        float dist = GetDistance(neighbour, currentNode);
                        if (neighbour.walkable && dist + neighbour.movementPenalty < minDistance)
                        {
                            minDistance = dist + neighbour.movementPenalty;
                            tempNode = neighbour;
                        }
                    }
                }
                tempNode.parent = currentNode;
                exploringGrid.Add(tempNode);

                if (tempNode == targetNode) break;
            }

            waypoints = RetracePath(startNode, targetNode);
            return waypoints;
        }

        public override void VisualizePathSearch(ref VisualizeData visualizeData, out bool finished)
        {
            if(visualizeData.searchedGrid == null)
            {
                visualizeData.searchedGrid = new List<Node>();
                visualizeData.searchedGrid.Add(startNode);
            }


            while (visualizeData.searchedGrid.Count > 0 && visualizeData.searchedGrid.Count < grid.gridSizeX * grid.gridSizeY)
            {
                int minDistance = int.MaxValue;
                Node tempNode = null;
                Node currentNode = null;

                for (int i = 0; i < visualizeData.searchedGrid.Count; i++)
                {
                    currentNode = visualizeData.searchedGrid[i];
                    visualizeData.currentSearchingNode = currentNode;
                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (visualizeData.searchedGrid.Contains(neighbour) && visualizeData.searchedGrid.Contains(neighbour.parent)) continue;

                        if (neighbour == targetNode)
                        {
                            tempNode = neighbour;
                            break;
                        }

                        int dist = GetDistance(neighbour, currentNode);
                        if (neighbour.walkable && dist + neighbour.movementPenalty < minDistance)
                        {
                            minDistance = dist + neighbour.movementPenalty;
                            tempNode = neighbour;
                        }
                    }
                }

                tempNode.parent = currentNode;
                visualizeData.searchedGrid.Add(tempNode);

                if (tempNode == targetNode) break;
                finished = false;
                return;
            }
            finished = true;

        }
    }
}
