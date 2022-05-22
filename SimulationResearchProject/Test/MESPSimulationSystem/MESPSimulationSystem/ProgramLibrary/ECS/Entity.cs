namespace Dalak.Ecs
{
    public readonly struct Entity
    {
        public static readonly Entity Null = new Entity(-1,-1,null);
        public readonly int idx;
        public readonly int generationId;
        public readonly World world;

        public Entity(int idx, int generationId, World world)
        {
            this.idx = idx;
            this.generationId = generationId;
            this.world = world;
        }
        
        public void Destroy()
        {
            world.DestroyEntity(this);
        }

        /// Returns false if entity is null or destroyed
        public bool IsAlive()
        {
            return world != null && world.IsEntityAlive(this);
        }

        /// returns true if entity is null
        public bool IsNull()
        {
            return world == null;
        }

        public ref T AddComponent<T>() where T : struct
        {
            return ref world.AddComponent<T>(this);
        }

        public void RemoveAllComponents()
        {
            world.RemoveAllComponents(this);
        }

        public bool HasComponent<T>() where T : struct
        {
            return world.HasComponent<T>(this);
        }

        public ref T GetComponent<T>() where T : struct
        {
            return ref world.GetComponent<T>(this);
        }

        public void RemoveComponent<T>() where T : struct
        {
            world.RemoveComponent<T>(this);
        }

        public override string ToString()
        {
#if UNITY_EDITOR
            if (!IsNull() && HasComponent<DebugNameComp>())
            {
                return GetComponent<DebugNameComp>().name +  " " + idx + ", " + generationId;
            }            
#endif
            return "Entity " + idx + ", " + generationId;
        }
        
        
        public bool Equals(Entity other)
        {
            return idx == other.idx && generationId == other.generationId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Entity entity && Equals(entity);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (idx * 397) ^ generationId;
            }
        }

        public static bool operator ==(Entity e1, Entity e2)
        {
            return e1.idx == e2.idx && e1.generationId == e2.generationId;
        }
        
        public static bool operator !=(Entity e1, Entity e2)
        {
            return e1.idx != e2.idx || e1.generationId != e2.generationId;
        }

        public void SetName(string name)
        {
            //AddComponent<DebugNameComp>().name = name;
        }
    }
    
   

}