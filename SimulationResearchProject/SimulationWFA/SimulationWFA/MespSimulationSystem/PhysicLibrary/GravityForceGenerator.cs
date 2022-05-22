using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicLibrary
{
    static class GravityForceGenerator
    {
        public static void ApplyGravity(Particle particle)
        {
            if (!particle.useGravity) return;
            if (particle.GetInverseMass() == 0) return;
            particle.AddForce(Physics.Gravity * particle.GetMass());

        }
    }
}
