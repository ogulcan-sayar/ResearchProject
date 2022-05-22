using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;

namespace SimulationSystem.Components
{
    public struct TransformPositionArrowComp
    {
        public Mesh mesh;
        public Material material;
        public MeshRenderer meshRenderer;


        public bool SetMeshRenderer()
        {
            if (meshRenderer == null)
            {
                if (mesh == null || material == null) return false;
                meshRenderer = new MeshRenderer();
                meshRenderer.SetMesh(mesh);
                meshRenderer.Setup();
                return true;
            }
            return true;
        }

    }
}
