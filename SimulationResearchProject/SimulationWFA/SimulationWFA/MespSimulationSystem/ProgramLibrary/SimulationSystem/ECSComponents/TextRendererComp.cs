using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RenderLibrary.Graphics;

namespace SimulationSystem.ECSComponents
{
    struct TextRendererComp
    {
        public TextRenderer textRenderer;

        public string text;
        public Vector3 color;
        public Vector2 UIPosition;
        public float scale;
    }
}
