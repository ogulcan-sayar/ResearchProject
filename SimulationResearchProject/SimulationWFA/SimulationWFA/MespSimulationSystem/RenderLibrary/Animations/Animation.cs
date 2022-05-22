using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenderLibrary.DLL;
using RenderLibrary.Graphics;
using RenderLibrary.Graphics.RenderData;

namespace RenderLibrary.Animations
{
    public class Animation
    {
        private IntPtr animationAdress;

        public Animation(string animationPath, Model model)
        {
            animationAdress = RenderProgramDLL.GetAnimationFromPath(animationPath, model.GetModelAdress());
        }

        public IntPtr GetAnimationAdress()
        {
            return animationAdress;
        }

    }
}
