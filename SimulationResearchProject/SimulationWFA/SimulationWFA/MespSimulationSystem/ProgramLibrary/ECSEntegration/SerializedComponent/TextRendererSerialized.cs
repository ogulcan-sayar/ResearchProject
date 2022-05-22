using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;

namespace TheSimulation.SerializedComponent
{
    public class TextRendererSerialized : SimulationSystem.ECS.Entegration.SerializedComponent
    {
        public Vector3 color;
        public Vector2 UIPosition;
        public float scale;
        public string text;

        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<TextRendererComp>() = new TextRendererComp() {
                color = color,
                UIPosition = UIPosition,
                scale = scale,
                text = text,
            };
        }

        public override string GetName()
        {
            return "Text Renderer Serialized";
        }
    }
}
