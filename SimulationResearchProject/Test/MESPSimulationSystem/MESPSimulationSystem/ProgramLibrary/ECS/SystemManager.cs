using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dalak.Ecs
{
    public class SystemManager
    {
        delegate Entity CreateEvent(World world);

        readonly World world;
        public readonly List<System> systems = new List<System>();
        readonly Dictionary<Type,object> injections = new Dictionary<Type, object>();
        readonly List<CreateEvent> createEvents = new List<CreateEvent>();
        readonly List<Entity> eventEntities = new List<Entity>();
        
        public SystemManager(World world)
        {
            this.world = world;
            world.OnDestroy += OnDestroy;
        }

        public T AddSystem<T>(T system, int systemGroupFlags) where T : System
        {
            system.manager = this;
            system.world = world;
            system.groupFlags = systemGroupFlags;
            systems.Add(system);
            return system;
        }
        
        public T InsertSystem<T>(T system, int idx, int systemGroupFlags) where T : System
        {
            system.manager = this;
            system.world = world;
            system.groupFlags = systemGroupFlags;
            systems.Insert(idx, system);
            return system;
        }

        /// enables system if its group flags contains given flag
        public void EnableSystems(int singleFlag)
        {
            foreach (var system in systems)
            {
                if ((system.groupFlags & singleFlag) != 0)
                {
                    system.disabled = false;
                }
            }
        }

        /// disables system if its group flags contains given flag
        public void DisableSystems(int systemGroupFlags)
        {
            foreach (var system in systems)
            {
                if ((system.groupFlags & systemGroupFlags) != 0)
                {
                    system.disabled = true;
                }
            }
        }

        void HandleInjections()
        {
            foreach (var system in systems)
            {
                var systemType = system.GetType();
                var filterType = typeof (Filter);

                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                foreach (var f in systemType.GetFields (flags))
                {
                    if (f.IsStatic) 
                    {
                        continue;
                    }
                    // Filter
                    if (f.FieldType.IsSubclassOf (filterType))
                    {
                        Filter filter = world.GetFilter(f.FieldType);
#if UNITY_EDITOR
                        filter.name = systemType.Name + "." + f.Name;
#endif
                        f.SetValue(system, filter);
                        continue;
                    }
                    // Other injections.
                    foreach (var pair in injections) 
                    {
                        if (f.FieldType.IsAssignableFrom (pair.Key)) 
                        {
                            f.SetValue(system, pair.Value);
                            break;
                        }
                    }
                }
            }
        }

        public void Inject<T>(T o)
        {
            injections.Add(typeof(T),o);
        }
        
        public void SendEvent<T>(T t = default) where T: struct
        {
            createEvents.Add((w) =>
            {
                var e = w.NewEntity();
                e.AddComponent<T>() = t;
                return e;
            });
        }
       
        // UNITY EVENTS
        public void Awake()
        {
            HandleInjections();
            foreach (var system in systems)
            {
                system.Awake();
            }
        }

        public void Start()
        {
            foreach (var system in systems)
            {
                system.Start();
            }
        }

        
        public void FixedUpdate()
        {
            foreach (var system in systems)
            {
                if (system.active && !system.disabled)
                {
                    system.FixedUpdate();
                }
            }
        }
        
        public void Update()
        {
            foreach (var entity in eventEntities)
            {
                entity.Destroy();
            }
            eventEntities.Clear();
            foreach (var createEvent in createEvents)
            {
                eventEntities.Add(createEvent(world));
            }
            createEvents.Clear();
            
            
            foreach (var system in systems)
            {
                for (int i = 0; i < system.callLaterActions.numberOfItems; i++)
                {
                    system.callLaterActions[i].timer -= DTime.deltaTime;
                    if (system.callLaterActions[i].timer <= 0)
                    {
                        system.callLaterActions[i].action.Invoke();
                        system.callLaterActions[i] = system.callLaterActions[system.callLaterActions.numberOfItems - 1];
                        system.callLaterActions.numberOfItems--;
                        i--;
                    }
                }
                
                if (system.active && !system.disabled)
                {
                    system.Update();
                }
            }
        }

        public void LateUpdate()
        {
            foreach (var system in systems)
            {
                if (system.active && !system.disabled)
                {
                    system.LateUpdate();
                }
            }
        }

        public void Render()
        {
            foreach (var system in systems)
            {
                if (system.active && !system.disabled)
                {
                    system.Render();
                }
            }
        }
       
        public void OnDestroy()
        {
            foreach (var system in systems)
            {
                system.OnDestroy();
            }
        }

    }

}