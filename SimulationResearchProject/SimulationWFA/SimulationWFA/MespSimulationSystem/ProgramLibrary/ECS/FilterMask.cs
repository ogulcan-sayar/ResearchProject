using System.Collections.Generic;

namespace Dalak.Ecs
{
    public class FilterMask
    {
        readonly BitSet includeMask = new BitSet();
        readonly BitSet excludeMask = new BitSet();
        
        public readonly List<int> includedCompPoolIdxList = new List<int>();
        public readonly List<int> excludedCompPoolIdxList = new List<int>();
        
        public FilterMask Include<T>() where T : struct
        {
            int componentPoolIdx = ComponentPoolIdx<T>.PoolIdx;
#if UNITY_EDITOR
            Debug.Assert(!includeMask.IsSet(componentPoolIdx),"Filter try to includes same component");
#endif
            includedCompPoolIdxList.Add(componentPoolIdx);
            includeMask.SetBit(componentPoolIdx);
            return this;
        }

        public FilterMask Exclude<T>() where T : struct
        {
            int componentPoolIdx = ComponentPoolIdx<T>.PoolIdx;
#if UNITY_EDITOR
            Debug.Assert(!excludeMask.IsSet(componentPoolIdx),"Filter try to exclude same component");
#endif
            excludedCompPoolIdxList.Add(componentPoolIdx);
            excludeMask.SetBit(componentPoolIdx);
            return this;
        }

        public bool CompareCompMask(BitSet compMask)
        {
            return includeMask.IsSubsetOf(compMask) && !excludeMask.AnyMatchingBits(compMask);
        }
    }
}