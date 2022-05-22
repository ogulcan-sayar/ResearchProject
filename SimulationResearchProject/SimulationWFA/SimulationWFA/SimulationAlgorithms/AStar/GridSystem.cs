using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using MESPSimulationSystem.Math;
using PhysicLibrary;
using ProgramLibrary;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationWFA.SimulationAlgorithms.AStar
{
    public class GridSystem : Dalak.Ecs.System
    {
        readonly Filter<GridComp, TransformComp> gridFilter = null;
        readonly Filter<UnitComp, ColliderComp> unitFollowFilter = null;

        public override void Awake()
        {
            var gridEntity = world.NewEntity();
            Grid grid = new Grid();
            grid.gridWorldSize = new Vector2(20, 20);
            grid.nodeRadius = 0.5f;
            gridEntity.AddComponent<GridComp>() = new GridComp() { grid = grid };
            var transform = new Transform(new Vector3(0,0,0), Vector3.One, Vector3.Zero);
            gridEntity.AddComponent<TransformComp>() = new TransformComp() { transform = transform };

            ref var transformComp = ref gridEntity.GetComponent<TransformComp>();
            BoxBounds unitBoxBound = unitFollowFilter.Get2(0).collider.bound as BoxBounds;
            grid.SetupGrid(transformComp.transform.position, unitBoxBound);

        }

        //public override void PostRender()
        //{
        //    foreach (var g in gridFilter)
        //    {
        //        ref var gridComp = ref gridFilter.Get1(g);
        //        ref var transformComp = ref gridFilter.Get2(g);
        //        var color = new Vector3(0, 1, 0);

        //        BoxBounds gridBound = new BoxBounds();
        //        gridBound.Center = transformComp.transform.position;
        //        gridBound.Size = new Vector3(gridComp.grid.gridWorldSize.X, 0, gridComp.grid.gridWorldSize.Y);
        //        MespDebug.DrawWireBox(gridBound, color);

        //        if (gridComp.grid != null)
        //        {
        //            foreach (Node n in gridComp.grid.grid)
        //            {
        //                color = Vector3.Lerp(new Vector3(1,1,1),Vector3.Zero, MathFunctions.InverseLerp(gridComp.grid.penaltyMin, gridComp.grid.penaltyMax, n.movementPenalty));
        //                color = (n.walkable) ? color : new Vector3(1,0,0);

        //                var gridPosition = n.worldPosition;
        //                gridPosition.Y += 0.2f;

        //                MespDebug.DrawWireBoxXZ(gridPosition, new Vector3(1 * gridComp.grid.nodeDiameter, 0 , 1 * gridComp.grid.nodeDiameter), color);
        //            }
        //        }

        //    }
        //}
    }
}
