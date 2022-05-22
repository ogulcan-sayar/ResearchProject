using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using ProgramLibrary;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.TimeUtils;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class VisualizeShortestPathAlgorithmSystem : Dalak.Ecs.System
    {
        readonly Filter<VisualizeShortestPathComp> visualizeFilter = null;
        readonly Filter<UnitFollowPathComp> unitFollowFilter = null;

        public Timer pathFindTimer = new Timer(.5f);
        public Timer backTracingTimer = new Timer(.5f);

        public override void Awake()
        {
            pathFindTimer.ForceComplete();
        }

        public override void Update()
        {
            foreach (var v in visualizeFilter)
            {
                if (!pathFindTimer.Update(Time.deltaTime)) continue;


                ref var visualizeComp = ref visualizeFilter.Get1(v);
                visualizeComp.shortestPathAlgorithm.VisualizePathSearch(ref visualizeComp.visualizeData, out var finished);
                pathFindTimer.Restart();

                if (finished)
                {
                    visualizeComp.finished = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.N) && !visualizeFilter.IsEmpty())
            {
                var unitEntity = unitFollowFilter.GetEntity(0);
                unitEntity.AddComponent<StartPathFollowComp>();

                var visualizEntity = visualizeFilter.GetEntity(0);
                visualizEntity.Destroy();

            }


        }

        public override void PostRender()
        {
            foreach (var v in visualizeFilter)
            {
                ref var visualizeComp = ref visualizeFilter.Get1(v);


                if (visualizeComp.finished)
                {
                    for(int i= visualizeComp.waypoints.Length-1; i> visualizeComp.waypoints.Length - visualizeComp.backTracingLimit-1 && i >0; i--)
                    {
                        var p1 = visualizeComp.waypoints[i-1];
                        p1.Y += 0.3f;

                        var p2 = visualizeComp.waypoints[i];
                        p2.Y += 0.3f;

                        MespDebug.DrawLine(p1, p2, new Vector3(0, 1, 1));

                        if (backTracingTimer.Update(Time.deltaTime))
                        {
                            visualizeComp.backTracingLimit++;

                            backTracingTimer.Restart();
                        }
                    }
                }
                else
                {
                    if (visualizeComp.visualizeData.currentSearchingNode == null) continue;
                    var currentNode = visualizeComp.visualizeData.currentSearchingNode.worldPosition;
                    currentNode.Y += 0.2f;

                    MespDebug.DrawWireBoxXZ(currentNode, new Vector3(1 * visualizeComp.grid.nodeDiameter, 0, 1 * visualizeComp.grid.nodeDiameter) / 4, new Vector3(0, 1, 1));

                    for (int i = 0; i < visualizeComp.visualizeData.searchedGrid.Count; i++)
                    {
                        var gridPosition = visualizeComp.visualizeData.searchedGrid[i].worldPosition;
                        gridPosition.Y += 0.2f;

                        var parent = visualizeComp.visualizeData.searchedGrid[i].parent;
                        if (parent != null)
                        {
                            var parentPosition = parent.worldPosition;
                            parentPosition.Y += 0.2f;
                            MespDebug.DrawLine(gridPosition, parentPosition, new Vector3(1, 0, 1));
                        }


                        MespDebug.DrawWireBoxXZ(gridPosition, new Vector3(1 * visualizeComp.grid.nodeDiameter, 0, 1 * visualizeComp.grid.nodeDiameter) / 2, new Vector3(0, 0, 1));
                    }
                }

            }
        }

    }
}
