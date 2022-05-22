using System;

namespace Dalak.Ecs
{
    public struct DelayedAction
    {
        public float timer;
        public Action action;
    }
    public abstract class System
    {
        public int groupFlags;
        public bool active = true;
        public bool disabled = false;
        public World world = null;
        
        public DynamicArray<DelayedAction> callLaterActions = new DynamicArray<DelayedAction>(8);
        
        public virtual void Awake(){}
        public virtual void Start(){}
        public virtual void Update(){}
        public virtual void FixedUpdate(){}
        public virtual  void LateUpdate(){}
        public virtual void Render(){}
        public virtual void PostRender(){}
        public virtual void OnDestroy(){}
        
        public virtual void OnApplicationQuit(){}

        public SystemManager manager = null;


        public void CallLater(float duration, Action action)
        {
            callLaterActions.Add() = new DelayedAction
            {
                timer = duration,
                action = action
            };
        }
        
        
    }
}