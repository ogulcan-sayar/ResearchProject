using System;
using System.Runtime.InteropServices;

namespace RenderLibrary.DLL
{
    class RenderProgramDLL
    {
        protected const string RenderProgramDLLPath = "RenderLibrary/RenderProgramDLL.dll";

        #region ScreenFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "CreateScreen")]
        public static extern IntPtr CreateScreen(int width, int height);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenShouldClose")]
        public static extern bool ScreenShouldClose(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenUpdate")]
        public static extern void ScreenUpdate(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenNewFrame")]
        public static extern void ScreenNewFrame(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenTerminate")]
        public static extern void ScreenTerminate(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenProcessInput")]
        public static extern void ScreenProcessInput(IntPtr screen);

        #endregion

        #region ShaderFunction

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewShader")]
        public static extern IntPtr NewShader(string vertexShaderPath, string fragmentShaderPath);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSetInt")]
        public static extern void ShaderSetInt(IntPtr shaderAdress, string name, int value);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSetFloat")]
        public static extern void ShaderSetFloat(IntPtr shaderAdress, string name, float value);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSet3Float")]
        public static extern void ShaderSet3Float(IntPtr shaderAdress, string name, float value, float value1, float value2);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSet4Float")]
        public static extern void ShaderSet4Float(IntPtr shaderAdress, string name, float value, float value1, float value2, float value3);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSetBool")]
        public static extern void ShaderSetBool(IntPtr shaderAdress, string name, bool value);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderSetMat4")]
        public static extern void ShaderSetMat4(IntPtr shaderAdress, string name, IntPtr mat4Adress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "ShaderActivate")]
        public static extern void ShaderActivate(IntPtr shaderAdress);
        
        #endregion

        #region TextureFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewTexture")]
        public static extern IntPtr NewTexture(string directory, string path, int aiType);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "TextureLoad")]
        public static extern IntPtr TextureLoad(IntPtr texture, bool flip);

        #endregion

        #region MeshFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "CreateMesh")]
        public static extern IntPtr CreateMesh();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetVerticesPos")]
        public static extern void MeshSetVerticesPos(IntPtr mesh, float[] pos, int sizeOfVertices);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetIndices")]
        public static extern void MeshSetIndices(IntPtr mesh, int[] indices);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetVerticesNormal")]
        public static extern void MeshSetVerticesNormal(IntPtr mesh, float[] normal);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshSetVerticesTexCoord")]
        public static extern void MeshSetVerticesTexCoord(IntPtr mesh, float[] texCoord);

        #endregion

        #region ModelFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "LoadModel")]
        public static extern IntPtr LoadModel(string path);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "GetTotalMeshCount")]
        public static extern int GetTotalMeshCount(IntPtr modelLoadingAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "GetTotalMaterialCount")]
        public static extern int GetTotalMaterialCount(IntPtr modelLoadingAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetIdxMeshesFromModel")]
        public static extern IntPtr GetIdxMeshesFromModel(IntPtr modelLoadingAdress, int idx);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetIdxMaterialFromModel")]
        public static extern IntPtr GetIdxMaterialFromModel(IntPtr modelLoadingAdress, int idx);


        #endregion

        #region InputFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetKeyDown")]
        public static extern bool GetKeyDown(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetKeyUp")]
        public static extern bool GetKeyUp(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetKey")]
        public static extern bool GetKey(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMouseKeyDown")]
        public static extern bool GetMouseKeyDown(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMouseKeyUp")]
        public static extern bool GetMouseKeyUp(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMouseKey")]
        public static extern bool GetMouseKey(int keyCode);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMouseX")]
        public static extern double GetMouseX();

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMouseY")]
        public static extern double GetMouseY();

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetDx")]
        public static extern double GetDx();

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetDy")]
        public static extern double GetDy();

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetScrollDx")]
        public static extern double GetScrollDx();

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetScrollDy")]
        public static extern double GetScrollDy();

        #endregion

        #region GLMatrixFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "ReturnMat4")]
        public static extern IntPtr ReturnMat4(float value);

        #endregion

        #region GLMathFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "LookAt")]
        public static extern IntPtr LookAt(float[] cameraPos, float[] cameraFront, float[] cameraUp);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "Perspective")]
        public static extern IntPtr Perspective(float fovy, float aspect, float near, float far);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "Rotate")]
        public static extern void Rotate(IntPtr modelMatrix, float degree, float[] axisOfRot, float[] newDirection);

        #endregion

        #region MaterialFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewLitMaterial")]
        public static extern IntPtr NewLitMaterial();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetShaderToMaterial")]
        public static extern void SetShaderToMaterial(IntPtr matAdress, IntPtr shader);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetAmbientToMaterial")]
        public static extern void SetAmbientToMaterial(IntPtr matAdress, float[] ambient);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetDiffuseToMaterial")]
        public static extern void SetDiffuseToMaterial(IntPtr matAdress, float[] diffuse);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetSpecularToMaterial")]
        public static extern void SetSpecularToMaterial(IntPtr matAdress, float[] specular);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetShininessToMaterial")]
        public static extern void SetShininessToMaterial(IntPtr matAdress, float shininess);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "AddTextureToMaterial")]
        public static extern void AddTextureToMaterial(IntPtr matAdress, IntPtr textureAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "NewUnlitMaterial")]
        public static extern IntPtr NewUnlitMaterial();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetColorToMaterial")]
        public static extern void SetColorToMaterial(IntPtr matAdress, float[] color);
        
        #endregion

        #region MeshRendererFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewMeshRenderer")]
        public static extern IntPtr NewMeshRenderer();

        [DllImport(RenderProgramDLLPath, EntryPoint = "SetMeshToMeshRenderer")]
        public static extern void SetMeshToMeshRenderer(IntPtr meshRendererAdress, IntPtr meshAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetMaterialToMeshRenderer")]
        public static extern void SetMaterialToMeshRenderer(IntPtr meshRendererAdress, IntPtr materialAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetupMeshRenderer")]
        public static extern void SetupMeshRenderer(IntPtr meshRendererAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "RenderMeshRenderer")]
        public static extern void RenderMeshRenderer(IntPtr meshRendererAdress, IntPtr transfromAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "CleanUpMeshRenderer")]
        public static extern void CleanUpMeshRenderer(IntPtr meshRendererAdress);
        
        #endregion

        
        #region TransformFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewTransform")]
        public static extern IntPtr NewTransform();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetTransformPosition")]
        public static extern void SetTransformPosition(IntPtr transformAdress, float[] pos);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetTransformSize")]
        public static extern void SetTransformSize(IntPtr transformAdress, float[] size);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetTransformRotation")]
        public static extern void SetTransformRotation(IntPtr transformAdress, float[] rotation);

        #endregion
    }
}
