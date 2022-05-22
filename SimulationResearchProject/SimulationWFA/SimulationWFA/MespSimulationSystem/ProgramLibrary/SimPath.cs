using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLibrary
{
    public static class SimPath
    {
        public static string currentDirectory = Directory.GetCurrentDirectory();
        private static string AssetPath = null;
        public static string ImagesPath = Path.Combine(GetAssetPath,"Images");
        public static string MaterialsPath = Path.Combine(GetAssetPath, "Materials");
        public static string MeshesPath = Path.Combine(GetAssetPath, "Meshes");
        public static string ModelsPath = Path.Combine(GetAssetPath, "Models");
        public static string ShadersPath = Path.Combine(GetAssetPath, "Shaders");
        public static string TexturesPath = Path.Combine(GetAssetPath, "Textures");
        public static string ScenesPath = Path.Combine(GetAssetPath, "Scenes");

        public static string GetAssetPath { 
            get {
                if(AssetPath == null)
                {
                    AssetPath = FindDirectoryPath("Assets");
                }
                return AssetPath;
            } }

        public static string FindDirectoryPath(string directoryName)
        {
            DirectoryInfo d = new DirectoryInfo(currentDirectory);
            int controlCounter = 0;

            while (true && controlCounter < 10)
            {
                foreach (var directory in d.Parent.GetDirectories())
                {
                    if (directory.Name == directoryName)
                    {
                        return directory.FullName;
                    }
                }
                d = d.Parent;
                controlCounter++;
            }
            

            Console.WriteLine("Asset pahti bulunamadı!");
            return null;
        }
    }
}
