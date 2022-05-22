using System;
using System.Collections.Generic;

namespace Dalak.Ecs
{
    public class World
    {
        const int ComponentBufferSize = 64;
        const int EntityBufferSize = 128;
        const int FreeEntityBufferSize = 64;
        const int ComponentPoolSize = 64;
        
        public readonly DynamicArray<EntityData> entitiesData;
        readonly DynamicArray<int> freeEntities;
        public readonly Dictionary<int, IComponentPool> componentPools;
        public readonly List<Filter> filters = new List<Filter>();
        readonly FilterMap filterMap = new FilterMap();
        public event Action OnDestroy;

#if UNITY_EDITOR
        public event Action<Entity> OnEntityCreate;
        public event Action<Entity> OnEntityDestroyed;
        public event Action<Entity> OnEntityComponentsChanged;
#endif
        public World()
        {
            entitiesData = new DynamicArray<EntityData>(EntityBufferSize);
            freeEntities = new DynamicArray<int>(FreeEntityBufferSize);
            componentPools = new Dictionary<int, IComponentPool>(ComponentPoolSize);
        }

        public ComponentPool<T> GetComponentPool<T>() where T : struct
        {
            int poolIdx = ComponentPoolIdx<T>.PoolIdx;
            if (componentPools.TryGetValue(poolIdx, out var pool))
            {
                return (ComponentPool<T>)pool;
            }

            var r = new ComponentPool<T>();
            componentPools.Add(poolIdx,r);
            return r;
        }
        public Entity NewEntity()
        {
            if (freeEntities.numberOfItems > 0)
            {
                int freeIdx = freeEntities[--freeEntities.numberOfItems];
                var e = new Entity(
                    entitiesData[freeIdx].idx, 
                    entitiesData[freeIdx].generationId, 
                    this);
#if UNITY_EDITOR
                OnEntityCreate?.Invoke(e);
#endif
                return e;
            }
            else
            {
                // Create New Entity

                EntityData data = new EntityData
                {
                    idx = entitiesData.numberOfItems,
                    generationId = 0,
                    components = new DynamicArray<ComponentIdx>(ComponentBufferSize),
                    filterIdxList = new DynamicArray<int>(ComponentBufferSize),
                    componentMask = new BitSet()
                };

                entitiesData.Add() = data;

                var e = new Entity(data.idx, data.generationId, this);
            
#if UNITY_EDITOR
                OnEntityCreate?.Invoke(e);
#endif
                return e;
            }
           
        }

        public void DestroyEntity(Entity entity)
        {
            if (!IsEntityAlive(entity))
            {
                return;
            }

            freeEntities.Add() = entity.idx;
            
            ref var data = ref entitiesData[entity.idx];
            

            // Remove from all filters
            for (int i = 0; i < data.filterIdxList.numberOfItems ; i++)
            {
                int filterIdx = data.filterIdxList[i];
                filters[filterIdx].OnRemoveEntity(entity);
            }
            
            // Remove Component Data
            for (int i = 0; i < data.components.numberOfItems; i++)
            {
                int poolIdx = data.components[i].poolIdx;
                componentPools[poolIdx].RecycleComp(data.components[i].idx);
            }
            
            data.generationId++;
            data.components.numberOfItems = 0;
            data.filterIdxList.numberOfItems = 0;
            data.componentMask.ClearAll();
            
#if UNITY_EDITOR
            OnEntityDestroyed?.Invoke(entity);
            OnEntityComponentsChanged?.Invoke(entity);
#endif
        }

