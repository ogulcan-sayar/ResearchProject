using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationSystem.TimeUtils
{
    public struct Timer
    {
        float duration;
        float timer;
        bool firtCompleteFlag;

        public float NormalizedTime => Math.Min(1, timer / duration);

        public float NormalizedTimePingPong {
            get {
                float t = Math.Min(1, timer / duration);
                if (t < 0.5f)
                {
                    return t * 2;
                }
                return (1 - t) * 2;
            }
        }
        public bool Done => timer >= duration;
        public float Duration => duration;
        public float TimePassed => timer;

        public Timer(float duration)
        {
            this.duration = duration;
            timer = 0;
            firtCompleteFlag = false;
        }

        public void Restart()
        {
            timer = 0;
            firtCompleteFlag = false;
        }

        public void Restart(float newDuration)
        {
            duration = newDuration;
            Restart();
        }

        public void UpdateLooped(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= duration)
            {
                timer -= duration;
            }
        }

        /// returns true when completed
        public bool Update(float deltaTime)
        {
            if (timer >= duration)
            {
                return true;
            }

            timer += deltaTime;
            if (timer >= duration)
            {
                timer = duration;
            }
            return false;
        }

        public bool Update(float deltaTime, out bool isFirstComplete)
        {
            bool r = Update(deltaTime);
            isFirstComplete = false;
            if (r && !firtCompleteFlag)
            {
                isFirstComplete = true;
                firtCompleteFlag = true;
            }
            return r;
        }

        public void ForceComplete()
        {
            timer = duration;
        }

        public override string ToString()
        {
            return timer + "/" + duration;
        }
    }
}


