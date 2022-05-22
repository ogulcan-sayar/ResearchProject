using System;
using System.Numerics;
using MESPLibrary.MESPMath;
using MESPSimulation.DLL;


namespace MESPSimulation.IO
{
    public enum CameraDirection
    {
        NONE,
        FORWARD,
        BACKWARD,
        LEFT,
        RIGHT,
        UP,
        DOWN
    };

    public class Camera
    {
        public Vector3 cameraPos;

        public Vector3 cameraFront;
        public Vector3 cameraUp;
        public Vector3 cameraRight;
        public Vector3 worldUp;

        public float yaw;
        public float pitch;
        public float speed;
        public float zoom;

        public Camera(Vector3 position)
        {
            cameraPos = position;
            worldUp = new Vector3(0, 1, 0);
            yaw = -90f;
            pitch = 0;
            speed = 2.5f;
            zoom = 45f;
            cameraFront = new Vector3(0, 0, -1);
            UpdateCameraVectors();
            
        }

        public void UpdataCameraDirection(double dx, double dy)
        {
            yaw += (float)dx;
            pitch += (float)dy;

            if (pitch > 89.0f) {
                pitch = 89.0f;
            }
            else if (pitch < -89.0f) {
                pitch = -89.0f;
            }
            UpdateCameraVectors();
        }

        public void UpdateCameraPos(CameraDirection direction, double dt)
        {
            float velocity = (float)dt * speed;

            switch (direction)
            {
                case CameraDirection.NONE:
                    break;
                case CameraDirection.FORWARD:
                    cameraPos += cameraFront * velocity;
                    break;
                case CameraDirection.BACKWARD:
                    cameraPos -= cameraFront * velocity;
                    break;
                case CameraDirection.LEFT:
                    cameraPos -= cameraRight * velocity;
                    break;
                case CameraDirection.RIGHT:
                    cameraPos += cameraRight * velocity;
                    break;
                case CameraDirection.UP:
                    cameraPos += worldUp * velocity;
                    break;
                case CameraDirection.DOWN:
                    cameraPos -= worldUp * velocity;
                    break;
            }
        }

        public void UpdateCameraZoom(double dy)
        {
            if (zoom >= 1.0f && zoom <= 45.0f) {
                zoom -= (float)dy;
            }
            else if (zoom < 1) {
                zoom = 1.0f;
            }
            else {
                zoom = 45.0f;
            }
        }

        public float GetZoom()
        {
            return zoom;
        }

        public Mat4 GetViewMatrix()
        {
            float[] camPos =  {cameraPos.X, cameraPos.Y, cameraPos.Z};
            float[] camFront =  {cameraFront.X, cameraFront.Y, cameraFront.Z};
            float[] camUp = {cameraUp.X, cameraUp.Y, cameraUp.Z};

            Mat4 mat4 = new Mat4();
            mat4.matrixAdress = RenderProgramDLL.LookAt(camPos, camFront, camUp);
            
            return mat4;
        }
        
        public Mat4 Perspective(float aspect)
        {
            Mat4 mat4 = new Mat4();
            mat4.matrixAdress = RenderProgramDLL.Perspective(zoom, aspect, 0.1f, 100.0f);
            return mat4;
        }


        void UpdateCameraVectors()
        {
            Vector3 direction;
            direction.X = MathF.Cos((float)MathFunctions.ConvertToRadians(yaw)) * MathF.Cos((float)MathFunctions.ConvertToRadians(pitch));
            direction.Y = MathF.Sin((float)MathFunctions.ConvertToRadians(pitch));
            direction.Z = MathF.Sin((float)MathFunctions.ConvertToRadians(yaw)) * MathF.Cos((float)MathFunctions.ConvertToRadians(pitch));
            cameraFront = Vector3.Normalize(direction);

            cameraRight = Vector3.Normalize(Vector3.Cross(cameraFront, worldUp));
            cameraUp = Vector3.Normalize(Vector3.Cross(cameraRight, cameraFront));
        }
    }
}