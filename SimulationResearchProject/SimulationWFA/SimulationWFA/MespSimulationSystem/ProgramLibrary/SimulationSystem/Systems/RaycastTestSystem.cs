using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem.Systems
{
    public class RaycastTestSystem : Dalak.Ecs.System
    {

        readonly Filter<CameraComp,TransformComp> cameraFilter = null;

        Ray ray = new Ray(new Vector3(9.5f, 1.5f, 0), new Vector3(0, -1f, 0));
        
        public override void Update()
        {
            

            //Console.WriteLine("x: " + Input.GetMousePosX() + "y: " + Input.GetMousePosY());

            ProgramLibrary.MespDebug.DrawRay(ray, 1.45f, new Vector3(1, 0, 0));
            if ( Physics.Raycast(ray, 1.45f, out var hit))
            {

              //  Console.WriteLine(hit.GetComponent<DebugNameComp>().name);

            }
        }

    }
}
