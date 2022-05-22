using System;
using System.Collections.Generic;
using ECSEntegration.SerializedComponent;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.ECSComponents;
using SimulationWFA.MespSimulationSystem.ProgramLibrary.ECSEntegration.SerializedComponent;
using SimulationWFA.SimulationAlgorithms.AStar;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.MespSimulationSystem.ProgramLibrary
{
    public static class SerializedComponentPool
    {
        public static Dictionary<int, Type> SerializedCompTypes = new Dictionary<int, Type>() {

            {0,  typeof(TransformSerialized)},
            {1, typeof(MeshRendererSerialized)},
            {2, typeof(BoxColliderSerialized)},
            {3, typeof(CameraSerialized)},
            {4, typeof(DirectionalLightSerialized)},
            {5, typeof(ParticleSerialized)},
            {6, typeof(PointLightSerialized)},
            {7, typeof(SpotLightSerialized)},
            {8, typeof(TextRendererSerialized)},
            {9, typeof(TriggerSerialized)},
            {10, typeof(ObstacleSerialized)},
            {11, typeof(UnitSerialized)},
            {12, typeof(TargetSerialized)},
            {13, typeof(SkinnedMeshSerialized)},

           // {2, new TestSystemSerialized()}
        };

        public static string[] SerializedCompNames = {

            "TransformSerialized",
            "MeshRendererSerialized",
            "BoxColliderSerialized",
            "CameraSerialized",
            "DirectionalLightSerialized",
            "ParticleSerialized",
            "PointLightSerialized",
            "SpotLightSerialized",
            "TextRendererSerialized",
            "TriggerSerialized",
            "ObstacleSerialized",
            "UnitSerialized",
            "TargetSerialized",
            "SkinnedMeshSerialized",
        };

        public static Type GetSerializedComponent(string serializedCompName)
        {
            switch (serializedCompName)     
            {
                case "Transform Serialized":
                    return typeof(TransformSerialized);
                    break;
                case "Mesh Serialized":
                    return typeof(MeshRendererSerialized);
                    break;
                case "Box Collider Serialized":
                    return typeof(BoxColliderSerialized);
                    break;
                case "Camera Serialized":
                    return typeof(CameraSerialized);
                    break;
                case "Directional Light Serialized":
                    return typeof(DirectionalLightSerialized);
                    break;
                case "Particle Serialized":
                    return typeof(ParticleSerialized);
                    break;
                case "Point Light Serialized":
                    return typeof(PointLightSerialized);
                    break;
                case "Spot Light Serialized":
                    return typeof(SpotLightSerialized);
                    break;
                case "Text Renderer Serialized":
                    return typeof(TextRendererSerialized);
                    break;
                case "Trigger Serialized":
                    return typeof(TriggerSerialized);
                    break;
                default:
                    break;
            }

            return null;
        }

        public static Type GetComponentForRemove(string serializedCompName)
        {
            switch (serializedCompName)
            {
                case "Transform Serialized":
                    return typeof(TransformComp);
                    break;
                case "Mesh Serialized":
                    return typeof(MeshRendererComp);
                    break;
                case "Box Collider Serialized":
                    return typeof(ColliderComp);
                    break;
                case "Camera Serialized":
                    return typeof(CameraComp);
                    break;
                case "Directional Light Serialized":
                    return typeof(DirectionalLightComp);
                    break;
                case "Particle Serialized":
                    return typeof(ParticleComp);
                    break;
                case "Point Light Serialized":
                    return typeof(PointLightComp);
                    break;
                case "Spot Light Serialized":
                    return typeof(SpotLightComp);
                    break;
                case "Text Renderer Serialized":
                    return typeof(TextRendererComp);
                    break;
                case "Trigger Serialized":
                    return typeof(TriggerComp);
                    break;
                default:
                    break;
            }

            return null;
        }
        public static SerializedComponent ReturnNewComponentFromList(int idx)
        {
            return Activator.CreateInstance(SerializedCompTypes[idx]) as SerializedComponent;
        }
    }
}