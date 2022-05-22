using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;

namespace PhysicLibrary
{
    public static class DragForceGenerator
    {
        //constants k1 -> speed, k2-> squared velocity


        public static void UpdateDragForce(Particle particle)
        {
            Vector3 velocity = particle.velocity;
            if(velocity == Vector3.Zero)
            {
                return;
            }

            float dragCoeff = velocity.Length();
            dragCoeff = particle.drag * dragCoeff + particle.drag * dragCoeff * dragCoeff;

            velocity = velocity.normalized() * -dragCoeff;
            particle.AddForce(velocity);
        }
    }
}
