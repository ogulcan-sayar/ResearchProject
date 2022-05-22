using SimulationSystem.Systems;

namespace SimulationSystem
{
    public class ECSSimulationController : EasyECSController
    {
        public const int GenericSystemGroup = 0; // Always active systems
        
        public override void OnInject()
        {
            
        }

        public override void AddSystems()
        {
            systemManager.AddSystem(new SceneLoadingSystem(), GenericSystemGroup);
            
            systemManager.AddSystem(new MeshRenderSystem(), GenericSystemGroup);
        }
    }
}