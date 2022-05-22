using System;
using RenderLibrary.DLL;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Graphics
{//TODO: test amaçlı sonrasında ecs için entegre et
    public class MeshRenderer
    {
        private IntPtr meshRendererAdress;

        public MeshRenderer()
        {
            meshRendererAdress = RenderProgramDLL.NewMeshRenderer();
        }

        public void SetMesh(Mesh mesh)
        {
            RenderProgramDLL.SetMeshToMeshRenderer(meshRendererAdress, mesh.GetMeshAdress());
        }

        public void SetMaterial(Material material)
        {
            RenderProgramDLL.SetMaterialToMeshRenderer(meshRendererAdress, material.GetAdress());
        }
        
        //TODO: get fonksiyonlarını ayarla
     /*   public void GetMesh()
        {
            //TODO: RenderDLL.GetMeshFromRenderer();
        }

        public void GetMaterial()
        {
            //TODO: RenderDLL.GetMaterialFromRenderer();
        }*/

        public void Setup()
        {
            RenderProgramDLL.SetupMeshRenderer(meshRendererAdress);
        }

        public void Render(Transform.Transform transform)
        {
            RenderProgramDLL.RenderMeshRenderer(meshRendererAdress,transform.GetAdress());
        }

        public void CleanUp()
        {
            RenderProgramDLL.CleanUpMeshRenderer(meshRendererAdress);
        }
        
    }
}