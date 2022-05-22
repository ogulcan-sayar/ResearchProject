using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicLibrary
{
    public class ContactResolver
    {
        public int iteration;
        public int iterationUsed;

        public void ResolveContact(Contact[] contactArr, int numContacts, float time)
        {
            iterationUsed = 0;
            while (iterationUsed < iteration)
            {
                float max = 0;
                int maxIdx = numContacts;
                for (int i = 0; i < numContacts; i++)
                {
                    float seperateVel = contactArr[i].CalculateSeperateVelocity();
                    if (seperateVel < max)
                    {
                        max = seperateVel;
                        maxIdx = i;
                    }
                }

                if (maxIdx == numContacts) break;
                contactArr[maxIdx].Resolve(time);
                iterationUsed++;
            }
        }
    }
}
