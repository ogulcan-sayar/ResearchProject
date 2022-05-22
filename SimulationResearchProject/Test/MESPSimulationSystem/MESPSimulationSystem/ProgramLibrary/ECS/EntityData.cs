namespace Dalak.Ecs
{
    public struct EntityData
    {
        public int idx;
        public int generationId;
        public DynamicArray<ComponentIdx> components;
        public DynamicArray<int> filterIdxList;
        public BitSet componentMask;
        
        public override string ToString()
        {
            return "Entity " + idx + ", " + generationId;
        }
    }
}