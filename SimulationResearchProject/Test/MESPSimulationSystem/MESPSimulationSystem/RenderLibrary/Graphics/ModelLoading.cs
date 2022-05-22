using System;
using RenderLibrary.DLL;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Graphics
{

    public class ModelLoading
    {
        private IntPtr modelAdress;

        public int meshCount;
        public int materialCount;


        public ModelLoading()
        {
        }

        public void LoadModel(string path)
        {
            modelAdress = RenderProgramDLL.LoadModel(path);
            meshCount = RenderProgramDLL.GetTotalMeshCount(modelAdress);
            materialCount = RenderProgramDLL.GetTotalMaterialCount(modelAdress);
        }

        public Mesh GetMesh(int idx)
        {
            Mesh mesh = new Mesh();
            mesh.SetMeshAdress(RenderProgramDLL.GetIdxMeshesFromModel(modelAdress, idx));
            return mesh;
        }

        public Material GetMaterial(int idx)
        {
            Material material = new Material();
            material.SetAdress(RenderProgramDLL.GetIdxMaterialFromModel(modelAdress, idx));
            return material;
        }
    }
}