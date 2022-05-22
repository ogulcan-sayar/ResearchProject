using RenderLibrary.IO;

namespace SimulationSystem
{
    public class WindowEcsManager
    {
        public EasyECSController ecsController;

        public WindowEcsManager(EasyECSController ecsController)
        {
            this.ecsController = ecsController;
        }
        
        public void Awake()
        {
            ecsController.Awake();
        }

        public void Start()
        {
            ecsController.Start();
        }

        public void Update()
        {
            ecsController.Update();
        }

        public void FixedUpdate()
        {
            ecsController.FixedUpdate();
        }
        
        public void LateUpdate()
        {
            ecsController.LateUpdate();
        }

        public void Render()
        {
            ecsController.Render();
        }

        public void PostRender()
        {
            ecsController.PostRender();
        }


        //TODO: on destroy

        public void OnSimulationQuit()
        {
            ecsController.OnApplicationQuit();
        }
    }
}