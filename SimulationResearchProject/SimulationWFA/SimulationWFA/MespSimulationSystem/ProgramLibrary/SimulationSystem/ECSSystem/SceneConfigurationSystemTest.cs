using System;
using System.Numerics;
using Dalak.Ecs;
using ECSEntegration.SerializedComponent;
using PhysicLibrary;
using ProgramLibrary;
using RenderLibrary.Animations;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.PreparedModels;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;
using RenderLibrary.Transform;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;
using SimulationSystem.SharedData;
using SimulationWFA.MespUtils;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{
    public class SceneConfigurationSystemTest : Dalak.Ecs.System
    {
        ModelPaths modelReferences = null;
        TextureReferences textureRef = null;

        public delegate void SceneIsReadyDelegate();
        public static event SceneIsReadyDelegate SceneIsReadyEvent;

        public override void Awake()
        {
            SceneManager.LoadScene("testScene",world);

            /*CreateEditorCamera();

            CreateDirectionalLight();

            CreateFPSDisplayer();

            CreateLambs();*/

            CreateBasicCube();
           // CreateBasicCube2();
            CreateBasicCube3();
            CreateBasicCube4();
            CreateBasicCube5();

            CreateBristleback1(new Vector3(1000,0,0));
            CreateBristleback1(new Vector3(960, 0, 0));


            /*// 

             //CreateBristleback2();

            // CreateJunkrat();

           //  CreateTrol();

             //CreateWindow();

             CreatePlane();
            // CreateDirectionArrows();


            // CreateGrass();*/

            CreateMespVersionText();

            if (SceneIsReadyEvent != null)
            {
                SceneIsReadyEvent();
            }

            //SceneManager.SaveScene("testScene");
        }

        //Create mesp version text
        public void CreateMespVersionText()
        {
            Entity entity = world.NewEntity();
            entity.AddComponent<TextRendererComp>() = new TextRendererComp() {
                text = "MESP ALPHA VERSION V0.1",
                color = new Vector3(0, 1, 1),
                UIPosition = new Vector2(0,30),
                scale = 16f,
            };
        }

        //Camera entity
        public void CreateEditorCamera()
        {
            var camSimObj = SimObject.NewSimObject("Editor Camera");
            camSimObj.CreateEntity(world);
            CameraSerialized camSerialized = new CameraSerialized() {
                speed = 2.5f,
                zoom = 45f,
                near = 0.1f,
                far = 100f,
            };

            SpotLightSerialized spotLightSerialized = new SpotLightSerialized() {
                cutOff = 5f,
                outerCutOff = 10f,
                ambient = new Vector4(0.0f, 0.0f, 0.0f, 1.0f),
                diffuse = Vector4.One,
                specular = Vector4.One,

            };

            camSimObj.AddNewSerializedComponent(camSerialized);
            camSimObj.AddNewSerializedComponent(spotLightSerialized);
            camSimObj.InjectAllSerializedComponents(world);
        }

        //Directional light entity
        public void CreateDirectionalLight()
        {
            var dirLightSimObj = SimObject.NewSimObject("Directional Light");
            dirLightSimObj.CreateEntity(world);

            DirectionalLightSerialized dirLightSerialized = new DirectionalLightSerialized();
            dirLightSerialized.ambient = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            dirLightSerialized.diffuse = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            dirLightSerialized.specular = new Vector4(0.75f, 0.75f, 0.75f, 1.0f);

            dirLightSimObj.AddNewSerializedComponent(dirLightSerialized);
            dirLightSimObj.InjectAllSerializedComponents(world);
        }

        //Lamba entitileri
        public void CreateLambs()
        {
            Transform[] lambTransforms = new[]
            {
                new Transform(new Vector3(0.7f, 5.0f, 10.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(2.3f, -3.3f, -4.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(-4.0f, 2.0f, -12.0f),new Vector3(.25f), Vector3.Zero),
                new Transform(new Vector3(0.0f, 0.0f, -3.0f),new Vector3(.25f), Vector3.Zero),
            };

            SimObject[] lambSimObj = new SimObject[4];
            for (int i = 0; i < 4; i++)
            {
                lambSimObj[i] = SimObject.NewSimObject("Lamb " + i.ToString());
                lambSimObj[i].CreateEntity(world);

                PointLightSerialized pointLightSerialized = new PointLightSerialized() {
                    ambient = new Vector4(0.05f, 0.05f, 0.05f, 1.0f),
                    diffuse = new Vector4(0.8f, 0.8f, 0.8f, 1.0f),
                    specular = new Vector4(1.0f),
                };

                MeshRendererSerialized meshRendererSerialized = new MeshRendererSerialized() {
                    materialPath = "lambMaterial.mat",
                    meshPath = "cube.mesh",
                };

                lambSimObj[i].AddNewSerializedComponent(pointLightSerialized);
                lambSimObj[i].AddNewSerializedComponent(meshRendererSerialized);
                lambSimObj[i].InjectAllSerializedComponents(world);

                lambSimObj[i].entity.GetComponent<TransformComp>().transform = lambTransforms[i];
            }
        }

        //Human entity
        public void CreateJunkrat()
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.JunkratPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.JunkratPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = humanModel,
            };

            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

            rootSimObj.GetTransform().scale = new Vector3(0.05f);
        }


        public void CreateBasicCube()
        {
            var basicCubeObj = SimObject.NewSimObject("Cube");
            basicCubeObj.CreateEntity(world);

            MeshRendererSerialized meshRendererSerialized = new MeshRendererSerialized() {
                meshPath = "cube.mesh",
                materialPath = "lambMaterial.mat",
            };



            basicCubeObj.AddNewSerializedComponent(meshRendererSerialized);
            basicCubeObj.InjectAllSerializedComponents(world);

            ref var transformComp = ref basicCubeObj.entity.GetComponent<TransformComp>();
            transformComp.transform.scale = Vector3.One;
            transformComp.transform.position = new Vector3(980, 5, 20);

            ref var meshRendererComp = ref basicCubeObj.entity.GetComponent<MeshRendererComp>();
            var material = (UnlitMaterial) meshRendererComp.material;
            material.SetColor(new Vector4(1, 0, 0, 1));


            Particle rg = new Particle();
            rg.SetMass(1f);
            rg.velocity = new Vector3(0,-40,0);
            rg.useGravity = false;

            basicCubeObj.entity.AddComponent<ParticleComp>() = new ParticleComp {
                particle = rg,
            };

            SphereCollider sphereCollider = new SphereCollider();
            (sphereCollider.bound as SphereBounds).radius = .5f;
            basicCubeObj.entity.AddComponent<ColliderComp>() = new ColliderComp {
                collider = sphereCollider,
            };
        }

        public void CreateBasicCube2()
        {
            var basicCubeObj = SimObject.NewSimObject("Cube");
            basicCubeObj.CreateEntity(world);

            UnlitMaterial cubeMaterial = AssetUtils.LoadFromAsset<UnlitMaterial>("lambMaterial.mat");
            cubeMaterial.SetColor(new Vector4(1, 0, 0, 1));

            BoxColliderSerialized boxColliderSerialized = new BoxColliderSerialized() {

                size = basicCubeObj.GetSerializedComponent<TransformSerialized>().scale,
            };

            TriggerSerialized triggerSerialized = new TriggerSerialized();

            basicCubeObj.AddNewSerializedComponent(boxColliderSerialized);
            basicCubeObj.AddNewSerializedComponent(triggerSerialized);
            basicCubeObj.InjectAllSerializedComponents(world);

            ref var transformComp = ref basicCubeObj.entity.GetComponent<TransformComp>();
            transformComp.transform.scale = Vector3.One;
            transformComp.transform.position = new Vector3(10, 0, 4);

        }

        public void CreateBasicCube3()
        {
            var basicCubeObj = SimObject.NewSimObject();
            basicCubeObj.CreateEntity(world);
            basicCubeObj.InjectAllSerializedComponents(world);

            ref var transformComp = ref basicCubeObj.entity.GetComponent<TransformComp>();
            transformComp.transform.scale = Vector3.One;
            transformComp.transform.position = new Vector3(980, 0, 20);

            UnlitMaterial cubeMaterial = AssetUtils.LoadFromAsset<UnlitMaterial>("lambMaterial.mat");
            cubeMaterial.SetColor(new Vector4(0, 0, 1, 1));

            basicCubeObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                mesh = AssetUtils.LoadFromAsset<Mesh>("cube.mesh"),
                material = cubeMaterial,
            };

            Particle rg = new Particle();
            rg.SetMass(1f, true);
            rg.velocity = Vector3.Zero;
            rg.useGravity = false;

            basicCubeObj.entity.AddComponent<ParticleComp>() = new ParticleComp {
                particle = rg,
            };

            SphereCollider sphereCollider = new SphereCollider();
            (sphereCollider.bound as SphereBounds).radius = .5f;
            basicCubeObj.entity.AddComponent<ColliderComp>() = new ColliderComp {
                collider = sphereCollider,
            };

            basicCubeObj.entity.AddComponent<CanMoveTestTag>();
        }

        public void CreateBasicCube4()
        {
            var basicCubeObj = SimObject.NewSimObject();
            basicCubeObj.CreateEntity(world);
            basicCubeObj.InjectAllSerializedComponents(world);

            ref var transformComp = ref basicCubeObj.entity.GetComponent<TransformComp>();
            transformComp.transform.scale = Vector3.One;
            transformComp.transform.position = new Vector3(980, 3, 20);

            UnlitMaterial cubeMaterial = AssetUtils.LoadFromAsset<UnlitMaterial>("lambMaterial.mat");
            cubeMaterial.SetColor(new Vector4(1, 0, 0, 1));

            basicCubeObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                mesh = AssetUtils.LoadFromAsset<Mesh>("cube.mesh"),
                material = cubeMaterial,
            };

            Particle rg = new Particle();
            rg.SetMass(1f);
            rg.velocity = new Vector3(0,60,0);
            rg.useGravity = false;

            basicCubeObj.entity.AddComponent<ParticleComp>() = new ParticleComp {
                particle = rg,
            };

            SphereCollider sphereCollider = new SphereCollider();
            (sphereCollider.bound as SphereBounds).radius = .5f;
            basicCubeObj.entity.AddComponent<ColliderComp>() = new ColliderComp {
                collider = sphereCollider,
            };

            basicCubeObj.entity.AddComponent<CanMoveTestTag>();
        }

        public void CreateBasicCube5()
        {
            var basicCubeObj = SimObject.NewSimObject();
            basicCubeObj.CreateEntity(world);
            basicCubeObj.InjectAllSerializedComponents(world);

            ref var transformComp = ref basicCubeObj.entity.GetComponent<TransformComp>();
            transformComp.transform.scale = Vector3.One;
            transformComp.transform.position = new Vector3(980, 6, 20);

            UnlitMaterial cubeMaterial = AssetUtils.LoadFromAsset<UnlitMaterial>("lambMaterial.mat");
            cubeMaterial.SetColor(new Vector4(0, 0, 1, 1));

            basicCubeObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                mesh = AssetUtils.LoadFromAsset<Mesh>("cube.mesh"),
                material = cubeMaterial,
            };

            Particle rg = new Particle();
            rg.SetMass(1f, true);
            rg.velocity = Vector3.Zero;
            rg.useGravity = false;

            basicCubeObj.entity.AddComponent<ParticleComp>() = new ParticleComp {
                particle = rg,
            };

            SphereCollider sphereCollider = new SphereCollider();
            (sphereCollider.bound as SphereBounds).radius = .5f;
            basicCubeObj.entity.AddComponent<ColliderComp>() = new ColliderComp {
                collider = sphereCollider,
            };

            basicCubeObj.entity.AddComponent<CanMoveTestTag>();
        }

        public void CreateGruGru(Vector3 pos)
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.GruGruPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.GruGruPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = humanModel,
            };

            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

            rootSimObj.GetTransform().scale = new Vector3(0.05f);
            rootSimObj.GetTransform().position = pos;
            rootSimObj.GetTransform().rotation = new Vector3(0,180,0);
        }



        public void CreateBristleback1(Vector3 pos)
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.BristlebackPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.BristlebackPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = humanModel,
            };


            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

            rootSimObj.GetTransform().scale = new Vector3(0.05f);
            rootSimObj.GetTransform().position = pos;


        }

        public void CreateBristleback2()
        {
            var humanModel = ModelLoader.LoadModel(modelReferences.BristlebackPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Animation animation = new Animation(modelReferences.BristlebackPath, humanModel);
            rootSimObj.entity.AddComponent<AnimatorComp>() = new AnimatorComp() {
                animator = new Animator(animation),
            };

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = humanModel,
            };

            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

            rootSimObj.GetTransform().scale = new Vector3(0.05f);
            rootSimObj.GetTransform().position = new Vector3(0, 0, -10f);
        }

        //Trol entity
        public void CreateTrol()
        {

            var trolModel = ModelLoader.LoadModel(modelReferences.TrolModelPath);

            var rootSimObj = SimObject.NewSimObject();
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            rootSimObj.entity.AddComponent<SkinnedMeshRendererComp>() = new SkinnedMeshRendererComp {
                rootModel = trolModel,
            };

            ref var skinnedMeshComp = ref rootSimObj.entity.GetComponent<SkinnedMeshRendererComp>();
            skinnedMeshComp.SetMeshRenderers(world, ref rootSimObj);

        }

        public void CreateDirectionArrows()
        {
            var arrowModel = ModelLoader.LoadModel(modelReferences.DiretionArrowPath);
            var rootSimObj = SimObject.NewSimObject("DirectionArrow");
            rootSimObj.CreateEntity(world);
            rootSimObj.InjectAllSerializedComponents(world);

            Mesh arrowMesh = new DirectionArrowMesh();
            UnlitMaterial cubeMaterial = AssetUtils.LoadFromAsset<UnlitMaterial>("lambMaterial.mat");
            cubeMaterial.SetColor(new Vector4(1, 0, 0, 1));

            rootSimObj.entity.AddComponent<TransformPositionArrowComp>() = new TransformPositionArrowComp {
                mesh = arrowMesh,
                material = cubeMaterial,
            };

        }


        public void CreatePlane()
        {
            var planeSimObj = SimObject.NewSimObject("Plane");
            planeSimObj.CreateEntity(world);

            MeshRendererSerialized meshRendererSerialized = new MeshRendererSerialized() {
                materialPath = "groundMaterial.mat",
                meshPath = "cube.mesh",
            };

            var transfromSerialized = planeSimObj.GetSerializedComponent<TransformSerialized>();
            transfromSerialized.pos = new Vector3(10, 0f, 0);
            transfromSerialized.scale = new Vector3(10, .1f, 10);


            BoxColliderSerialized boxColliderSerialized = new BoxColliderSerialized() {
                size = planeSimObj.GetSerializedComponent<TransformSerialized>().scale,
            };

            planeSimObj.AddNewSerializedComponent(meshRendererSerialized);
            planeSimObj.AddNewSerializedComponent(boxColliderSerialized);
            planeSimObj.InjectAllSerializedComponents(world);
        }

        public void CreateGrass()
        {
            var grassObj = SimObject.NewSimObject();
            grassObj.CreateEntity(world);
            grassObj.InjectAllSerializedComponents(world);

            ref var transform = ref grassObj.entity.GetComponent<TransformComp>().transform;
            transform.position = new Vector3(0, .55f, 10);
            transform.scale = new Vector3(1, 1f, 1); ;

            grassObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = AssetUtils.LoadFromAsset<UnlitMaterial>("grassMaterial.mat"),
                mesh = AssetUtils.LoadFromAsset<Mesh>("quad.mesh"),
            };

        }

        public void CreateWindow()
        {
            var windowObj = SimObject.NewSimObject();
            windowObj.CreateEntity(world);
            windowObj.InjectAllSerializedComponents(world);

            ref var transform = ref windowObj.entity.GetComponent<TransformComp>().transform;
            transform.position = new Vector3(0, .55f, 8);
            transform.scale = new Vector3(1, 1f, 1); ;

            windowObj.entity.AddComponent<MeshRendererComp>() = new MeshRendererComp {
                material = AssetUtils.LoadFromAsset<UnlitMaterial>("WindowMaterial.mat"),
                mesh = AssetUtils.LoadFromAsset<Mesh>("quad.mesh"),
            };
        }

        public void CreateFPSDisplayer()
        {
            var simObj = SimObject.NewSimObject("FPS Displayer");
            simObj.CreateEntity(world);

            TextRendererSerialized textRendererSerialized = new TextRendererSerialized() {
                color = new Vector3(1, 1, 1),
                UIPosition = new Vector2(0, 500),
                scale = 30,
                text = "FPS: ",
            };
            simObj.AddNewSerializedComponent(new FPSDisplaySerialized());
            simObj.AddNewSerializedComponent(textRendererSerialized);

            simObj.InjectAllSerializedComponents(world);

        }
        //Grid entity
        public void CreateGridSystem()
        {
     
            
        }
    }
}