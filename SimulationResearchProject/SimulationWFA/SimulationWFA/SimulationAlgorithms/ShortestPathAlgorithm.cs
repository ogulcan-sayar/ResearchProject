using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;
using static SimulationWFA.SimulationAlgorithms.DijkstraAlgorithm.Dijkstra;

namespace SimulationWFA.SimulationAlgorithms
{
    public abstract class ShortestPathAlgorithm
    {
        public struct VisualizeData
        {
            public List<Node> searchedGrid;
            public Node currentSearchingNode;
            //dijkstra
            public Queue<Node> exploringGrid;
            public Stack<Node> exploringGridStack;
            public DijkstraNode[,] distanceGrid;

            //astar
            public Heap<Node> openSet;
            public HashSet<Node> closedSet;

        }

        public Node startNode;
        public Node targetNode;
        public Grid grid;

        public bool add;

        public abstract Vector3[] FindPath(Vector3 startPos, Vector3 targetPos, Grid grid);
        public abstract void VisualizePathSearch(ref VisualizeData visualizeData,out bool finished);


        public Vector3[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            Vector3[] waypoints = SimplifyPath(path);
            Array.Reverse(waypoints);
            return waypoints;

        }

        public Vector3[] SimplifyPath(List<Node> path)
        {
            List<Vector3> waypoints = new List<Vector3>();
            Vector2 directionOld = Vector2.Zero;

            for (int i = 1; i < path.Count; i++)
            {
              //  Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
              //  if (directionNew != directionOld)
               // {
                    waypoints.Add(path[i].worldPosition);
              //  }
              //  directionOld = directionNew;
            }
            return waypoints.ToArray();
        }

        public int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Math.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Math.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }

    }
}
