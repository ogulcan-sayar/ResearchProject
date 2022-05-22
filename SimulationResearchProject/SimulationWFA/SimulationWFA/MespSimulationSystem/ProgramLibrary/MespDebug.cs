using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using MespSimulationSystem.Math;
using MESPSimulationSystem.Math;
using PhysicLibrary;
using RenderLibrary.Graphics;
using SimulationSystem;

namespace ProgramLibrary
{
    public static class MespDebug
    {

        public static void DrawLine(Vector3 from, Vector3 to, Vector3 color)
        {
            MespEditorDebugSystem.eventManager.SendEvent(new DrawLineEvent() {

                from = from,
                to = to,
                color = color,
            });
        }

        public static void DrawRay(Ray ray, float magnitude, Vector3 color)
        {
            ray.direction.Normalize();

            MespEditorDebugSystem.eventManager.SendEvent(new DrawLineEvent() {

                from = ray.origin,
                to = ray.origin+ ray.direction * magnitude,
                color = color,
            });
        }

        public static void DrawWireBox(Vector3 center, Vector3 size, Vector3 color)
        {
            BoxBounds bound = new BoxBounds();
            bound.Center = center;
            bound.Size = size;

            DrawWireBox(bound, color);
        }

        public static void DrawWireBox(BoxBounds bound, Vector3 color)
        {
            Vector3[] points = new Vector3[] {

                new Vector3(bound.xPoints.X, bound.yPoints.X, bound.zPoints.X),
                new Vector3(bound.xPoints.X, bound.yPoints.Y, bound.zPoints.X),
                new Vector3(bound.xPoints.Y, bound.yPoints.Y, bound.zPoints.X),
                new Vector3(bound.xPoints.Y, bound.yPoints.X, bound.zPoints.X),
                new Vector3(bound.xPoints.X, bound.yPoints.X, bound.zPoints.Y),
                new Vector3(bound.xPoints.X, bound.yPoints.Y, bound.zPoints.Y),
                new Vector3(bound.xPoints.Y, bound.yPoints.Y, bound.zPoints.Y),
                new Vector3(bound.xPoints.Y, bound.yPoints.X, bound.zPoints.Y),

            };

            int[] indices = new int[] {
                0,1,1,2,2,3,3,0,4,5,5,6,6,7,7,4,0,4,1,5,2,6,3,7
            };

            for(int i = 0; i < 24;)
            {
                MespEditorDebugSystem.eventManager.SendEvent(new DrawLineEvent() {

                    from = points[indices[i++]],
                    to = points[indices[i++]],
                    color = color,
                });
            }
        }

        public static void DrawWireBoxXZ(Vector3 center, Vector3 size, Vector3 color)
        {



            Vector3[] points = new Vector3[] {

                new Vector3(center.X - size.X/2, center.Y, center.Z - size.Z/2),
                new Vector3(center.X - size.X/2, center.Y, center.Z + size.Z/2),
                new Vector3(center.X + size.X/2, center.Y, center.Z + size.Z/2),
                new Vector3(center.X + size.X/2, center.Y, center.Z - size.Z/2),
            };

            int[] indices = new int[] {
                0,1,1,2,2,3,3,0
            };

            for (int i = 0; i < 8;)
            {
                MespEditorDebugSystem.eventManager.SendEvent(new DrawLineEvent() {

                    from = points[indices[i++]],
                    to = points[indices[i++]],
                    color = color,
                });
            }
        }

        public static void DrawCircle(Vector3 center, float radius,Vector3 normal,Vector3 direction, int nStep,Vector3 color)
        {
            float perRotation = (float)360 / nStep;
            Vector3[] positions = new Vector3[nStep];

            for (int i = 0; i < nStep; i++)
            {

                var pos = MathFunctions.Rotate(new Mat4(1), i * perRotation, normal, radius * direction);
                pos += center;
                positions[i] = (pos);
            }

            for (int i = 0; i < positions.Length - 1; i++)
            {
                MespEditorDebugSystem.eventManager.SendEvent(new DrawLineEvent() {

                    from = positions[i],
                    to = positions[i + 1],
                    color = color
                });
            }

            MespEditorDebugSystem.eventManager.SendEvent(new DrawLineEvent() {

                from = positions[positions.Length - 1],
                to = positions[0],
                color = color
            });
        }

        public static void DrawWireSphere(Vector3 center, float radius, int nStep)
        {

            DrawCircle(center, radius, new Vector3(1, 0, 0), new Vector3(0, 0, 1), nStep, new Vector3(0, 0, 1));
            DrawCircle(center, radius, new Vector3(0, 1, 0), new Vector3(1, 0, 0), nStep, new Vector3(1, 0, 0));
            DrawCircle(center, radius, new Vector3(0, 0, 1), new Vector3(0, 1, 0), nStep, new Vector3(0, 1, 0));

        }




    }
}
