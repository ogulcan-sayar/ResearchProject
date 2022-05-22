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
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationWFA.SimulationAlgorithms
{
    public class UnitReturnPathSystem : Dalak.Ecs.System
    {
        readonly Filter<UnitReturnPathComp, UnitComp, TransformComp, StartPathFollowComp> unitFollowFilter = null;
        readonly Filter<TargetComp, TransformComp> targetFilter = null;

        public override void Update()
        {
            foreach (var u in unitFollowFilter)
            {
                ref var unitReturnPathComp = ref unitFollowFilter.Get1(u);
                ref var unitComp = ref unitFollowFilter.Get2(u);
                ref var transformComp = ref unitFollowFilter.Get3(u);

                ref var targetComp = ref targetFilter.Get1(0);
                ref var targetTransformComp = ref targetFilter.Get2(0);

                if(unitReturnPathComp.index <= 0)
                {
                    var entity = unitFollowFilter.GetEntity(u);
                    entity.RemoveComponent<UnitReturnPathComp>();
                    entity.RemoveComponent<StartPathFollowComp>();
                    continue;
                }
                var unitPos = unitReturnPathComp.pathPositions[unitReturnPathComp.index];
                transformComp.transform.position = unitPos;

                targetTransformComp.transform.position = transformComp.transform.right + transformComp.transform.position;

                unitReturnPathComp.index--;


            }
        }
        public override void PostRender()
        {
            foreach (var u in unitFollowFilter)
            {
                ref var unitFollowPathComp = ref unitFollowFilter.Get1(u);
                ref var unitComp = ref unitFollowFilter.Get2(u);
                ref var transformComp = ref unitFollowFilter.Get3(u);

                for (int i = 0; i < unitFollowPathComp.path.lookPoints.Length - 1; i++)
                {
                    var p1 = unitFollowPathComp.path.lookPoints[i];
                    p1.Y += 0.3f;

                    var p2 = unitFollowPathComp.path.lookPoints[i + 1];
                    p2.Y += 0.3f;

                    ProgramLibrary.MespDebug.DrawLine(p1, p2, new Vector3(0, 0, 1));
                }

            }
        }
    }
}
