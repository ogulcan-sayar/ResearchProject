using System;

namespace Dalak.Ecs
{
    public class DynamicArray<T> where T : struct
    {
        T[] items;
        public int numberOfItems;
        public DynamicArray(int initSize)
        {
            items = new T[initSize];
            numberOfItems = 0;
        }
        public ref T Add()
        {
            if (numberOfItems >= items.Length)
            {
                Array.Resize (ref items, items.Length << 1);
            }
            numberOfItems++;
            return ref items[numberOfItems - 1];
        }
        
        public ref T Push()
        {
            if (numberOfItems >= items.Length)
            {
                Array.Resize (ref items, items.Length << 1);
            }
            numberOfItems++;
            return ref items[numberOfItems - 1];
        }

        public void RemoveLast()
        {
            numberOfItems--;
        }
       
        public void Pop()
        {
            numberOfItems--;
        }
        
        // Define the indexer to allow client code to use [] notation.
        public ref T this[int i] => ref items[i];


        public delegate bool Condition(ref T t);

        public void RemoveIfAll(Condition condition)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                if (condition(ref items[i]))
                {
                    items[numberOfItems - 1] = items[i];
                    numberOfItems--;
                    i--;
                }
            }
        }
        
        public void RemoveIfFirst(Condition condition)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                if (condition(ref items[i]))
                {
                    items[numberOfItems - 1] = items[i];
                    numberOfItems--;
                    return;
                }
            }
        }


        
    }
}