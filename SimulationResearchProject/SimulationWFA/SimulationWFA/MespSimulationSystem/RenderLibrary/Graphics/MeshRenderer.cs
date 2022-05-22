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

        public void Setup()
        {
            RenderProgramDLL.SetupMeshRenderer(meshRendererAdress);
        }

        public void Render(Transform.Transform transform, Material material)
        {
            RenderProgramDLL.RenderMeshRenderer(meshRendererAdress,transform.GetAdress(),material.GetAdress());
        }

        public void CleanUp()
        {
            RenderProgramDLL.CleanUpMeshRenderer(meshRendererAdress);
        }
        
    }
}