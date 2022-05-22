using RenderLibrary.IO;
using SimulationSystem.ECSSystems;
using SimulationSystem.SharedData;
using SimulationSystem.Systems;

namespace SimulationSystem
{
    public class ECSSimulationController : EasyECSController
    {
        public const int GenericSystemGroup = 0; // Always active systems
        public Screen screen;

        public ECSSimulationController(Screen screen)
        {
            this.screen = screen;
        }

        public override void OnInject()
        {
            systemManager.Inject(screen);
            systemManager.Inject(new ModelPaths());
            systemManager.Inject(new TextureReferences());
        }

        public override void AddSystems()
        {
            systemManager.AddSystem(new SceneConfigurationSystemTest(), GenericSystemGroup);

            //physics
            systemManager.AddSystem(new UpdateForceSystem(), GenericSystemGroup);
            systemManager.AddSystem(new ParticleMovementSystem(), GenericSystemGroup);
            systemManager.AddSystem(new ColliderBoundsUpdateSystem(), GenericSystemGroup);
            systemManager.AddSystem(new CollisionDetectionSystem(), GenericSystemGroup);
            systemManager.AddSystem(new TriggerDetectionSystem(), GenericSystemGroup);
            systemManager.AddSystem(new ResolveCollisionSystem(), GenericSystemGroup);
            systemManager.AddSystem(new SimulationPhysicEntegrationSystem(), GenericSystemGroup);

            
            systemManager.AddSystem(new TransformSystem(), GenericSystemGroup);

            //custom systems


            //

            systemManager.AddSystem(new EditorCameraSystem(), GenericSystemGroup);


            //RenderSystems 
            systemManager.AddSystem(new AnimationSystem(), GenericSystemGroup);
            systemManager.AddSystem(new LightSystem(), GenericSystemGroup);

            systemManager.AddSystem(new MeshRenderSystem(), GenericSystemGroup);

            systemManager.AddSystem(new TextRendererSystem(), GenericSystemGroup);

            systemManager.AddSystem(new MespEditorDebugSystem(), GenericSystemGroup);
        }
    }
}