using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MESPSimulationSystem.Math;
using PhysicLibrary;
using SimulationSystem.ECSComponents;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class Grid
    {
        public bool displayGridGizmos;
        public PhysicsLayer unwalkableMask = PhysicsLayer.unwalkableLayer;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        public TerrainType[] walkableRegions = new TerrainType[1];
        public int obstacleProximityPenalty = 10;
        Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();
        PhysicsLayer walkableMask = PhysicsLayer.roadLayer;

        public Node[,] grid;

        public float nodeDiameter;
        public int gridSizeX, gridSizeY;

        public int penaltyMin = int.MaxValue;
        public int penaltyMax = int.MinValue;

        public void SetupGrid(Vector3 position, BoxBounds unitBound)
        {
            nodeDiameter = nodeRadius = unitBound.Size.X;
            gridSizeX = MathFunctions.RoundToInt(gridWorldSize.X / nodeDiameter);// Mat.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = MathFunctions.RoundToInt(gridWorldSize.Y / nodeDiameter);

            walkableRegions[0] = new TerrainType();
            walkableRegions[0].terrainMask = PhysicsLayer.roadLayer;
            walkableRegions[0].terrainPenalty = 0;

            foreach (TerrainType region in walkableRegions)
            {
                walkableMask.layerId = region.terrainMask.layerId;
                walkableRegionsDictionary.Add(region.terrainMask.layerId, region.terrainPenalty);
            }

            CreateGrid(position, unitBound);
        }

        public int MaxSize {
            get {
                return gridSizeX * gridSizeY;
            }
        }

        public void CreateGrid(Vector3 position,BoxBounds unitBound)
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector3 worldBottomLeft = position - Vector3.UnitX * gridWorldSize.X / 2 - Vector3.UnitZ * gridWorldSize.Y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.UnitX * (x * nodeDiameter + nodeRadius/2) + Vector3.UnitZ * (y * nodeDiameter + nodeRadius/2);


                    bool walkable = !(Physics.CheckBox(worldPoint,unitBound.Size, unwalkableMask));

                    int movementPenalty = 0;


                    Ray ray = new Ray(worldPoint + Vector3.UnitY * 50, -Vector3.UnitY);
                    //RaycastHit hit;

                    if (Physics.Raycast(ray, 100, out var hit, walkableMask))
                    {
                        Collider collider = hit.GetComponent<ColliderComp>().collider;
                        walkableRegionsDictionary.TryGetValue(collider.physicsLayer.layerId, out movementPenalty);
                    }

                    if (!walkable)
                    {
                        movementPenalty += obstacleProximityPenalty;
                    }


                    grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
                }
            }

            BlurPenaltyMap(3);

        }

        void BlurPenaltyMap(int blurSize)
        {
            int kernelSize = blurSize * 2 + 1;
            int kernelExtents = (kernelSize - 1) / 2;

            int[,] penaltiesHorizontalPass = new int[gridSizeX, gridSizeY];
            int[,] penaltiesVerticalPass = new int[gridSizeX, gridSizeY];

            for (int y = 0; y < gridSizeY; y++)
            {
                for (int x = -kernelExtents; x <= kernelExtents; x++)
                {
                    int sampleX = MathFunctions.Clamp(x, 0, kernelExtents);
                    penaltiesHorizontalPass[0, y] += grid[sampleX, y].movementPenalty;
                }

                for (int x = 1; x < gridSizeX; x++)
                {
                    int removeIndex = MathFunctions.Clamp(x - kernelExtents - 1, 0, gridSizeX);
                    int addIndex = MathFunctions.Clamp(x + kernelExtents, 0, gridSizeX - 1);

                    penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - grid[removeIndex, y].movementPenalty + grid[addIndex, y].movementPenalty;
                }
            }

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = -kernelExtents; y <= kernelExtents; y++)
                {
                    int sampleY = MathFunctions.Clamp(y, 0, kernelExtents);
                    penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
                }

                int blurredPenalty = MathFunctions.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
                grid[x, 0].movementPenalty = blurredPenalty;

                for (int y = 1; y < gridSizeY; y++)
                {
                    int removeIndex = MathFunctions.Clamp(y - kernelExtents - 1, 0, gridSizeY);
                    int addIndex = MathFunctions.Clamp(y + kernelExtents, 0, gridSizeY - 1);

                    penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y - 1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
                    blurredPenalty = MathFunctions.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
                    grid[x, y].movementPenalty = blurredPenalty;

                    if (blurredPenalty > penaltyMax)
                    {
                        penaltyMax = blurredPenalty;
                    }
                    if (blurredPenalty < penaltyMin)
                    {
                        penaltyMin = blurredPenalty;
                    }
                }
            }

        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }


        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = (worldPosition.X + gridWorldSize.X / 2) / gridWorldSize.X;
            float percentY = (worldPosition.Z + gridWorldSize.Y / 2) / gridWorldSize.Y;
            percentX = MathFunctions.Clamp(percentX, 0, 1);
            percentY = MathFunctions.Clamp(percentY, 0, 1);

            int x = MathFunctions.RoundToInt((gridSizeX - 1) * percentX);
            int y = MathFunctions.RoundToInt((gridSizeY - 1) * percentY);
            return grid[x, y];
        }


        [System.Serializable]
        public class TerrainType
        {
            public PhysicsLayer terrainMask;
            public int terrainPenalty;
        }


    }
}
