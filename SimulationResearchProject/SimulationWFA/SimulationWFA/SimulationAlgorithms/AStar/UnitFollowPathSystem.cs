using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using MespSimulationSystem.Math;
using MESPSimulationSystem.Math;
using SimulationSystem.Components;
using SimulationSystem.TimeUtils;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class UnitFollowPathSystem : Dalak.Ecs.System
    {
        readonly Filter<UnitFollowPathComp, UnitComp, TransformComp,StartPathFollowComp> unitFollowFilter = null;

        public override void Update()
        {
            foreach (var u in unitFollowFilter)
            {
                ref var unitFollowPathComp = ref unitFollowFilter.Get1(u);
                ref var unitComp = ref unitFollowFilter.Get2(u);
                ref var transformComp = ref unitFollowFilter.Get3(u);

                bool followingPath = true;

                Vector2 pos2D = new Vector2(transformComp.transform.position.X, transformComp.transform.position.Z);

                while (unitFollowPathComp.path.turnBoundaries[unitFollowPathComp.pathIndex].HasCrossedLine(pos2D))
                {
                    if (unitFollowPathComp.pathIndex == unitFollowPathComp.path.finishLineIndex)
                    {
                        followingPath = false;
                        var entity = unitFollowFilter.GetEntity(u);
                        entity.AddComponent<UnitReturnPathComp>() = new UnitReturnPathComp() {
                            path = unitFollowPathComp.path,
                            pathPositions = unitFollowPathComp.positionList,
                            index = unitFollowPathComp.positionList.Count-1,
                        };
                        entity.RemoveComponent<UnitFollowPathComp>();
                        //entity.RemoveComponent<StartPathFollowComp>();

                        

                        return;
                    }
                    else
                    {
                        unitFollowPathComp.pathIndex++;
                    }
                }

                if (followingPath)
                {

                    if (unitFollowPathComp.pathIndex >= unitFollowPathComp.path.slowDownIndex && unitComp.stoppingDst > 0)
                    {
                        unitFollowPathComp.speedPercent = MathFunctions.Clamp(unitFollowPathComp.path.turnBoundaries[unitFollowPathComp.path.finishLineIndex].DistanceFromPoint(pos2D) / unitComp.stoppingDst, 0, 1);
                        if (unitFollowPathComp.speedPercent < 0.1f)
                        {
                            var entity = unitFollowFilter.GetEntity(u);
                            entity.AddComponent<UnitReturnPathComp>() = new UnitReturnPathComp() {
                                path = unitFollowPathComp.path,
                                pathPositions = unitFollowPathComp.positionList,
                                index = unitFollowPathComp.positionList.Count-1,
                            };
                            entity.RemoveComponent<UnitFollowPathComp>();
                            //entity.RemoveComponent<StartPathFollowComp>();
                            return;
                        }
                    }

                    var direction = unitFollowPathComp.path.lookPoints[unitFollowPathComp.pathIndex] - transformComp.transform.position;

                    var pos = transformComp.transform.position;
                    pos += direction.normalized() * Time.deltaTime * unitComp.speed * unitFollowPathComp.speedPercent;
                    pos.Y = transformComp.transform.position.Y;
                    unitFollowPathComp.positionList.Add(pos);


                    transformComp.transform.position = pos;
                }

            }
        }

        public override void PostRender()
        {
            foreach(var u in unitFollowFilter)
            {
                ref var unitFollowPathComp = ref unitFollowFilter.Get1(u);
                ref var unitComp = ref unitFollowFilter.Get2(u);
                ref var transformComp = ref unitFollowFilter.Get3(u);

                for(int i = 0; i < unitFollowPathComp.path.lookPoints.Length-1; i++)
                {
                    var p1 = unitFollowPathComp.path.lookPoints[i];
                    p1.Y += 0.3f;

                    var p2 = unitFollowPathComp.path.lookPoints[i+1];
                    p2.Y += 0.3f;

                    ProgramLibrary.MespDebug.DrawLine(p1, p2, new Vector3(0, 0, 1));
                }

            }
        }

    }
}
