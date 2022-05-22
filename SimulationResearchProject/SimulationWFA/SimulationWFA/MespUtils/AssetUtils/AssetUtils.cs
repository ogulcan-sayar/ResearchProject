using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;

namespace SimulationWFA.MespUtils
{
    public static class AssetUtils
    {
        // supported extensions --> .mat , .texture, .mesh
        // eğer asset dosyasından varsa bir daha yaratmaz !

        public static void CreateAsset(object asset, string fileName)
        {
            IAssetSerializator assetSerializator = asset as IAssetSerializator;

            if (assetSerializator == null) return;

            var assetFileName = FindFileByType(fileName);

            if (assetFileName == null)
            {
                Console.WriteLine("Unsupported extenxtions type");
                return;
            }

            Directory.CreateDirectory(assetFileName);
            string directoryPath = Path.Combine(assetFileName, fileName);

            if (File.Exists(directoryPath))
            {
                return;
            }

            var seriliazedObj = assetSerializator.Serializate();
            AssetSerializationData serializedData = (AssetSerializationData)seriliazedObj;

            File.WriteAllText(directoryPath, JsonConvert.SerializeObject(serializedData, Formatting.Indented));
        }

        public static T LoadFromAsset<T>(string fileName) where T : IAssetSerializator, new()
        {
            var assetFileName = FindFileByType(fileName);

            if (assetFileName == null)
            {
                Console.WriteLine("Unsupported extenxtions type");
              //  return null;
            }

            string directoryPath = Path.Combine(assetFileName, fileName);
            if (!File.Exists(directoryPath)) Console.WriteLine("There is no that named file!");

            var dataString = File.ReadAllText(directoryPath);
            AssetSerializationData data = (AssetSerializationData)JsonConvert.DeserializeObject(dataString,typeof(AssetSerializationData));

            T newAsset = new T();
            return (T)newAsset.Deserializate(data);
        }

        public static string FindFileByType(string fileName)
        {
            string assetTypeName = fileName.Substring(fileName.IndexOf("."));

            switch (assetTypeName)
            {
                case ".mat": return SimPath.MaterialsPath;
                case ".texture": return SimPath.TexturesPath;
                case ".mesh": return SimPath.MeshesPath;
            }

            return null;
        }

        public static string GetAssetPathByType(string fileName)
        {
            var assetFileName = FindFileByType(fileName);
            string directoryPath = Path.Combine(assetFileName, fileName);
            return directoryPath;
        }
    }
}
