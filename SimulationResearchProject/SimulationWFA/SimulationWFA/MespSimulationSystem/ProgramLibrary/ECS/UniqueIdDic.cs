using System;
using System.Collections;
using System.Collections.Generic;

namespace Dalak.Ecs
{
    public class UniqueIdDic<T>
    {
        T[] values;
        T emptyValue;

        public UniqueIdDic(T emptyValue, int initSize)
        {
            this.emptyValue = emptyValue;
            values = new T[initSize];
        }

        public ref T Set(int key)
        {
            if (key >= values.Length)
            {
                Array.Resize(ref values,key + 1);
            }
            return ref values[key];
        }

        public void Remove(int key)
        {
            values[key] = emptyValue;
        }

        public ref T Get(int key)
        {
            if (key >= values.Length)
            {
                return ref emptyValue;
            }
            return ref values[key];
        }

    }
}
