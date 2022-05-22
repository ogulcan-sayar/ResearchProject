namespace SimulationSystem
{
    public class SimulationEvents
    {
        private EasyECSController ecsController;

        public SimulationEvents(EasyECSController ecsController)
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

        //TODO: fixedupdate()
        
        public void LateUpdate()
        {
            ecsController.LateUpdate();
        }

        public void Render()
        {
            ecsController.Render();
        }

        public void OnSimulationQuit()
        {
            ecsController.OnDestroy();
        }
    }
}