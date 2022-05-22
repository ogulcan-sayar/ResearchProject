using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;
using SimulationSystem.TimeUtils;

namespace PhysicLibrary
{
    public class Particle
    {
        public Vector3 position;
        public Vector3 velocity;

        public Vector3 acceleration;
        public Vector3 oldAcc;

        protected float mass;
        protected float inverseMass;

        public float drag = 0.05f;

        
        public bool useGravity = true;
      
        public Vector3 totalForce;

        public Vector3 rotation;
        public Vector3 totalTorque;
        public Vector3 angularVelocity;
        public Vector3 inertiaTensor = Vector3.One;

        public Particle()
        {
            position = new Vector3();
            velocity = new Vector3();
            acceleration = new Vector3();
            SetMass(1);
        }

        public void SetMass(float mass, bool infinite = false)
        {
            if (infinite)
            {
                this.mass = float.MaxValue;
                inverseMass = 0;
                return;
            }

            this.mass = mass;
            inverseMass = 1 / mass;
        }

        public float GetInverseMass()
        {
            return inverseMass;
        }

        public float GetMass()
        {
            return mass;
        }

        public void Integrate(float time)
        {
            position += velocity * time;

            Vector3 resultingAcc = acceleration;
            resultingAcc += totalForce * inverseMass;
            oldAcc = resultingAcc;

            velocity += resultingAcc * time;
            velocity = velocity.Round(Physics.DecimalPrecision);
            //velocity *= Mathf.Pow(damping,time);

            ClearForceAccum();
        }

        public void ClearForceAccum()
        {
            totalForce = Vector3.Zero;
        }

        public void ApplyImpulse(Vector3 impulse)
        {
            totalForce += (impulse / Time.fixedDeltaTime);
        }

        public void AddTorque(Vector3 torque)
        {
            totalTorque += torque;
        }
        public void AddForce(Vector3 force)
        {
            totalForce += force;
        }
    }
}
