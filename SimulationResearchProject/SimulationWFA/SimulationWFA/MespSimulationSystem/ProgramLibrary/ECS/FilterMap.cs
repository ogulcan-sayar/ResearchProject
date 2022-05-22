using System;
using System.Collections.Generic;

namespace Dalak.Ecs
{
    public class FilterMap
    {
        readonly UniqueIdDic<List<Filter>> filterIncludeMap = new UniqueIdDic<List<Filter>>(null,128);
        readonly UniqueIdDic<List<Filter>> filterExcludedMap = new UniqueIdDic<List<Filter>>(null,128);
        
        readonly List<Filter> emptyList = new List<Filter>(0);
        readonly Dictionary<Type,Filter> filterMap = new Dictionary<Type, Filter>();
        
        
        public void AddFilter(Filter filter)
        {
            foreach (var componentIdx in filter.filterMask.includedCompPoolIdxList)
            {
                List<Filter> list = filterIncludeMap.Get(componentIdx);
                if (list == null)
                {
                    list = new List<Filter>();
                }
                list.Add(filter);
                filterIncludeMap.Set(componentIdx) = list;
            }

            foreach (var componentIdx in filter.filterMask.excludedCompPoolIdxList)
            {
                List<Filter> list = filterExcludedMap.Get(componentIdx);
                if (list == null)
                {
                    list = new List<Filter>();
                }
                list.Add(filter);
                filterExcludedMap.Set(componentIdx) = list;
            }

            filterMap.Add(filter.GetType(), filter);
        }

        public List<Filter> GetFiltersIncluded(int componentIdx)
        {
            List<Filter> list = filterIncludeMap.Get(componentIdx);
            if (list != null)
            {
                return list;
            }
            return emptyList;
        }

        public List<Filter> GetFiltersExcluded(int componentIdx)
        {
            List<Filter> list = filterExcludedMap.Get(componentIdx);
            if (list != null)
            {
                return list;
            }
            return emptyList;
        }

        public Filter GetFilterByType(Type type)
        {
            if (filterMap.TryGetValue(type, out var filter))
            {
                return filter;
            }

            return null;
        }
        
    }
}