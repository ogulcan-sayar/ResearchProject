using System;
using Dalak.Ecs;
using MespEvents;
using SimulationSystem.ECS.Entegration;
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

    }

    public struct OnEditorFunction : IEvent
    {
        public EditorFunction editorFunction;
    }

    class EditorEventListenSystem : Dalak.Ecs.System
    {
        public static MespEventManager eventManager = new MespEventManager();

        public override void Update()
        {
            ListenEditorEvents(world);
        }

        private void ListenEditorEvents(World world)
        {

            if (eventManager.ListenEvent<OnEditorCreateSimObjEvent>(out var createData))
            {
                createData.simObject.CreateEntity(world);
                createData.simObject.InjectAllSerializedComponents(world);
            }

           if (eventManager.ListenEvent<OnEditorAddCompSimObjEvent>(out var addCompData))
            {
                addCompData.simObject.AddNewSerializedComponent(world, addCompData.serializedComponent);
            }

            if (eventManager.ListenEvent<OnEditorFunction>(out var functionData))
            {
                functionData.editorFunction();
            }

            if (eventManager.ListenEvent<OnEditorRefresh>(out var changeData))
            {
                var allSimObj = SimObject.FindObjectsOfType<TransformSerialized>();

                foreach(var simObj in allSimObj)
                {
                    simObj.RemoveAllComponents();
                    simObj.InjectAllSerializedComponents(world);
                }
                return;
            }
        }
    }
}
