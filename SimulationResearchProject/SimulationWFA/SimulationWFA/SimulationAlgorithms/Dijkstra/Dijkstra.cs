using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms.DijkstraAlgorithm
{
    public class Dijkstra : ShortestPathAlgorithm
    {
        public class DijkstraNode
        {
            public float distance = float.MaxValue;
        }

        public override Vector3[] FindPath(Vector3 startPos, Vector3 targetPos, Grid grid)
        {
            Vector3[] waypoints = new Vector3[0];
            bool pathSuccess = false;

            this.grid = grid;
            startNode = grid.NodeFromWorldPoint(startPos);
            targetNode = grid.NodeFromWorldPoint(targetPos);

            DijkstraNode[,] distanceGrid = new DijkstraNode[grid.gridSizeX, grid.gridSizeY];
            for (int x = 0; x < grid.gridSizeX; x++)
            {
                for (int y = 0; y < grid.gridSizeY; y++)
                {
                    distanceGrid[x, y] = new DijkstraNode();
                    distanceGrid[x, y].distance = int.MaxValue;
                }
            }
            distanceGrid[startNode.gridX, startNode.gridY].distance = 0;

            Queue<Node> exploringGrid = new Queue<Node>();
            exploringGrid.Enqueue(startNode);

            while (exploringGrid.Count > 0)
            {
                Node currentNode = exploringGrid.Dequeue();

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    var tempNeighborDist = GetDistance(neighbour, currentNode);

                    if (neighbour.walkable && distanceGrid[neighbour.gridX, neighbour.gridY].distance > distanceGrid[currentNode.gridX, currentNode.gridY].distance + tempNeighborDist+ neighbour.movementPenalty)
                    {
                        distanceGrid[neighbour.gridX, neighbour.gridY].distance = distanceGrid[currentNode.gridX, currentNode.gridY].distance + tempNeighborDist + neighbour.movementPenalty;
                        neighbour.parent = currentNode;

                        exploringGrid.Enqueue(neighbour);
                    }
                }
            }

            waypoints = RetracePath(startNode, targetNode);
            return waypoints;

        }

        public override void VisualizePathSearch(ref VisualizeData visualizeData, out bool finished)
        {
            if(visualizeData.searchedGrid == null)
            {
                visualizeData.searchedGrid = new List<Node>();

                visualizeData.distanceGrid = new DijkstraNode[grid.gridSizeX, grid.gridSizeY];
                for (int x = 0; x < grid.gridSizeX; x++)
                {
                    for (int y = 0; y < grid.gridSizeY; y++)
                    {
                        visualizeData.distanceGrid[x, y] = new DijkstraNode();
                        visualizeData.distanceGrid[x, y].distance = int.MaxValue;
                    }
                }
                visualizeData.distanceGrid[startNode.gridX, startNode.gridY].distance = 0;

                visualizeData.exploringGrid = new Queue<Node>();
                visualizeData.exploringGrid.Enqueue(startNode);

                visualizeData.searchedGrid.Add(startNode);
            }

            while (visualizeData.exploringGrid.Count > 0)
            {
                Node currentNode = visualizeData.exploringGrid.Dequeue();
                visualizeData.currentSearchingNode = currentNode;

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    var tempNeighborDist = GetDistance(neighbour, currentNode);

                    if (neighbour.walkable && visualizeData.distanceGrid[neighbour.gridX, neighbour.gridY].distance > visualizeData.distanceGrid[currentNode.gridX, currentNode.gridY].distance + tempNeighborDist + neighbour.movementPenalty)
                    {
                        visualizeData.distanceGrid[neighbour.gridX, neighbour.gridY].distance = visualizeData.distanceGrid[currentNode.gridX, currentNode.gridY].distance + tempNeighborDist + neighbour.movementPenalty;
                        neighbour.parent = currentNode;

                        if (!visualizeData.searchedGrid.Contains(neighbour))
                        {
                            visualizeData.exploringGrid.Enqueue(neighbour);
                            visualizeData.searchedGrid.Add(neighbour);
                        }
                    }
                }
                finished = false;
                return;
            }

            finished = true;
        }
    }
}
