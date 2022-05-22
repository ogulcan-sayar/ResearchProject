using System;
using Dalak.Ecs;

namespace SimulationSystem.ECS.Entegration
{
    public abstract class SerializedComponent
    {
        public bool add = true;
        public Type type;
        private SimObject ownerSimObj;

        public SerializedComponent()
        {
            type = this.GetType();
        }

        public abstract void AddComponent(Entity entity,World world);
        public abstract string GetName();

        public SimObject GetOwner()
        {
            return ownerSimObj;
        }
        
        public void SetOwner(SimObject ownerSimObj)
        {
            this.ownerSimObj = ownerSimObj;
        }

    }
    
    public abstract class SerializedComponent<T> : SerializedComponent where T : struct
    {
        public T t;

        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<T>() = t;
        }

        
    }

    public abstract class SerializedTag<T> : SerializedComponent where T : struct
    {
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<T>();
        }
    }
    
}