        public void RemoveAllComponents(Entity entity)
        {
            //Debug.Assert(IsEntityAlive(entity));
            
            ref var data = ref entitiesData[entity.idx];
            
            // Remove from all filters
            for (int i = 0; i < data.filterIdxList.numberOfItems ; i++)
            {
                int filterIdx = data.filterIdxList[i];
                filters[filterIdx].OnRemoveEntity(entity);
            }
            
            // Remove Component Data
            for (int i = 0; i < data.components.numberOfItems; i++)
            {
                int poolIdx = data.components[i].poolIdx;
                componentPools[poolIdx].RecycleComp(data.components[i].idx);
            }
            
            data.components.numberOfItems = 0;
            data.filterIdxList.numberOfItems = 0;
            data.componentMask.ClearAll();

/*#if UNITY_EDITOR
            OnEntityComponentsChanged?.Invoke(entity);
#endif*/
        }
        
        
        public bool IsEntityAlive(Entity entity)
        {
            return entitiesData[entity.idx].generationId == entity.generationId;
        }

        public ref T AddComponent<T>(Entity entity) where T : struct
        {
            //Debug.Assert(IsEntityAlive(entity),"Cant add component to destroyed entity");
            
            int componentPoolIdx = ComponentPoolIdx<T>.PoolIdx;
            ref var entityData = ref this.entitiesData[entity.idx];

            if (!componentPools.TryGetValue(componentPoolIdx, out var pool))
            {
                pool = new ComponentPool<T>();
                componentPools.Add(componentPoolIdx,pool);
            }
            
            ComponentPool<T> componentPool = (ComponentPool<T>) pool;
            
            if (entity.HasComponent<T>())
            {
                int cIdx = GetComponentIdx(entity, componentPoolIdx);
                return ref componentPool.GetComponent(cIdx);
            }
            // Add component for the first time
            entityData.componentMask.SetBit(componentPoolIdx);

            int componentIdx = componentPool.NewComponent();

            entityData.components.Add() = new ComponentIdx
            {
                poolIdx = componentPoolIdx,
                idx = componentIdx
            };
            
            // Check removed filters
            for (int i = entityData.filterIdxList.numberOfItems - 1; i >= 0 ; i--)
            {
                int filterIdx = entityData.filterIdxList[i];
                
                if (!filters[filterIdx].IsEntityCompatible(entityData.componentMask))
                {
                    filters[filterIdx].OnRemoveEntity(entity);
                    var items = entityData.filterIdxList;
                    items[i] = items[entityData.filterIdxList.numberOfItems - 1];
                    entityData.filterIdxList.numberOfItems--;
                }
            }
            
            // Check added filters
            foreach (var filter in filterMap.GetFiltersIncluded(componentPoolIdx))
            {
                bool filterContainsEntity = false;

                for (int f = 0; f < entityData.filterIdxList.numberOfItems; f++)
                {
                    if (entityData.filterIdxList[f] == filter.filterIdx)
                    {
                        filterContainsEntity = true;
                        break;
                    }
                }

                if (!filterContainsEntity && filter.IsEntityCompatible(entityData.componentMask))
                {
                    entityData.filterIdxList.Add() = filter.filterIdx;
                    filter.OnAddEntity(entity);
                }
            }
            
/*#if UNITY_EDITOR
            OnEntityComponentsChanged?.Invoke(entity);
#endif*/
            return ref componentPool.GetComponent(componentIdx);
        }
        
        public void RemoveComponent<T>(Entity entity) where T : struct
        {
            if (!HasComponent<T>(entity))
            {
                return;
            }
            RemoveComponent(entity,ComponentPoolIdx<T>.PoolIdx);
        }

