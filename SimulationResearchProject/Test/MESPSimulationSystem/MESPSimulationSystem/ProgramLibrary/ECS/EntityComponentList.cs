using System;

namespace Dalak.Ecs
{
    public class EntityComponentList
    {
        readonly int nComponents;
        readonly int[] componentPoolIdxMap;

        readonly UniqueIdDic<int> entityOrderMap = new UniqueIdDic<int>(-1,64);
        public Entity[] entities;
        int[] componentIdxList;
        public int nEntities = 0;
        
        public EntityComponentList(int initSize, FilterMask filterMask)
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

        public void AddEntity(Entity entity)
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
            
            entityOrderMap.Set(entity.idx) = nEntities;
            nEntities++;
        }

        public int GetComponentIdx(int entityOrder, int componentOrder)
        {
            int cStartIdx = entityOrder * nComponents;
            return componentIdxList[cStartIdx + componentOrder];
        }

        public void RemoveEntity(Entity removedEntity)
        {
            int removedEntityIdx = entityOrderMap.Get(removedEntity.idx);
            int lastEntityIdx = nEntities - 1;
            
            entities[removedEntityIdx] = entities[lastEntityIdx];
            int cStartIdx = removedEntityIdx * nComponents;
            int ocStartIdx = lastEntityIdx * nComponents;
            
            for (int i = 0; i < nComponents; i++)
            {
                componentIdxList[cStartIdx + i] = componentIdxList[ocStartIdx + i];
            }

            nEntities--;
            entityOrderMap.Set(entities[removedEntityIdx].idx) = removedEntityIdx;
            entityOrderMap.Remove(removedEntity.idx);
        }
        

    }
}