using System;
using Dalak.Ecs;
using MespEvents;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.ECSComponents;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{

    public delegate void EditorFunction();

    public struct OnEditorCreateSimObjEvent : IEvent
    {
        public SimObject simObject;
    }

    public struct OnEditorAddCompSimObjEvent : IEvent
    {
        public SimObject simObject;
        public SerializedComponent serializedComponent;
    }

    public struct OnEditorRefresh : IEvent
    {
        public SimObject refreshedSimObj;
    }

    public struct OnEditorFunction : IEvent
    {
        public EditorFunction editorFunction;
    }

    public struct OnEditorRemoveSimObj : IEvent
    {
        public SimObject removedSimObj;
    }

    public struct OnEditorChoosingSimObj : IEvent
    {
        public SimObject chosedSimObj;
    }


    class EditorEventListenSystem : Dalak.Ecs.System
    {
        public static MespEventManager eventManager = new MespEventManager();
        readonly Filter<MeshRendererComp, TransformComp, OutlineBorderRenderComp> renderFilter = null;

        public override void Update()
        {
            ListenEditorEvents(world);
        }

        private void ListenEditorEvents(World world)
        {
            if(eventManager.ListenEvent<OnEditorChoosingSimObj>(out var choosingData))
            {
                foreach(var r in renderFilter)
                {
                    var entity = renderFilter.GetEntity(r);
                    entity.RemoveComponent<OutlineBorderRenderComp>();
                }

                choosingData.chosedSimObj.entity.AddComponent<OutlineBorderRenderComp>();
            }

            if (eventManager.ListenEvent<OnEditorCreateSimObjEvent>(out var createData))
            {
                createData.simObject.CreateEntity(world);
                createData.simObject.InjectAllSerializedComponents(world);
            }

            if(eventManager.ListenEvent<OnEditorRemoveSimObj>(out var removedData))
            {
                removedData.removedSimObj.entity.Destroy();
                removedData.removedSimObj.RemoveParent();
            }

           if (eventManager.ListenEvent<OnEditorAddCompSimObjEvent>(out var addCompData))
            {
                addCompData.serializedComponent.SetOwner(addCompData.simObject);
                addCompData.simObject.AddNewSerializedComponent(addCompData.serializedComponent);
            }

            if (eventManager.ListenEvent<OnEditorFunction>(out var functionData))
            {
                functionData.editorFunction();
            }

            if (eventManager.ListenEvent<OnEditorRefresh>(out var changeData))
            {
                changeData.refreshedSimObj.RemoveAllComponents();
                changeData.refreshedSimObj.InjectAllSerializedComponents(world);
            }
        }
    }
}