        public void RemoveComponent(Entity entity, int removedComponentPoolIdx)
        {
            ref var entityData = ref this.entitiesData[entity.idx];
            int entityComponentIdxIdx = -1; // not typo, componentidx's idx inside entities components array
            for (int i = 0; i < entityData.components.numberOfItems; i++)
            {
                if (entityData.components[i].poolIdx == removedComponentPoolIdx)
                {
                    entityComponentIdxIdx = i;
                    break;
                }
            }
            
            entityData.componentMask.ClearBit(removedComponentPoolIdx);
            
            // Remove component data
            componentPools[removedComponentPoolIdx].RecycleComp(entityData.components[entityComponentIdxIdx].idx);
            entityData.components[entityComponentIdxIdx] =
                entityData.components[entityData.components.numberOfItems - 1];
            entityData.components.numberOfItems--;
                    
/*#if UNITY_EDITOR
            OnEntityComponentsChanged?.Invoke(entity);
#endif*/
            
            // Check removed filters
            for (int i = entityData.filterIdxList.numberOfItems - 1; i >= 0 ; i--)
            {
                int filterIdx = entityData.filterIdxList[i];
                
                if (!filters[filterIdx].IsEntityCompatible(entityData.componentMask))
                {
                    filters[filterIdx].OnRemoveEntity(entity);
                    var items = entityData.filterIdxList;
                    items[i] = items[entityData.filterIdxList.numberOfItems - 1];
                    entityData.filterIdxList.numberOfItems--;
                }
            }

            foreach (var filter in filterMap.GetFiltersExcluded(removedComponentPoolIdx))
            {
                bool filterContainsEntity = false;

                for (int f = 0; f < entityData.filterIdxList.numberOfItems; f++)
                {
                    if (entityData.filterIdxList[f] == filter.filterIdx)
                    {
                        filterContainsEntity = true;
                        break;
                    }
                }

                if (!filterContainsEntity && filter.IsEntityCompatible(entityData.componentMask))
                {
                    entityData.filterIdxList.Add() = filter.filterIdx;
                    filter.OnAddEntity(entity);
                }
            }
        }


        public ref T GetComponent<T>(Entity entity) where T : struct
        {
            //Debug.Assert(IsEntityAlive(entity),"Cant get component on destroyed entity");
            //Debug.Assert(HasComponent<T>(entity),"Entity does not have requested component!");
            
            ref var entityData = ref this.entitiesData[entity.idx];
            int componentPoolIdx = ComponentPoolIdx<T>.PoolIdx;

            int compIdx = 0;
            for (int i = 0; i < entityData.components.numberOfItems; i++)
            {
                if (entityData.components[i].poolIdx == componentPoolIdx)
                {
                    compIdx = entityData.components[i].idx;
                    break;
                }
            }

            ComponentPool<T> componentPool = (ComponentPool<T>) componentPools[componentPoolIdx];
            return ref componentPool.GetComponent(compIdx);
        }

        public bool IsDataIgnored(int componentPoolIdx)
        {
            return componentPools[componentPoolIdx].IsDataIgnored;
        }
        public int GetComponentIdx(Entity entity, int componentPoolIdx)
        {
            ref var entityData = ref this.entitiesData[entity.idx];
            for (int i = 0; i < entityData.components.numberOfItems; i++)
            {
                if (entityData.components[i].poolIdx == componentPoolIdx)
                {
                    return entityData.components[i].idx;
                }
            }
            //Debug.Assert(false,"Invalid GetComponentIdx Call");
            return -1;
        }
        
        public bool HasComponent<T>(Entity entity) where T : struct
        {
            int componentPoolIdx = ComponentPoolIdx<T>.PoolIdx;
            ref var entityData = ref this.entitiesData[entity.idx];
            return entityData.componentMask.IsSet(componentPoolIdx);
        }

        public Filter GetFilter(Type filterType)
        {
            Filter f = filterMap.GetFilterByType(filterType);
            if (f != null)
            {
                return f;
            }
            
            var filter = (Filter) Activator.CreateInstance(filterType,new object[]{this,filters.Count});
            filters.Add(filter);
            filterMap.AddFilter(filter);
            return filter;
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
            for (int i = 0; i < entitiesData.numberOfItems; i++)
            {
                if (entitiesData[i].components != null && entitiesData[i].components.numberOfItems > 0)
                {
                    new Entity(entitiesData[i].idx,entitiesData[i].generationId , this).Destroy();
                }
            }
        }
    }

}