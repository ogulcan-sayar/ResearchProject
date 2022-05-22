using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Dalak.Ecs
{
    public abstract class Filter
    {
#if UNITY_EDITOR
        public string name;
#endif
        struct Operation
        {
            public Entity entity;
            public bool addOperation;
        }
        
        public int NumberOfEntities => entityComponentList.nEntities;

        public bool IsEmpty()
        {
            return entityComponentList.nEntities == 0;
        }
        int iteratorLock = 0;

        // readonly SortedList<int,EntityContainer> entities = new SortedList<int, EntityContainer>();
        readonly EntityComponentList entityComponentList;
        public readonly  FilterMask filterMask;
        public readonly int filterIdx;
        readonly List<Operation> delayedOperations = new List<Operation>(10);
        
        public Filter(World world,int filterIdx, FilterMask filterMask)
        {
            this.filterMask = filterMask;
            this.filterIdx = filterIdx;
            entityComponentList = new EntityComponentList(32,filterMask);
        }
        
        public bool IsEntityCompatible(BitSet componentMask)
        {
            return filterMask.CompareCompMask(componentMask);
        }
        
        public void OnAddEntity(Entity entity)
        {
            if (iteratorLock > 0)
            {
                delayedOperations.Add(new Operation
                {
                    addOperation = true,
                    entity = entity
                });
                return;
            }

            entityComponentList.AddEntity(entity);
        }

        public void OnRemoveEntity(Entity entity)
        {
            if (iteratorLock > 0)
            {
                delayedOperations.Add(new Operation
                {
                    addOperation = false,
                    entity = entity
                });
                return;
            }
            entityComponentList.RemoveEntity(entity);
        }

        public Entity GetEntity(int i)
        {
            return entityComponentList.entities[i];
        }

        protected int GetComponentIdx(int i,int componentOrder)
        {
            return entityComponentList.GetComponentIdx(i,componentOrder);
        }
        
        void Lock()
        {
            iteratorLock++;
        }

        void Unlock()
        {
            iteratorLock--;
            if (iteratorLock <= 0)
            {
                foreach (var operation in delayedOperations)
                {
                    if (operation.addOperation)
                    {
                        OnAddEntity(operation.entity);
                    }
                    else
                    {
                        OnRemoveEntity(operation.entity);
                    }
                }
                delayedOperations.Clear();
            }
        }

        public struct Enumerator : IDisposable, IEnumerator<int>
        {
            readonly Filter filter;
            readonly int numberOfEntities;
            int idx;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            internal Enumerator (Filter filter) 
            {
                this.filter = filter;
                numberOfEntities = filter.NumberOfEntities;
                idx = -1;
                filter.Lock();
            }

            public void Reset()
            {
                idx = 0;
            }

            object IEnumerator.Current => Current;

            public int Current => idx;

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public void Dispose ()
            {
                filter.Unlock();
            }

            [MethodImpl (MethodImplOptions.AggressiveInlining)]
            public bool MoveNext () 
            {
                return ++idx < numberOfEntities;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
    }
    
   
    public class Filter<TInc1> : Filter
        where TInc1 : struct
    {
        readonly ComponentPool<TInc1> pool1;

        public Filter(World world, int filterIdx) : base(world,filterIdx, new FilterMask()
            .Include<TInc1>())
        {
            pool1 = world.GetComponentPool<TInc1>();
        }

        public ref TInc1 Get1(int i)
        {
            return ref pool1.GetComponent(GetComponentIdx(i, 0));
        }
        
        public class Exclude<Exc1> : Filter<TInc1>
            where Exc1 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>();
            }
        }

        public class Exclude<Exc1, Exc2> : Filter<TInc1>
            where Exc1 : struct
            where Exc2 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>().Exclude<Exc2>();
            }
        }
    }
    
    public class Filter<TInc1,TInc2> : Filter
        where TInc1 : struct
        where TInc2 : struct
    {
        readonly ComponentPool<TInc1> pool1;
        readonly ComponentPool<TInc2> pool2;

        public Filter(World world, int filterIdx) : base(world, filterIdx,new FilterMask()
            .Include<TInc1>()
            .Include<TInc2>())
        {
            pool1 = world.GetComponentPool<TInc1>();
            pool2 = world.GetComponentPool<TInc2>();
        }

        public ref TInc1 Get1(int i)
        {
            return ref pool1.GetComponent(GetComponentIdx(i, 0));
        }
        
        public ref TInc2 Get2(int i)
        {
            return ref pool2.GetComponent(GetComponentIdx(i, 1));
        }

        public class Exclude<Exc1> : Filter<TInc1,TInc2>
            where Exc1 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>();
            }
        }

        public class Exclude<Exc1, Exc2> : Filter<TInc1, TInc2>
            where Exc1 : struct
            where Exc2 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>().Exclude<Exc2>();
            }
        }
    }

    
    public class Filter<TInc1,TInc2,TInc3> : Filter
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct
    {
        readonly ComponentPool<TInc1> pool1;
        readonly ComponentPool<TInc2> pool2;
        readonly ComponentPool<TInc3> pool3;

        public Filter(World world, int filterIdx) : base(world, filterIdx,new FilterMask()
            .Include<TInc1>()
            .Include<TInc2>()
            .Include<TInc3>())
        {
            pool1 = world.GetComponentPool<TInc1>();
            pool2 = world.GetComponentPool<TInc2>();
            pool3 = world.GetComponentPool<TInc3>();
        }

        public ref TInc1 Get1(int i)
        {
            return ref pool1.GetComponent(GetComponentIdx(i, 0));
        }
        
        public ref TInc2 Get2(int i)
        {
            return ref pool2.GetComponent(GetComponentIdx(i, 1));
        }
        
        public ref TInc3 Get3(int i)
        {
            return ref pool3.GetComponent(GetComponentIdx(i, 2));
        }

        public class Exclude<Exc1> : Filter<TInc1,TInc2,TInc3>
            where Exc1 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>();
            }
        }

        public class Exclude<Exc1, Exc2> : Filter<TInc1, TInc2, TInc3>
            where Exc1 : struct
            where Exc2 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>().Exclude<Exc2>();
            }
        }
    }
    
    public class Filter<TInc1,TInc2,TInc3,TInc4> : Filter
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct
        where TInc4 : struct
    {
        readonly ComponentPool<TInc1> pool1;
        readonly ComponentPool<TInc2> pool2;
        readonly ComponentPool<TInc3> pool3;
        readonly ComponentPool<TInc4> pool4;

        public Filter(World world, int filterIdx) : base(world, filterIdx,new FilterMask()
                .Include<TInc1>()
                .Include<TInc2>()
                .Include<TInc3>()
                .Include<TInc4>())
        {
            pool1 = world.GetComponentPool<TInc1>();
            pool2 = world.GetComponentPool<TInc2>();
            pool3 = world.GetComponentPool<TInc3>();
            pool4 = world.GetComponentPool<TInc4>();
        }

        public ref TInc1 Get1(int i)
        {
            return ref pool1.GetComponent(GetComponentIdx(i, 0));
        }
        
        public ref TInc2 Get2(int i)
        {
            return ref pool2.GetComponent(GetComponentIdx(i, 1));
        }
        
        public ref TInc3 Get3(int i)
        {
            return ref pool3.GetComponent(GetComponentIdx(i, 2));
        }
        
        public ref TInc4 Get4(int i)
        {
            return ref pool4.GetComponent(GetComponentIdx(i, 3));
        }

        public class Exclude<Exc1> : Filter<TInc1,TInc2,TInc3,TInc4>
            where Exc1 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>();
            }
        }

        public class Exclude<Exc1, Exc2> : Filter<TInc1, TInc2, TInc3, TInc4>
            where Exc1 : struct
            where Exc2 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>().Exclude<Exc2>();
            }
        }
    }
    
    public class Filter<TInc1,TInc2,TInc3,TInc4,TInc5> : Filter
        where TInc1 : struct
        where TInc2 : struct
        where TInc3 : struct
        where TInc4 : struct
        where TInc5 : struct
    {
        readonly ComponentPool<TInc1> pool1;
        readonly ComponentPool<TInc2> pool2;
        readonly ComponentPool<TInc3> pool3;
        readonly ComponentPool<TInc4> pool4;
        readonly ComponentPool<TInc5> pool5;

        public Filter(World world, int filterIdx) : base(world, filterIdx,new FilterMask()
            .Include<TInc1>()
            .Include<TInc2>()
            .Include<TInc3>()
            .Include<TInc4>()
            .Include<TInc5>())
        {
            pool1 = world.GetComponentPool<TInc1>();
            pool2 = world.GetComponentPool<TInc2>();
            pool3 = world.GetComponentPool<TInc3>();
            pool4 = world.GetComponentPool<TInc4>();
            pool5 = world.GetComponentPool<TInc5>();
        }

        public ref TInc1 Get1(int i)
        {
            return ref pool1.GetComponent(GetComponentIdx(i, 0));
        }
        
        public ref TInc2 Get2(int i)
        {
            return ref pool2.GetComponent(GetComponentIdx(i, 1));
        }
        
        public ref TInc3 Get3(int i)
        {
            return ref pool3.GetComponent(GetComponentIdx(i, 2));
        }
        
        public ref TInc4 Get4(int i)
        {
            return ref pool4.GetComponent(GetComponentIdx(i, 3));
        }
        
        public ref TInc5 Get5(int i)
        {
            return ref pool5.GetComponent(GetComponentIdx(i, 4));
        }

        public class Exclude<Exc1> : Filter<TInc1,TInc2,TInc3,TInc4>
            where Exc1 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>();
            }
        }

        public class Exclude<Exc1, Exc2> : Filter<TInc1, TInc2, TInc3, TInc4>
            where Exc1 : struct
            where Exc2 : struct
        {
            public Exclude(World world, int filterIdx) : base(world,filterIdx)
            {
                filterMask.Exclude<Exc1>().Exclude<Exc2>();
            }
        }
    }

    
    
}