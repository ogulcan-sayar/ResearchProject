using Dalak.Ecs;
using RenderLibrary.IO;

namespace SimulationSystem
{
    public abstract class EasyECSController
    {
        public World world;
        public SystemManager systemManager;

        public void Awake()
        {
            world = new World();
            systemManager = new SystemManager(world);

            AddSystems();

            OnInject();
            systemManager.Awake();
        }

        public void Start()
        {
            systemManager.Start();
        }

        public void FixedUpdate()
        {
            //DTime.fixedDeltaTime = Time.fixedDeltaTime;
            systemManager.FixedUpdate();
        }

        public void Update()
        {
            //DTime.deltaTime = Time.deltaTime;
            systemManager.Update();
        }

        public void LateUpdate()
        {
            systemManager.LateUpdate();
        }

        public void Render()
        {
            systemManager.Render();
        }

        public void PostRender()
        {
            systemManager.PostRender();
        }

        public void OnDestroy()
        {
            systemManager.OnDestroy();
        }

        public void OnApplicationQuit()
        {
            systemManager.OnApplicationQuit();
        }


        public abstract void OnInject();
        public abstract void AddSystems();
    }
}