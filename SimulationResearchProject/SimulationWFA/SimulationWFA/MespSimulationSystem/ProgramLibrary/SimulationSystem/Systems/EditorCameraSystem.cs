using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.IO;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.Timer;

namespace SimulationSystem.Systems
{
    class EditorCameraSystem : Dalak.Ecs.System
    {
        readonly Filter<CameraComp,TransformComp> cameraFilter = null;


        public override void Update()
        {
            foreach(var c in cameraFilter)
            {
                ref var transformComp = ref cameraFilter.Get2(c);
                ref var cameraComp = ref cameraFilter.Get1(c);

                float velocity = cameraComp.speed * Time.deltaTime;
                CameraMoveInput(transformComp.transform, velocity);

                if (Input.GetMouseKey(1))
                {
                    double dx = Input.GetMouseDx();
                    double dy = Input.GetMouseDy();
                    if (dx != 0 || dy != 0)
                    {
                        UpdateCameraDirection(transformComp.transform, dx, dy);
                    }
                }
                

                double scrollDy = Input.GetMouseScrolDy();
                if (scrollDy != 0)
                {
                    cameraComp.UpdateCameraZoom(scrollDy);
                }
            }
        }

        public override void LateUpdate()
        {
            foreach(var c in cameraFilter)
            {
                ref var cameraComp = ref cameraFilter.Get1(c);
                ref var transformComp = ref cameraFilter.Get2(c);

                cameraComp.view = cameraComp.GetViewMatrix(transformComp.transform);
                cameraComp.projection = cameraComp.Perspective(800f / 600f);
            }
        }


        public void CameraMoveInput(Transform transform, float velocity)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += Transform.WorldUp * velocity;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position -= Transform.WorldUp * velocity;
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * velocity;
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * velocity;
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * velocity;
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * velocity;
            }
        }

        public void UpdateCameraDirection(Transform transform,double dx, double dy)
        {
            Vector3 rotation = transform.rotation;
            rotation.Y += (float)dx;
            rotation.X += (float)dy;

            if (rotation.X > 89.0f)
            {
                rotation.X = 89.0f;
            }
            else if (rotation.X < -89.0f)
            {
                rotation.X = -89.0f;
            }
            transform.rotation = rotation;
        }
    }
}
