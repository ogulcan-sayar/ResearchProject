using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenderLibrary.DLL;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Graphics.RenderData
{
    public class Model
    {
        protected IntPtr modelAdress;

        public Model(IntPtr modelAdress)
        {
            this.modelAdress = modelAdress;
        }

        public int ModelChildCount()
        {
            return RenderProgramDLL.GetModelChildCount(modelAdress);
        }

        public int ModelMeshCount()
        {
            return RenderProgramDLL.GetMeshCount(modelAdress);
        }

        public int ModelMaterialCount()
        {
            return RenderProgramDLL.GetMaterialCount(modelAdress);
        }

        public Model GetChildModel(int idx)
        {
            return new Model(RenderProgramDLL.GetChildModel(modelAdress, idx));
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

        public int GetTotalMeshCount()
        {
            return RenderProgramDLL.GetTotalMeshCount(modelAdress);
        }

        public int GetTotalMaterialCount()
        {
            return RenderProgramDLL.GetTotalMaterialCount(modelAdress);
        }

        public IntPtr GetModelAdress()
        {
            return modelAdress;
        }
    }
}
