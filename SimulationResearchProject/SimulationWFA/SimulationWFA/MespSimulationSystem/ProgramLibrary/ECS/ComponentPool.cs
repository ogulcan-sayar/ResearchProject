using System;

namespace Dalak.Ecs
{
    static class ComponentPoolIdxCounter
    {
        // ReSharper disable StaticMemberInGenericType
        public static int componentCounter = 0;
        // ReSharper restore StaticMemberInGenericType
    }

    static class ComponentPoolIdx<T>
    {
        // ReSharper disable StaticMemberInGenericType
        public static readonly int PoolIdx;
        // ReSharper restore StaticMemberInGenericType

        static ComponentPoolIdx()
        {
            PoolIdx = ComponentPoolIdxCounter.componentCounter++;
        }
    }

    public interface IComponentPool
    {
        bool IsDataIgnored { get; }
        void RecycleComp(int idx);
        object DebugGetComponent(int idx);
        void DebugSetComponent(int idx, object o);
    }

    public interface IComponentCustomReset<T>
    {
        void CustomReset(ref T comp);
    }

    public interface IComponentCustomCreate<T> where T : struct
    {
        void CustomCreate(ref T comp);
    }

    public interface IComponentIgnoreData
    {
        
    }
    
    public class ComponentPool<T> : IComponentPool where T : struct
    {
        readonly DynamicArray<T> components;
        readonly DynamicArray<int> freeComponentIndices;
        public delegate void CustomReset(ref T c);
        public delegate void CustomCreate(ref T c);
        
        readonly CustomReset customReset;
        readonly CustomCreate customCreate;
        T ignoreData;
        public bool IsDataIgnored { get; } = false;

        public ComponentPool()
        {
            IsDataIgnored = typeof(IComponentIgnoreData).IsAssignableFrom(typeof(T));
            if (IsDataIgnored)
            {
                return;
            }
            
            components = new DynamicArray<T>(64);
            freeComponentIndices = new DynamicArray<int>(64);
            if (components[0] is IComponentCustomReset<T>)
            {
                IComponentCustomReset<T> t = (IComponentCustomReset<T>) components[0];
                customReset = t.CustomReset;
            }

            if (components[0] is IComponentCustomCreate<T>)
            {
                IComponentCustomCreate<T> t = (IComponentCustomCreate<T>) components[0];
                customCreate = t.CustomCreate;
            }
        }
        
        public int NewComponent()
        {
            if (IsDataIgnored)
            {
                return 0;
            }
            int idx = -1;
            if (freeComponentIndices.numberOfItems > 0)
            {
                idx = freeComponentIndices[--freeComponentIndices.numberOfItems];
            }
            else
            {
                components.Add();
                idx = components.numberOfItems - 1;
                if (customCreate != null)
                {
                    customCreate(ref components[idx]);
                }
                else
                {
                    components[idx] = default;
                }
            }
            return idx;
        }

        public ref T GetComponent(int i)
        {
            if (IsDataIgnored)
            {
                return ref ignoreData;
            }
            return ref components[i];
        }

        /* Interface */
        public void RecycleComp(int idx)
        {
            if (IsDataIgnored)
            {
                return;
            }
            
            if (customReset != null)
            {
                customReset(ref components[idx]);
            }
            else
            {
                components[idx] = default;
            }
            freeComponentIndices.Add() = idx;
        }

        public object DebugGetComponent(int idx)
        {
            if (IsDataIgnored)
            {
                return ignoreData;
            }
            return components[idx];
        }

        public void DebugSetComponent(int idx, object o)
        {
            components[idx] = (T) o;
        }

    }


}