using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using Newtonsoft.Json;
using ProgramLibrary;
using SimulationSystem;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.MespUtils
{
    public static class SceneManager
    {

        public static void SaveScene(string sceneName)
        {
            var simObjArr = SimObject.FindObjectsOfType<TransformSerialized>();

            string saveFolderPath = Path.Combine(SimPath.ScenesPath, sceneName);
            if (Directory.Exists(saveFolderPath))
            {
                Directory.Delete(saveFolderPath,true);
            }

            Directory.CreateDirectory(saveFolderPath);

            foreach (var simObj in simObjArr)
            {
                string simObjSavePath = Path.Combine(saveFolderPath, simObj.objectData.name);
                Directory.CreateDirectory(simObjSavePath);
                var serializedComponents = simObj.objectData.GetSerializedComponents();


                foreach (var serializedComponent in serializedComponents)
                {
                    var serializedFile = Path.Combine(simObjSavePath, serializedComponent.GetName() + ".comp");
                    var json = JsonConvert.SerializeObject(serializedComponent, Formatting.Indented);
                    File.WriteAllText(serializedFile, json);
                }
            }
        }

        public static void LoadScene(string sceneName, World world)
        {
            var sceneDirectoryPath = Directory.GetDirectories(SimPath.ScenesPath, sceneName);

            if (sceneDirectoryPath == null)
            {
                Console.WriteLine("Empty scene !");
                return;
            }

            var simObjectsDirectories = Directory.GetDirectories(sceneDirectoryPath[0]);

            foreach (var simObjPath in simObjectsDirectories)
            {
                var directoryInfo = new DirectoryInfo(simObjPath);

                var newSimObj = SimObject.NewSimObject(directoryInfo.Name);
                newSimObj.CreateEntity(world);

                var componentPaths = directoryInfo.GetFiles();
               
                newSimObj.objectData.RemoveSerializedComp(typeof(TransformSerialized));
                newSimObj.entity.RemoveComponent<TransformComp>();

                foreach (var componentPath in componentPaths)
                {

                    var dataString = File.ReadAllText(componentPath.FullName);
                    var tempData = (TempSerializedData)JsonConvert.DeserializeObject(dataString, typeof(MespUtils.TempSerializedData));

                    var data = (SerializedComponent)JsonConvert.DeserializeObject(dataString, tempData.type);
                    data.SetOwner(newSimObj);
                    newSimObj.AddNewSerializedComponent(data);
                }

                newSimObj.InjectAllSerializedComponents(world);
            }

        }
    }
}
