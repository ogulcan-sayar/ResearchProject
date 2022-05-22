using RenderLibrary.IO;
using SimulationSystem.ECSSystems;
using SimulationSystem.SharedData;
using SimulationWFA.MespSimulationSystem.ProgramLibrary.EditorWindowSystem.Systems;
using SimulationWFA.ShowCinematic;
using SimulationWFA.SimulationAlgorithms;
using SimulationWFA.SimulationAlgorithms.AStar;

namespace SimulationSystem.Systems
{
    public class ECSEditorController : EasyECSController
    {
        public const int GenericSystemGroup = 0; // Always active systems
        public Screen screen;

        PathRequestManager pathRequestManager = new PathRequestManager();

        public ECSEditorController(Screen screen)
        {
            this.screen = screen;
        }

        public override void OnInject()
        {
            systemManager.Inject(screen);
            systemManager.Inject(new ModelPaths());
            systemManager.Inject(new TextureReferences());
            systemManager.Inject(pathRequestManager);
        }

        public override void AddSystems() // Ecs Sistemleri
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

            systemManager.AddSystem(new FPSCalculatorSystem(), GenericSystemGroup);
            systemManager.AddSystem(new TransformSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new SceneSelectObjectSystem(), GenericSystemGroup);

            //Custom Systems

            //systemManager.AddSystem(new TestSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new PhysicTestSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new TriggerTestSystem(), GenericSystemGroup);
            //systemManager.AddSystem(new RaycastTestSystem(), GenericSystemGroup);
            systemManager.AddSystem(new ObstacleUpdateSystem(), GenericSystemGroup);
            systemManager.AddSystem(new GridSystem(), GenericSystemGroup);

            systemManager.AddSystem(new UnitPathFindSystem(), GenericSystemGroup);
            systemManager.AddSystem(new VisualizeShortestPathAlgorithmSystem(), GenericSystemGroup);
            systemManager.AddSystem(new UnitFollowPathSystem(), GenericSystemGroup);
            systemManager.AddSystem(new UnitReturnPathSystem(), GenericSystemGroup);
            systemManager.AddSystem(new RestartUnitSystem(), GenericSystemGroup);

            systemManager.AddSystem(new SleepingModeSystem(), GenericSystemGroup);
            systemManager.AddSystem(new CameraRotateSystem(), GenericSystemGroup);
            
            //
            systemManager.AddSystem(new EditorCameraSystem(), GenericSystemGroup);

            //RenderSystems 
            systemManager.AddSystem(new AnimationSystem(), GenericSystemGroup);
            systemManager.AddSystem(new LightSystem(), GenericSystemGroup);

            systemManager.AddSystem(new MeshRenderSystem(), GenericSystemGroup);
            systemManager.AddSystem(new OutlineBorderRenderSystem(), GenericSystemGroup);

            systemManager.AddSystem(new EditorInfiniteGridSystem(), GenericSystemGroup);
            systemManager.AddSystem(new MespEditorDebugSystem(), GenericSystemGroup);

            systemManager.AddSystem(new TextRendererSystem(), GenericSystemGroup);

            systemManager.AddSystem(new SkyboxRenderSystem(), GenericSystemGroup);
            //EventSystems
            systemManager.AddSystem(new EditorEventListenSystem(), GenericSystemGroup);
            systemManager.AddSystem(new EditorTransformSyncSystem(), GenericSystemGroup);
        }
    }
}