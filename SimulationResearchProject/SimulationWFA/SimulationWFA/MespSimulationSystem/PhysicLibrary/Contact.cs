using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;

namespace PhysicLibrary
{
    public class Contact
    {
        public Particle[] particles = new Particle[2];
        public float restitution;
        public Vector3 contactNormal;

        public float penetration;

        public Contact()
        {

        }

        public Contact(Particle particle, Particle otherParticle, Vector3 contactNormal, float penetration)
        {
            particles[0] = particle;
            particles[1] = otherParticle;
            this.contactNormal = contactNormal;
            this.penetration = penetration;
        }

        public void Resolve(float time)
        {
            ResolveVelocity(time);
            ResolveInterpenetration(time);
        }

        public float CalculateSeperateVelocity()
        {
            Vector3 relativeVelocity = particles[0].velocity;
            if (particles[1] != null) relativeVelocity -= particles[1].velocity;
            return Vector3.Dot(relativeVelocity, contactNormal);
        }

        void ResolveVelocity(float time)
        {
            
            float separatingVelocity = CalculateSeperateVelocity();

            // Check whether it needs to be resolved.
            if (separatingVelocity > 0)
            {
                // The contact is either separating or stationary - there’s
                // no impulse required.
                return;
            }

            // Calculate the new separating velocity.
            float newSepVelocity = -separatingVelocity * restitution;


            // Check the velocity build-up due to acceleration only.
            Vector3 accCausedVelocity = particles[0].oldAcc;
            if (particles[1] != null) accCausedVelocity -= particles[1].oldAcc;
            float accCausedSepVelocity = Vector3.Dot(accCausedVelocity, contactNormal) * time *10f;

            // If we’ve got a closing velocity due to acceleration build-up,
            // remove it from the new separating velocity.
            if (accCausedSepVelocity < 0)
            {
                newSepVelocity += restitution * accCausedSepVelocity;
                // Make sure we haven’t removed more than was
                // there to remove.
                if (newSepVelocity < 0) newSepVelocity = 0;
            }

            float deltaVelocity = newSepVelocity - separatingVelocity;

            float totalInverseMass = particles[0].GetInverseMass();
            if (particles[1] != null) totalInverseMass += particles[1].GetInverseMass();

            if (totalInverseMass <= 0) return;

            float impulse = deltaVelocity / totalInverseMass;
            Vector3 impulsePerIMass = contactNormal * impulse;

            impulsePerIMass = impulsePerIMass.Round(Physics.DecimalPrecision);

            particles[0].velocity += impulsePerIMass * particles[0].GetInverseMass();

            if (particles[1] != null)
            {
                particles[1].velocity += -impulsePerIMass * particles[1].GetInverseMass();
            }
        }

        void ResolveInterpenetration(float duration)
        {
            if (penetration <= 0) return;
            float totalInverseMass = particles[0].GetInverseMass();
            if (particles[1] != null) totalInverseMass += particles[1].GetInverseMass();

            if (totalInverseMass <= 0) return;

            Vector3 movePerIMass = contactNormal * (-penetration / totalInverseMass);
            particles[0].position -= movePerIMass * particles[0].GetInverseMass();
            if (particles[1] != null) particles[1].position += movePerIMass * particles[1].GetInverseMass();
        }

    }
}
