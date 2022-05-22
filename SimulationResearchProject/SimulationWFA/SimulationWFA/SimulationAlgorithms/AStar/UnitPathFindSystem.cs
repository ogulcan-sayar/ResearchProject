using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationWFA.SimulationAlgorithms.DijkstraAlgorithm;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class UnitPathFindSystem : Dalak.Ecs.System
    {
        readonly Filter<UnitComp,TransformComp> unitFilter = null;
        readonly Filter<TargetComp, TransformComp> targetFilter = null;
        readonly Filter<GridComp> gridFilter = null;

        readonly Filter<VisualizeShortestPathComp> visualizeFilter = null;

        PathRequestManager pathRequestManager = null;

        public const float minPathUpdateTime = .2f;
        public const float pathUpdateMoveThreshold = .5f;


        public override void Update()
        {

            foreach(var u in unitFilter)
            {
                ref var unitComp = ref unitFilter.Get1(u);
                ref var transformComp = ref unitFilter.Get2(u);

                if (Input.GetKeyDown(KeyCode.M))
                {

                    Vector3 targetPosition = new Vector3();
                    foreach (var t in targetFilter)
                    {
                        ref var targetComp = ref targetFilter.Get1(t);
                        ref var targetTransformComp = ref targetFilter.Get2(t);
                        targetPosition = targetTransformComp.transform.position;
                        targetComp.initialPos = targetPosition;
                    }

                    ref var gridComp = ref gridFilter.Get1(0);

                    var waypoints = pathRequestManager.StartAlgorithms(transformComp.transform.position, targetPosition, gridComp.grid, out var algoritm);

                    if(waypoints != null)
                    {
                        unitComp.startPos = transformComp.transform.position;

                        var path = new Path(waypoints, transformComp.transform.position, unitComp.turnDst, unitComp.stoppingDst);

                        var unitEntity = unitFilter.GetEntity(u);
                        unitEntity.AddComponent<UnitFollowPathComp>() = new UnitFollowPathComp() {
                            path = path,
                            pathIndex = 0,
                            speedPercent = 1,
                            positionList = new List<Vector3>(),
                        };

                        if (!visualizeFilter.IsEmpty())
                        {
                            var oldVisualEntity = visualizeFilter.GetEntity(0);
                            oldVisualEntity.Destroy();
                        }

                        Entity visualizeEntity = world.NewEntity();
                        visualizeEntity.AddComponent<VisualizeShortestPathComp>() = new VisualizeShortestPathComp() {
                            shortestPathAlgorithm = algoritm,
                            visualizeData = new ShortestPathAlgorithm.VisualizeData(),
                            grid = gridComp.grid,
                            waypoints = waypoints,
                            backTracingLimit = 1,
                        };
                    }
                }

            }
        }

       

    }
}
