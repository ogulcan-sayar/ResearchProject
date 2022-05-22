using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RenderLibrary.DLL
{
    class RenderProgramDLL
    {

        protected const string RenderProgramDLLPath = "RenderProgramDLL.dll";

        #region ScreenFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "CreateScreen")]
        public static extern IntPtr CreateScreen(int width, int height);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenSetParameters")]
        public static extern void ScreenSetParameters(IntPtr screen);

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

        [DllImport(RenderProgramDLLPath, EntryPoint = "SetClearColor")]
        public static extern void SetClearColor(IntPtr screen, float[] clearColor);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenGetWidth")]
        public static extern float ScreenGetWidth(IntPtr screen);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ScreenGetHeight")]
        public static extern float ScreenGetHeight(IntPtr screen);

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

        [DllImport(RenderProgramDLLPath, EntryPoint = "TextureSetWrapParameters")]
        public static extern IntPtr TextureSetWrapParameters(IntPtr texture, int wrapSParameter, int wrapTParameter);


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

        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshGetVerticesPos")]
        public static extern void MeshGetVerticesPos(IntPtr mesh, float[] pos);

        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshGetIndices")]
        public static extern void MeshGetIndices(IntPtr mesh, int[] indices);

        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshGetVerticesNormal")]
        public static extern void MeshGetVerticesNormal(IntPtr mesh, float[] normal);

        [DllImport(RenderProgramDLLPath, EntryPoint = "MeshGetVerticesTexCoord")]
        public static extern void MeshGetVerticesTexCoord(IntPtr mesh, float[] texCoord);

        #endregion

        #region ModelLoader

        [DllImport(RenderProgramDLLPath, EntryPoint = "LoadModel")]
        public static extern IntPtr LoadModel(string path);

        #endregion

        #region ModelFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetModelChilCount")]
        public static extern int GetModelChildCount(IntPtr modelAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMeshCount")]
        public static extern int GetMeshCount(IntPtr modelAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetMaterialCount")]
        public static extern int GetMaterialCount(IntPtr modelAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetIdxMeshesFromModel")]
        public static extern IntPtr GetIdxMeshesFromModel(IntPtr modelAdress, int idx);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetIdxMaterialFromModel")]
        public static extern IntPtr GetIdxMaterialFromModel(IntPtr modelAdress, int idx);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetChildModel")]
        public static extern IntPtr GetChildModel(IntPtr modelAdress, int idx);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetTotalMeshCount")]
        public static extern int GetTotalMeshCount(IntPtr modelAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetTotalMaterialCount")]
        public static extern int GetTotalMaterialCount(IntPtr modelAdress);


        #endregion

        #region AnimationFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetAnimationFromPath")]
        public static extern IntPtr GetAnimationFromPath(string animationPath, IntPtr modelAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewAnimator")]
        public static extern IntPtr NewAnimator(IntPtr animationAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "UpdateAnimation")]
        public static extern void UpdateAnimation(IntPtr animationAdress, float deltaTime);

        [DllImport(RenderProgramDLLPath, EntryPoint = "SetBoneMatrixToShader")]
        public static extern void SetBoneMatrixToShader(IntPtr animationAdress, IntPtr shaderAdress);

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

        [DllImport(RenderProgramDLLPath, EntryPoint = "ReturnMat3")]
        public static extern IntPtr ReturnMat3(float value);

        [DllImport(RenderProgramDLLPath, EntryPoint = "ReturnMat4FromMat4")]
        public static extern IntPtr ReturnMat4FromMat4(IntPtr mat4Adress);

        #endregion

        #region GLMathunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "LookAt")]
        public static extern IntPtr LookAt(float[] cameraPos, float[] cameraFront, float[] cameraUp);

        [DllImport(RenderProgramDLLPath, EntryPoint = "Perspective")]
        public static extern IntPtr Perspective(float fovy, float aspect, float near, float far);

        [DllImport(RenderProgramDLLPath, EntryPoint = "Orthographic")]
        public static extern IntPtr Orthographic();

        [DllImport(RenderProgramDLLPath, EntryPoint = "Rotate")]
        public static extern void Rotate(IntPtr modelMatrix, float degree, float[] axisOfRot, float[] newDirection);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetRayFromScreenSpace")]
        public static extern void GetRayFromScreenSpace(float[] screenPos, IntPtr projectionMat, IntPtr viewMat, float width, float height, float[] result);

        #endregion

        #region MaterialFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewLitMaterial")]
        public static extern IntPtr NewLitMaterial();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetShaderToMaterial")]
        public static extern void SetShaderToMaterial(IntPtr matAdress, IntPtr shader);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetShaderFromMaterial")]
        public static extern IntPtr GetShaderFromMaterial(IntPtr matAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "SetAmbientToMaterial")]
        public static extern void SetAmbientToMaterial(IntPtr matAdress, float[] ambient);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetDiffuseToMaterial")]
        public static extern void SetDiffuseToMaterial(IntPtr matAdress, float[] diffuse);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetSpecularToMaterial")]
        public static extern void SetSpecularToMaterial(IntPtr matAdress, float[] specular);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetShininessToMaterial")]
        public static extern void SetShininessToMaterial(IntPtr matAdress, float shininess);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetAmbientFromMaterial")]
        public static extern void GetAmbientFromMaterial(IntPtr matAdress, float[] ambient);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetDiffuseFromMaterial")]
        public static extern void GetDiffuseFromMaterial(IntPtr matAdress, float[] diffuse);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetSpecularFromMaterial")]
        public static extern void GetSpecularFromMaterial(IntPtr matAdress, float[] specular);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetShininessFromMaterial")]
        public static extern float GetShininessFromMaterial(IntPtr matAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "AddTextureToMaterial")]
        public static extern void AddTextureToMaterial(IntPtr matAdress, IntPtr textureAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "NewUnlitMaterial")]
        public static extern IntPtr NewUnlitMaterial();
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetColorToMaterial")]
        public static extern void SetColorToMaterial(IntPtr matAdress, float[] color);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetColorFromMaterial")]
        public static extern void GetColorFromMaterial(IntPtr matAdress, float[] color);

        [DllImport(RenderProgramDLLPath, EntryPoint = "AddTextureToUnlitMaterial")]
        public static extern void AddTextureToUnlitMaterial(IntPtr matAdress, IntPtr textureAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "GetTextureFromUnlitMaterial")]
        public static extern IntPtr GetTextureFromUnlitMaterial(IntPtr matAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "SetTransparent")]
        public static extern void SetTransparent(IntPtr matAdress, bool isTransparent);

        #endregion

        #region MeshRendererFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewMeshRenderer")]
        public static extern IntPtr NewMeshRenderer();

        [DllImport(RenderProgramDLLPath, EntryPoint = "SetMeshToMeshRenderer")]
        public static extern void SetMeshToMeshRenderer(IntPtr meshRendererAdress, IntPtr meshAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "SetupMeshRenderer")]
        public static extern void SetupMeshRenderer(IntPtr meshRendererAdress);
        
        [DllImport(RenderProgramDLLPath, EntryPoint = "RenderMeshRenderer")]
        public static extern void RenderMeshRenderer(IntPtr meshRendererAdress, IntPtr transfromAdress, IntPtr materialAdress);
        
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

        #region OpenGLFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLEnable")]
        public static extern void OpenGLEnable(int glEnum);

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLDisable")]
        public static extern void OpenGLDisable(int glEnum);

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLClear")]
        public static extern void OpenGLClear(int glEnum);

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLStencilMask")]
        public static extern void OpenGLStencilMask(int mask);

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLStencilFunc")]
        public static extern void OpenGLStencilFunc(int func, int refValue, int mask);

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLStencilOp")]
        public static extern void OpenGLStencilOp(int sfail, int dpfail, int dppass);

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLCheckStencil")]
        public static extern void OpenGLCheckStencil();

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLBlendFunc")]
        public static extern void OpenGLBlendFunc(int sfactor, int dfactor);

        [DllImport(RenderProgramDLLPath, EntryPoint = "OpenGLDepthFunc")]
        public static extern void OpenGLDepthFunc(int func);

        #endregion

        #region TextRendererFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewTextRenderer")]
        public static extern IntPtr NewTextRenderer();

        [DllImport(RenderProgramDLLPath, EntryPoint = "LoadFontToTextRenderer")]
        public static extern void LoadFontToTextRenderer(IntPtr textRendererAdress, IntPtr textureAdress, int widthRes, int heightRes, int cellHeight, int cellWidth, int initialASCII);

        [DllImport(RenderProgramDLLPath, EntryPoint = "SetupTextQuad")]
        public static extern void SetupTextQuad(IntPtr textRendererAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "RenderText")]
        public static extern void RenderText(IntPtr textRendererAdress, IntPtr shader, string text, float x, float y, float scale, float[] color);

        #endregion

        #region LineRendererFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewLineRenderer")]
        public static extern IntPtr NewLineRenderer();

        [DllImport(RenderProgramDLLPath, EntryPoint = "LineRendererSetup")]
        public static extern void LineRendererSetup(IntPtr lineRendererAdress);

        [DllImport(RenderProgramDLLPath, EntryPoint = "LineRendererSetNewPosition")]
        public static extern void LineRendererSetNewPosition(IntPtr lineRendererAdress, float[] from, float[] to);

        [DllImport(RenderProgramDLLPath, EntryPoint = "LineRendererSetNewColor")]
        public static extern void LineRendererSetNewColor(IntPtr lineRendererAdress, float[] color);

        [DllImport(RenderProgramDLLPath, EntryPoint = "LineRender")]
        public static extern void LineRender(IntPtr lineRendererAdress, IntPtr shaderAdress,float lineWidth);

        [DllImport(RenderProgramDLLPath, EntryPoint = "LineRendererCleanUp")]
        public static extern void LineRendererCleanUp(IntPtr textRendererAdress);


        #endregion

        #region CubemagpFunctions

        [DllImport(RenderProgramDLLPath, EntryPoint = "NewCubemap")]
        public static extern IntPtr NewCubemap();

        [DllImport(RenderProgramDLLPath, EntryPoint = "LoadTextureToCubemap")]
        public static extern void LoadTextureToCubemap(IntPtr cubemapAdress,IntPtr meshAdress, string[] texturePaths);

        [DllImport(RenderProgramDLLPath, EntryPoint = "RenderCubemap")]
        public static extern void RenderCubemap(IntPtr cubemapAdress);

        #endregion

    }
}
