using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenderLibrary.DLL;
using RenderLibrary.Graphics.Rendering;

namespace RenderLibrary.Animations
{
    public class Animator
    {
        private IntPtr animatorAdress;
        private IntPtr currentAnimationAdress;
        public Animator(Animation animation)
        {
            currentAnimationAdress = animation.GetAnimationAdress();
            animatorAdress = RenderProgramDLL.NewAnimator(currentAnimationAdress);
        }

        public void Update(float deltaTime)
        {
            RenderProgramDLL.UpdateAnimation(animatorAdress, deltaTime);
        }

        public void SetBoneMatrixToShader(Shader shader)
        {
            RenderProgramDLL.SetBoneMatrixToShader(animatorAdress, shader.GetShaderAdress());
        }

        public IntPtr GetAnimatorAdress()
        {
            return animatorAdress;
        }
    }
}
