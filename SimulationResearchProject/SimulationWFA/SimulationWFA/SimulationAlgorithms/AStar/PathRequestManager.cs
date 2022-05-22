using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationWFA.SimulationAlgorithms.AStar;
using SimulationWFA.SimulationAlgorithms.DijkstraAlgorithm;

namespace SimulationWFA.SimulationAlgorithms
{
    public class PathRequestManager
    {
        public delegate void OnAlgorithmDoneDelegate();

        public static event OnAlgorithmDoneDelegate OnAlgoDoneEvent;

        public static string findedAlgorithmName = "NONE";

        IEnumerable<ShortestPathAlgorithm> algorithms;
        bool isProcessingPath;

        public static Dictionary<string, bool> algorithmDictionary = new Dictionary<string, bool>() {
            { "AStar",  false },
            { "Dijkstra",  false },
            { "Prims",  false },
            { "DFS",  false },
            { "BFS",  false },
            { "Custom",  false },
        };

        public static Dictionary<string, string> algorithmMSDictionary = new Dictionary<string, string>()
        {
             { "AStar",  "NONE" },
            { "Dijkstra",  "NONE" },
            { "Prims",  "NONE" },
            { "DFS",  "NONE" },
            { "BFS",  "NONE" },
            { "Custom",  "NONE" },
        };

        public static Dictionary<string, string> algorithmDistanceDictionary = new Dictionary<string, string>()
        {
             { "AStar",  "NONE" },
            { "Dijkstra",  "NONE" },
            { "Prims",  "NONE" },
            { "DFS",  "NONE" },
            { "BFS",  "NONE" },
            { "Custom",  "NONE" },
        };


        public Vector3[] StartAlgorithms(Vector3 pathStart, Vector3 pathEnd, Grid grid, out ShortestPathAlgorithm foundedAlgorithm)
        {
            algorithmMSDictionary = new Dictionary<string, string>()
         {
             { "AStar",  "NONE" },
            { "Dijkstra",  "NONE" },
            { "Prims",  "NONE" },
            { "DFS",  "NONE" },
            { "BFS",  "NONE" },
            { "Custom",  "NONE" },
        };

            algorithmDistanceDictionary = new Dictionary<string, string>()
            {
             { "AStar",  "NONE" },
            { "Dijkstra",  "NONE" },
            { "Prims",  "NONE" },
            { "DFS",  "NONE" },
            { "BFS",  "NONE" },
            { "Custom",  "NONE" },
        };


            findedAlgorithmName = "NONE";
            algorithms = GetAllShortesPathAlgorithm();

            List<ShortestPathAlgorithm> activeAlgorithms = new List<ShortestPathAlgorithm>();

            foreach (var algorithm in algorithms)
            {
                if (algorithmDictionary[algorithm.GetType().Name])
                {
                    activeAlgorithms.Add(algorithm);
                }
            }

            Stopwatch sw = new Stopwatch();

            foundedAlgorithm = null;
            double shortestTimePassed = double.MaxValue;
            Type shortestPathType = null;
            Vector3[] waypoints = null;
            float tempDistance = float.MaxValue;
            foreach (var algorithm in activeAlgorithms)
            {
                sw.Reset();
                sw.Start();

                waypoints = algorithm.FindPath(pathStart, pathEnd, grid);

                sw.Stop();
                Console.WriteLine(algorithm.GetType().Name + " found: " + sw.Elapsed.TotalMilliseconds + " ms");

                float currentDist = GetDistanceMagnitude(waypoints);
                double currentShortestTimePassed = sw.Elapsed.TotalMilliseconds;
                algorithmMSDictionary[algorithm.GetType().Name] = currentShortestTimePassed.ToString();
                algorithmDistanceDictionary[algorithm.GetType().Name] = currentDist.ToString();

                if (currentDist < tempDistance)
                {
                    tempDistance = currentDist;
                    foundedAlgorithm = algorithm;
                    shortestPathType = algorithm.GetType();
                }

                else if (currentDist == tempDistance && currentShortestTimePassed < shortestTimePassed)
                {
                    tempDistance = currentDist;
                    foundedAlgorithm = algorithm;
                    shortestPathType = algorithm.GetType();
                }

                if (currentShortestTimePassed < shortestTimePassed)
                {
                    shortestTimePassed = currentShortestTimePassed;
                }

                
            }
            findedAlgorithmName = shortestPathType.Name;
            if (OnAlgoDoneEvent != null) OnAlgoDoneEvent();

            Console.WriteLine("Chosen algoritm: " + shortestPathType.Name);
            return waypoints;
        }


        IEnumerable<ShortestPathAlgorithm> GetAllShortesPathAlgorithm()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(ShortestPathAlgorithm)))
                .Select(type => Activator.CreateInstance(type) as ShortestPathAlgorithm);
        }

        public float GetDistanceMagnitude(Vector3[] path)
        {
            float totalDistance = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                totalDistance += (path[i + 1] - path[i]).Length();
            }

            return totalDistance;
        }

    }
}
