using System;
using System.Collections.Generic;
using Dalak.Ecs;
using SimulationSystem.ECS.Entegration;

namespace SimulationSystem
{

    [Serializable]
    public struct SimObjectData
    {
        public string name;
        public List<SerializedComponent> serializedComponentList;
    }
    
    
    public class SimObject
    {
        public SimObjectData objectData;
        
        public static SimObject Hiearchy = new SimObject();
        public SimObject parent;
        public List<SimObject> child;


        public static SimObject NewSimObject()
        {
            SimObject newSimObject = new SimObject();
            newSimObject.objectData = new SimObjectData();
            newSimObject.child = new List<SimObject>();

            return newSimObject;
        }

        public static SimObject[] GetChildren(SimObject parentObject)
        {
            return parentObject.child.ToArray();
        }
        
        
        
    }
    


    
}