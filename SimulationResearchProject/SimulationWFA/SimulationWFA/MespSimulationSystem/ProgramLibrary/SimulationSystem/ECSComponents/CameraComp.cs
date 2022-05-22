using System.Numerics;
using MESPSimulationSystem.Math;
using RenderLibrary.DLL;
using RenderLibrary.IO;
using RenderLibrary.Transform;

namespace SimulationSystem.Components
{
    struct CameraComp
    {
        public float speed;
        public float zoom;
        public float near;
        public float far;

        public Mat4 view;
        public Mat4 projection;

        public Mat4 GetViewMatrix(Transform transform)
        {
            float[] camPos = { transform.position.X, transform.position.Y, transform.position.Z };
            float[] camFront = { transform.forward.X, transform.forward.Y, transform.forward.Z };
            float[] camUp = { transform.up.X, transform.up.Y, transform.up.Z };

            Mat4 mat4 = new Mat4();
            mat4.matrixAdress = RenderProgramDLL.LookAt(camPos, camFront, camUp);

            return mat4;
        }

        public void UpdateCameraZoom(double dy)
        {
            if (zoom >= 1.0f && zoom <= 45.0f)
            {
                zoom -= (float)dy;
            }
            else if (zoom < 1)
            {
                zoom = 1.0f;
            }
            else
            {
                zoom = 45.0f;
            }
        }

        public float GetZoom()
        {
            return zoom;
        }

        public Mat4 Perspective(float aspect)
        {
            Mat4 mat4 = new Mat4();
            mat4.matrixAdress = RenderProgramDLL.Perspective(zoom, aspect, near, far);
            return mat4;
        }

        public Vector3 RaycastFromCamera(Vector2 screenPos,Transform cameraTransform,Screen screen)
        {
            float[] screenPosArr = new float[] { screenPos.X, screenPos.Y };
            float[] resultArr = new float[3];

            float width = screen.GetWidth();
            float height = screen.GetHeight();

            RenderProgramDLL.GetRayFromScreenSpace(screenPosArr, Perspective(width / height).matrixAdress, GetViewMatrix(cameraTransform).matrixAdress, width, height, resultArr);

            return new Vector3(resultArr[0], resultArr[1], resultArr[2]);
        }

        public static Mat4 GetOrthographic()
        {
            Mat4 mat4 = new Mat4();
            mat4.matrixAdress = RenderProgramDLL.Orthographic();
            return mat4;
        }
    }
}
