using System;

namespace Dalak.Ecs
{
    // TODO if you remove the component than add it again component
    // idx will change filter list cannot detect it
    // put a refresh function?
    // TODO HAVE BUGS DONT USE
    public abstract class FilterList
    {
        readonly int nComponents;
        readonly int[] componentPoolIdxMap;

        Entity[] entities;
        int[] componentIdxList;
        int nEntities = 0;
        public int NumberOfEntities => nEntities;
        
        public FilterList(int initSize, FilterMask filterMask)
        {
            this.nComponents = filterMask.includedCompPoolIdxList.Count;
            
            entities = new Entity[initSize];
            componentIdxList = new int[initSize * nComponents];
            componentPoolIdxMap = new int[nComponents];
            for (int i = 0; i < filterMask.includedCompPoolIdxList.Count; i++)
            {
                componentPoolIdxMap[i] = filterMask.includedCompPoolIdxList[i];
            }
        }

        public void Add(Entity entity)
        {
            if (nEntities >= entities.Length)
            {
                int len = entities.Length * 2;
                Array.Resize(ref entities, len);
                Array.Resize(ref componentIdxList, len * nComponents);
            }

            entities[nEntities] = entity;
            int cStartIdx = nEntities * nComponents;
            for (int i = 0; i < nComponents; i++)
            {
                componentIdxList[cStartIdx + i] = entity.world.GetComponentIdx(entity, componentPoolIdxMap[i]);
            }
            
            nEntities++;
        }
        
        public void Replace(Entity entity, int idx)
        {
            //Debug.Assert(idx >= 0 && idx < NumberOfEntities);
            entities[idx] = entity;
            int cStartIdx = idx * nComponents;
            for (int i = 0; i < nComponents; i++)
            {
                componentIdxList[cStartIdx + i] = entity.world.GetComponentIdx(entity, componentPoolIdxMap[i]);
            }
        }

        public void Insert(Entity entity, int idx)
        {
            //Debug.Assert(idx >= 0 && idx <= NumberOfEntities);

            if (nEntities >= entities.Length)
            {
                int len = entities.Length * 2;
                Array.Resize(ref entities, len);
                Array.Resize(ref componentIdxList, len * nComponents);
            }

            // Shift entities and their components
            for (int i = nEntities; i > idx; i--)
            {
                entities[i] = entities[i - 1];
                int componentStartIdx = i * nComponents;
                int nextComponentStartIdx = (i - 1) * nComponents;
                
                for (int j = 0; j < nComponents; j++)
                {
                    componentIdxList[componentStartIdx + i] = componentIdxList[nextComponentStartIdx + i];
                }
            }
            
            entities[idx] = entity;
            int cStartIdx = idx * nComponents;
            for (int i = 0; i < nComponents; i++)
            {
                componentIdxList[cStartIdx + i] = entity.world.GetComponentIdx(entity, componentPoolIdxMap[i]);
            }

            nEntities++;
        }

        public int GetComponentIdx(int entityIdx, int componentOrder)
        {
            //Debug.Assert(entityIdx >= 0 && entityIdx < NumberOfEntities);
            //Debug.Assert(componentOrder >= 0 && componentOrder < nComponents);
            int cStartIdx = entityIdx * nComponents;
            return componentIdxList[cStartIdx + componentOrder];
        }

        public Entity GetEntity(int idx)
        {
            //Debug.Assert(idx >= 0 && idx < NumberOfEntities);
            return entities[idx];
        }

        public int FindEntity(Entity entity)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                if (entity == entities[i])
                {
                    return i;
                }
            }

