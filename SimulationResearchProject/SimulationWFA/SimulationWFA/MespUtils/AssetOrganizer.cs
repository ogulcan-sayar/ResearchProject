using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramLibrary;

namespace SimulationWFA.MespUtils
{
    public static class AssetOrganizer
    {
        
        public static string GetTexturePathByFileID(string id)
        {
            string[] files = Directory.GetFiles(SimPath.GetAssetPath, "*.texture", SearchOption.AllDirectories);

            foreach(var file in files)
            {
                var context = File.ReadAllText(file);
                var uniqeID = context.Substring(context.IndexOf("uniqueFileID") +16, 36);
                if (id == uniqeID) 
                {
                    FileInfo fileInfo = new FileInfo(file);

                    return fileInfo.Name; 
                }
            }

            Console.WriteLine("There is no file which has that ID");
            return null;
        }

        public static string GetMaterialPathByFileID(string id)
        {
            string[] files = Directory.GetFiles(SimPath.GetAssetPath, "*.mat", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var context = File.ReadAllText(file);
                var uniqeID = context.Substring(context.IndexOf("uniqueFileID") + 16, 36);
                if (id == uniqeID)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    return fileInfo.Name;
                }
            }

            Console.WriteLine("There is no file which has that ID");
            return null;
        }

        public static string GetMeshPathByFileID(string id)
        {
            string[] files = Directory.GetFiles(SimPath.GetAssetPath, "*.mesh", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var context = File.ReadAllText(file);
                var uniqeID = context.Substring(context.IndexOf("uniqueFileID") + 16, 36);
                if (id == uniqeID)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    return fileInfo.Name;
                }
            }

            Console.WriteLine("There is no file which has that ID");
            return null;
        }

    }
}
