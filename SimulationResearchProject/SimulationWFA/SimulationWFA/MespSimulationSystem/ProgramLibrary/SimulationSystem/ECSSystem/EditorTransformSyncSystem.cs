using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationSystem.Components;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{
    class EditorTransformSyncSystem : Dalak.Ecs.System
    {
        public SimObject[] simObjectArr;

        public override void Awake()
        {
            
        }

        public override void Update()
        {
            simObjectArr = SimObject.FindObjectsOfType<TransformSerialized>();
            for (int i = 0; i < simObjectArr.Length; i++)
            {
                ref var transformComp = ref simObjectArr[i].entity.GetComponent<TransformComp>();

                simObjectArr[i].GetSerializedComponent<TransformSerialized>().pos = transformComp.transform.position;
                simObjectArr[i].GetSerializedComponent<TransformSerialized>().rotation = transformComp.transform.rotation;
                simObjectArr[i].GetSerializedComponent<TransformSerialized>().scale = transformComp.transform.scale;

            }


        }

    }
}
