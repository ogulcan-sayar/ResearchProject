using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationWFA.MespUtils
{
    public static class ListExtensions
    {
        public static void CopyFrom<T>(this List<T> list, List<T> otherList)
        {
            if(list == null)
            {
                list = new List<T>();
            }

            for(int i = 0; i < otherList.Count; i++)
            {
                list.Add(otherList[i]);
            }
        }

    }
}
