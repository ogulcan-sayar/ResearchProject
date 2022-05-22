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
    public class SceneSelectObjectSystem : Dalak.Ecs.System
    {

        readonly Filter<CameraComp, TransformComp> cameraFilter = null;
        readonly Filter<OutlineBorderRenderComp> outlineFilter = null;

        Ray cameraRay = new Ray(new Vector3(9.5f, 1.5f, 0), new Vector3(0, -1f, 0));

        public override void Update()
        {
            foreach (var c in cameraFilter)
            {
                ref var cameraComp = ref cameraFilter.Get1(c);
                ref var transfromComp = ref cameraFilter.Get2(c);

                var result = cameraComp.RaycastFromCamera(new Vector2((float)Input.GetMousePosX(), (float)Input.GetMousePosY()), transfromComp.transform);
                if (Input.GetMouseKeyDown(0))
                {
                    cameraRay.origin = transfromComp.transform.position;
                    cameraRay.direction = result;

                    if(Physics.Raycast(cameraRay, 0, out var hitEntity, true))
                    {
                        var currentSelectedEntity = outlineFilter.GetEntity(0);
                        currentSelectedEntity.RemoveComponent<OutlineBorderRenderComp>();

                        hitEntity.AddComponent<OutlineBorderRenderComp>();
                    }

                }
            }
        }

    }
}