            return -1;
        }

        public bool RemoveEntity(Entity entity)
        {
            int idx = FindEntity(entity);
            if (idx == -1)
            {
                return false;
            }
            RemoveAt(idx);
            return true;
        }

        public bool RemoveEntityWithSwap(Entity entity)
        {
            int idx = FindEntity(entity);
            if (idx == -1)
            {
                return false;
            }
            RemoveAtWithSwap(idx);
            return true;
        }

       
        

        public void RemoveAtWithSwap(int idx)
        {
            //Debug.Assert(idx >= 0 && idx < NumberOfEntities);

            int lastEntityIdx = nEntities - 1;
            
            entities[idx] = entities[lastEntityIdx];
            int cStartIdx = idx * nComponents;
            int ocStartIdx = lastEntityIdx * nComponents;
            
            for (int i = 0; i < nComponents; i++)
            {
                componentIdxList[cStartIdx + i] = componentIdxList[ocStartIdx + i];
            }

            nEntities--;
        }

        public void RemoveAt(int idx)
        {
            //Debug.Assert(idx >= 0 && idx < NumberOfEntities);

            for (int i = idx; i < nEntities - 1; i++)
            {
                entities[i] = entities[i + 1];
                int componentStartIdx = i * nComponents;
                int nextComponentStartIdx = (i + 1) * nComponents;
                
                for (int j = 0; j < nComponents; j++)
                {
                    componentIdxList[componentStartIdx + i] = componentIdxList[nextComponentStartIdx + i];
                }
            }

            nEntities--;
        }
        
        
    }
    
    public class FilterList<TInc1> : FilterList 
        where TInc1 : struct
    {
        ComponentPool<TInc1> pool1 = null;

        public FilterList(int initSize) : base(initSize, new FilterMask().Include<TInc1>()){}

        public ref TInc1 Get1(int i)
        {
            //??=
            if (GetEntity(i).world.GetComponentPool<TInc1>() != null) pool1 = GetEntity(i).world.GetComponentPool<TInc1>();
            return ref pool1.GetComponent(GetComponentIdx(i, 0));
        }
    }
    
    public class FilterList<TInc1,TInc2> : FilterList 
        where TInc1 : struct
        where TInc2: struct 
    {
        ComponentPool<TInc1> pool1 = null;
        ComponentPool<TInc2> pool2 = null;

        public FilterList(int initSize) : base(initSize, new FilterMask().Include<TInc1>().Include<TInc2>()){}

        public ref TInc1 Get1(int i)
        {
            //??=
            if(GetEntity(i).world.GetComponentPool<TInc1>() != null) pool1 = GetEntity(i).world.GetComponentPool<TInc1>();
            return ref pool1.GetComponent(GetComponentIdx(i, 0));
        }
        
        public ref TInc2 Get2(int i)
        {
            //??=
            if (GetEntity(i).world.GetComponentPool<TInc2>() != null) pool2 = GetEntity(i).world.GetComponentPool<TInc2>();
            return ref pool2.GetComponent(GetComponentIdx(i, 1));
        }
    }
    
    public class FilterList<TInc1,TInc2,TInc3> : FilterList 
        where TInc1 : struct
        where TInc2: struct 
        where TInc3 : struct
    {
        ComponentPool<TInc1> pool1 = null;
        ComponentPool<TInc2> pool2 = null;
        ComponentPool<TInc3> pool3 = null;

        public FilterList(int initSize) : base(initSize, new FilterMask().Include<TInc1>().Include<TInc2>().Include<TInc3>()){}

        public ref TInc1 Get1(int i)
        {
            if(GetEntity(i).world.GetComponentPool<TInc1>() != null) pool1 = GetEntity(i).world.GetComponentPool<TInc1>();
            return ref pool1.GetComponent(GetComponentIdx(i, 0));
        }
        
        public ref TInc2 Get2(int i)
        {
            if(GetEntity(i).world.GetComponentPool<TInc2>() != null) pool2 = GetEntity(i).world.GetComponentPool<TInc2>();
            return ref pool2.GetComponent(GetComponentIdx(i, 1));
        }
        
        public ref TInc3 Get3(int i)
        {
            if (GetEntity(i).world.GetComponentPool<TInc3>() != null) pool3 = GetEntity(i).world.GetComponentPool<TInc3>();
            return ref pool3.GetComponent(GetComponentIdx(i, 2));
        }
    }
    
}