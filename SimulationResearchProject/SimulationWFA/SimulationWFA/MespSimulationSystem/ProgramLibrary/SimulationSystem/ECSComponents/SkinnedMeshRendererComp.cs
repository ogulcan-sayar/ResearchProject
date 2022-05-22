using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;

namespace SimulationSystem.ECSComponents
{
    public struct SkinnedMeshRendererComp
    {
        public Model rootModel;

        public Mesh[] mesh;
        public Material[] material;
        public MeshRenderer[] meshRenderer;

        private int counter;
        

        public void SetMeshRenderers(World world, ref SimObject rootSimObj)
        {
            mesh = new Mesh[rootModel.GetTotalMeshCount()];
            material = new Material[rootModel.GetTotalMaterialCount()];
            meshRenderer = new MeshRenderer[rootModel.GetTotalMeshCount()];
            counter = 0;

            SetupModel(world, rootModel, ref rootSimObj);
        }

        private void SetupModel(World world,Model rootModel, ref SimObject rootSimObj)
        {
            int meshCount = rootModel.ModelMeshCount();

            SimObject[] meshSimObj = new SimObject[meshCount];

            for (int i = 0; i < meshCount; i++)
            {
                meshSimObj[i] = SimObject.NewSimObject();
                meshSimObj[i].CreateEntity(world);
                meshSimObj[i].InjectAllSerializedComponents(world);
                meshSimObj[i].SetParent(rootSimObj);

                mesh[counter] = rootModel.GetMesh(i);
                material[counter] = rootModel.GetMaterial(i);
                material[counter].SetShader(ShaderPool.GetShaderByType(ShaderPool.ShaderType.LitShader));

                meshRenderer[counter] = new MeshRenderer();
                meshRenderer[counter].SetMesh(mesh[counter]);
                meshRenderer[counter].Setup();
                counter++;
            }

            int childCount = rootModel.ModelChildCount();
            for (int i = 0; i < childCount; i++)
            {
                if (meshCount > 0) SetupModel(world,rootModel.GetChildModel(i), ref meshSimObj[0]);
                else SetupModel(world, rootModel.GetChildModel(i), ref rootSimObj);

            }
        }
    }
}
