using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.TimeUtils;

namespace SimulationWFA.ShowCinematic
{
    public class SleepingModeSystem : Dalak.Ecs.System
    {
        readonly Filter<CameraComp, TransformComp>.Exclude<RotateCameraComp> cameraFilter = null;
        readonly Filter<CameraComp, TransformComp, RotateCameraComp> rotateCameraFilter = null;

        readonly Filter<ShowMespNameComp> nameFilter = null;

        public Timer timer = new Timer(10f);
        float mousePosX, mousePosY;
        public static bool sleeping = false;
        public override void Awake()
        {
            timer.Update(Time.deltaTime);
        }


        public override void Update()
        {
            var tempMousePosX = (float)Input.GetMousePosX();
            var tempMousePosY = (float)Input.GetMousePosY();

            if (Math.Abs(mousePosX - tempMousePosX) < 1 && (Math.Abs(mousePosY - tempMousePosY) < 1))
            {
                if (timer.Update(Time.deltaTime) && !sleeping)
                {
                    var cameraEntity = cameraFilter.GetEntity(0);
                    ref var transformComp = ref cameraFilter.Get2(0);

                    cameraEntity.AddComponent<RotateCameraComp>() = new RotateCameraComp() {
                        rotationSpeed = 20,
                        currentRotation = 0,
                        oldPos = transformComp.transform.position,
                        oldRotate = transformComp.transform.rotation,
                    };

                    sleeping = true;
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
            }
            else
            {
                timer.Restart();
                mousePosX = tempMousePosX;
                mousePosY = tempMousePosY;

                if (rotateCameraFilter.IsEmpty()) return;
                
                foreach(var r in rotateCameraFilter)
                {
                    ref var transformComp = ref rotateCameraFilter.Get2(r);
                    ref var rotateComp = ref rotateCameraFilter.Get3(r);
                    var entity = rotateCameraFilter.GetEntity(r);

                    transformComp.transform.position = rotateComp.oldPos;
                    transformComp.transform.rotation = rotateComp.oldRotate;

                    entity.RemoveComponent<RotateCameraComp>();
                    sleeping = false;
                }

                foreach(var m in nameFilter)
                {
                    var mespEntity = nameFilter.GetEntity(m);
                    mespEntity.Destroy();
                }

            }

        }
    }
}

