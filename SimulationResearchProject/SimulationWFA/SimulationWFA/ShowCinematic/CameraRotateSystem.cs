using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using MespSimulationSystem.Math;
using MESPSimulationSystem.Math;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.TimeUtils;

namespace SimulationWFA.ShowCinematic
{
    public class CameraRotateSystem : Dalak.Ecs.System
    {
        readonly Filter<CameraComp, TransformComp>.Exclude<RotateCameraComp> cameraFilter = null;
        readonly Filter<CameraComp, TransformComp, RotateCameraComp> rotateCameraFilter = null;

        readonly Filter<ShowMespNameComp,TextRendererComp> nameFilter = null;

        public override void Update()
        {

            foreach (var c in cameraFilter)
            {
                var cameraEntity = cameraFilter.GetEntity(c);
                ref var transformComp = ref cameraFilter.Get2(c);
                if (Input.GetKeyDown(KeyCode.I))
                {

                    cameraEntity.AddComponent<RotateCameraComp>() = new RotateCameraComp() {
                        rotationSpeed = 20,
                        currentRotation = 0,
                        oldPos = transformComp.transform.position,
                        oldRotate = transformComp.transform.rotation,
                    };

                    SleepingModeSystem.sleeping = true;
                    transformComp.transform.position = new Vector3(980, 2, 0);

                    var mespEntity = world.NewEntity();
                    mespEntity.AddComponent<TextRendererComp>() = new TextRendererComp() {

                        text = "MESP",
                        color = new Vector3(1, 1, 1),
                        UIPosition = new Vector2(0, 350),
                        scale = 150,
                    };
                    mespEntity.AddComponent<ShowMespNameComp>();



                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    transformComp.transform.position = new Vector3(980, 2, 0);
                }
            }


            foreach (var r in rotateCameraFilter)
            {
                ref var rotateCameraComp = ref rotateCameraFilter.Get3(r);
                ref var transformComp = ref rotateCameraFilter.Get2(r);

                rotateCameraComp.currentRotation += rotateCameraComp.rotationSpeed * Time.deltaTime;
                if (rotateCameraComp.currentRotation > 360) rotateCameraComp.currentRotation -= 360;

                var pos = MathFunctions.Rotate(new Mat4(1), rotateCameraComp.currentRotation, new System.Numerics.Vector3(0, 1, 0), 10 * new Vector3(0, 0, 1));
                pos += new Vector3(980, 2, 0);

                transformComp.transform.position = pos;
                var rotation = transformComp.transform.rotation;
                rotation.Y = rotateCameraComp.currentRotation;
                rotation.Z = 0;
                rotation.X = 0;
                transformComp.transform.rotation = rotation;
            }

            foreach(var n in nameFilter)
            {
                ref var textRendererComp = ref nameFilter.Get2(n);
                ref var nameComp = ref nameFilter.Get1(n);

                nameComp.time += Time.deltaTime;
                float value = nameComp.time;
                if(nameComp.time > 5)
                {
                    value = 10 - nameComp.time;
                    if (nameComp.time > 10)
                    {
                        value -= 10;
                    }
                   
                }

                textRendererComp.color =  Vector3.Lerp(new Vector3(1, 0, 0), new Vector3(0, 0, 1), value / 5);
            }

        }

    }
}
