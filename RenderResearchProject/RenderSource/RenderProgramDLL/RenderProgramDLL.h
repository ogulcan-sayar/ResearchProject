#ifndef _RENDER_PROGRAM_DLL_H_
#define _RENDER_PROGRAM_DLL_H_

#define _RENDER_PROGRAM_DLL_H_ extern "C" __declspec(dllexport)

#include"RenderProgram.h"
#include"Screen.h"
#include "Graphics/Rendering/Shader.h"
#include "Graphics/Objects/ModelLoader.h"
#include "IO/KeyboardInput.h"
#include "IO/MouseInput.h"
//#include "MeshRenderer.h"
#include "OpenGLFunctions/OpenGLFunctions.h"
#include "Animations/Animator.h"
#include "Graphics/Objects/Model.h"

#include"iostream"
#include <Graphics/Rendering/TextRenderer.h>
#include <LineRenderer.h>
#include <Graphics/Rendering/Cubemaps.h>


	//screen functions
	_RENDER_PROGRAM_DLL_H_ Screen* CreateScreen(int width, int height);
	_RENDER_PROGRAM_DLL_H_ void ScreenSetParameters(Screen* screen);
	_RENDER_PROGRAM_DLL_H_ bool ScreenShouldClose(Screen* screen);
	_RENDER_PROGRAM_DLL_H_ void ScreenUpdate(Screen* screen);
	_RENDER_PROGRAM_DLL_H_ void ScreenNewFrame(Screen* screen);
	_RENDER_PROGRAM_DLL_H_ void ScreenTerminate(Screen* screen);
	_RENDER_PROGRAM_DLL_H_ void ScreenProcessInput(Screen* screen);
	_RENDER_PROGRAM_DLL_H_ void SetClearColor(Screen* screen,float* clearColor);
	_RENDER_PROGRAM_DLL_H_ float ScreenGetWidth(Screen* screen);
	_RENDER_PROGRAM_DLL_H_ float ScreenGetHeight(Screen* screen);


	//shader functions
	_RENDER_PROGRAM_DLL_H_ Shader* NewShader(const char* vertexShaderPath, const char* fragShaderPath);
	_RENDER_PROGRAM_DLL_H_ void ShaderSetInt(Shader* shader, const char* name, int value);
	_RENDER_PROGRAM_DLL_H_ void ShaderSetFloat(Shader* shader, const char* name, float value);
	_RENDER_PROGRAM_DLL_H_ void ShaderSet3Float(Shader* shader, const char* name, float value, float value1, float value2);
	_RENDER_PROGRAM_DLL_H_ void ShaderSet4Float(Shader* shader, const char* name, float value, float value1, float value2, float value3);
	_RENDER_PROGRAM_DLL_H_ void ShaderSetBool(Shader* shader, const char* name, bool value);
	_RENDER_PROGRAM_DLL_H_ void ShaderSetMat4(Shader* shader, const char* name, glm::mat4* val);
	_RENDER_PROGRAM_DLL_H_ void ShaderActivate(Shader* shader);


	//Texture functions
	_RENDER_PROGRAM_DLL_H_ Texture* NewTexture(const char* directory, const char* name, int type);
	_RENDER_PROGRAM_DLL_H_ void TextureLoad(Texture* texture, bool flip);
	_RENDER_PROGRAM_DLL_H_ void TextureSetWrapParameters(Texture* texture, int wrapSParameter, int wrapTParameter);


	//Mesh functions
	_RENDER_PROGRAM_DLL_H_ Mesh* CreateMesh();
	_RENDER_PROGRAM_DLL_H_ void MeshSetVerticesPos(Mesh* mesh, float* pos, int sizeOfVertices);
	_RENDER_PROGRAM_DLL_H_ void MeshSetIndices(Mesh* mesh, int* indices);
	_RENDER_PROGRAM_DLL_H_ void MeshSetVerticesNormal(Mesh* mesh, float* normal);
	_RENDER_PROGRAM_DLL_H_ void MeshSetVerticesTexCoord(Mesh* mesh, float* texCoord);
	_RENDER_PROGRAM_DLL_H_ void MeshGetVerticesPos(Mesh* mesh, float* pos);
	_RENDER_PROGRAM_DLL_H_ void MeshGetIndices(Mesh* mesh, int* indices);
	_RENDER_PROGRAM_DLL_H_ void MeshGetVerticesNormal(Mesh* mesh, float* normal);
	_RENDER_PROGRAM_DLL_H_ void MeshGetVerticesTexCoord(Mesh* mesh, float* texCoord);

	
	//modelloader functions
	_RENDER_PROGRAM_DLL_H_ Model* LoadModel(const char* path);

	//model functions
	_RENDER_PROGRAM_DLL_H_ int GetModelChilCount(Model* model);
	_RENDER_PROGRAM_DLL_H_ int GetMeshCount(Model* model);
	_RENDER_PROGRAM_DLL_H_ int GetMaterialCount(Model* model);
	_RENDER_PROGRAM_DLL_H_ Mesh* GetIdxMeshesFromModel(Model* model, int idx);
	_RENDER_PROGRAM_DLL_H_ Material* GetIdxMaterialFromModel(Model* model, int idx);
	_RENDER_PROGRAM_DLL_H_ Model* GetChildModel(Model* model, int idx);
	_RENDER_PROGRAM_DLL_H_ int GetTotalMeshCount(Model* model);
	_RENDER_PROGRAM_DLL_H_ int GetTotalMaterialCount(Model* model);

	//animation functions
	_RENDER_PROGRAM_DLL_H_ Animation* GetAnimationFromPath(const char* animationPath, Model* model);
	_RENDER_PROGRAM_DLL_H_ Animator* NewAnimator(Animation* animation);
	_RENDER_PROGRAM_DLL_H_ void UpdateAnimation(Animator* animator, float dt);
	_RENDER_PROGRAM_DLL_H_ void SetBoneMatrixToShader(Animator* animator, Shader* shader);


	//input functions
	_RENDER_PROGRAM_DLL_H_ bool GetKeyDown(int keyCode);
	_RENDER_PROGRAM_DLL_H_ bool GetKeyUp(int keyCode);
	_RENDER_PROGRAM_DLL_H_ bool GetKey(int keyCode);
	_RENDER_PROGRAM_DLL_H_ bool GetMouseKeyDown(int keyCode);
	_RENDER_PROGRAM_DLL_H_ bool GetMouseKeyUp(int keyCode);
	_RENDER_PROGRAM_DLL_H_ bool GetMouseKey(int keyCode);
	_RENDER_PROGRAM_DLL_H_ double GetMouseX();
	_RENDER_PROGRAM_DLL_H_ double GetMouseY();
	_RENDER_PROGRAM_DLL_H_ double GetDx();
	_RENDER_PROGRAM_DLL_H_ double GetDy();
	_RENDER_PROGRAM_DLL_H_ double GetScrollDx();
	_RENDER_PROGRAM_DLL_H_ double GetScrollDy();


	//GLMatrix functions
	_RENDER_PROGRAM_DLL_H_ glm::mat4* ReturnMat4(float value);
	_RENDER_PROGRAM_DLL_H_ glm::mat3* ReturnMat3(float value);
	_RENDER_PROGRAM_DLL_H_ glm::mat4* ReturnMat4FromMat4(glm::mat4* mat4);
	

	//GLMath functions
	_RENDER_PROGRAM_DLL_H_ glm::mat4* LookAt(float* cameraPos, float* cameraFront, float* cameraUp);
	_RENDER_PROGRAM_DLL_H_ glm::mat4* Perspective(float fovy, float aspect, float near, float far);
	_RENDER_PROGRAM_DLL_H_ glm::mat4* Orthographic();
	_RENDER_PROGRAM_DLL_H_ void Rotate(glm::mat4* modelMatrix, float degree, float* axisOfRotation, float* newDirection);
	_RENDER_PROGRAM_DLL_H_ void GetRayFromScreenSpace(float* screenPos, glm::mat4 projectionMat, glm::mat4 viewMat, float width, float height, float* result);


	//Material functions
	_RENDER_PROGRAM_DLL_H_ Material* NewLitMaterial();
	_RENDER_PROGRAM_DLL_H_ void SetShaderToMaterial(Material* material, Shader* shader);
	_RENDER_PROGRAM_DLL_H_ Shader* GetShaderFromMaterial(Material* material);
	_RENDER_PROGRAM_DLL_H_ void SetAmbientToMaterial(LitMaterial* material,float* ambient);
	_RENDER_PROGRAM_DLL_H_ void SetDiffuseToMaterial(LitMaterial* material,float* diffuse);
	_RENDER_PROGRAM_DLL_H_ void SetSpecularToMaterial(LitMaterial* material,float* specular);
	_RENDER_PROGRAM_DLL_H_ void SetShininessToMaterial(LitMaterial* material,float shininess);
	_RENDER_PROGRAM_DLL_H_ void GetAmbientFromMaterial(LitMaterial* material, float* ambient);
	_RENDER_PROGRAM_DLL_H_ void GetDiffuseFromMaterial(LitMaterial* material, float* diffuse);
	_RENDER_PROGRAM_DLL_H_ void GetSpecularFromMaterial(LitMaterial* material, float* specular);
	_RENDER_PROGRAM_DLL_H_ float GetShininessFromMaterial(LitMaterial* material);
	_RENDER_PROGRAM_DLL_H_ void AddTextureToMaterial(LitMaterial* material,Texture* texture);
	_RENDER_PROGRAM_DLL_H_ Material* NewUnlitMaterial();
	_RENDER_PROGRAM_DLL_H_ void SetColorToMaterial(UnlitMaterial* material, float* color);
	_RENDER_PROGRAM_DLL_H_ void GetColorFromMaterial(UnlitMaterial* material, float* color);
	_RENDER_PROGRAM_DLL_H_ void AddTextureToUnlitMaterial(UnlitMaterial* material, Texture* texture);
	_RENDER_PROGRAM_DLL_H_ Texture* GetTextureFromUnlitMaterial(UnlitMaterial* material);
	_RENDER_PROGRAM_DLL_H_ void SetTransparent(Material* material, bool isTransparent);

	//MeshRenderer functions
	_RENDER_PROGRAM_DLL_H_ MeshRenderer* NewMeshRenderer();
	_RENDER_PROGRAM_DLL_H_ void SetMeshToMeshRenderer(MeshRenderer* meshRenderer, Mesh* mesh);
	_RENDER_PROGRAM_DLL_H_ void SetupMeshRenderer(MeshRenderer* meshRenderer);
	_RENDER_PROGRAM_DLL_H_ void RenderMeshRenderer(MeshRenderer* meshRenderer, Transform* transform, Material* material);
	_RENDER_PROGRAM_DLL_H_ void CleanUpMeshRenderer(MeshRenderer* meshRenderer);

	//Transform functions
	_RENDER_PROGRAM_DLL_H_ Transform* NewTransform();
	_RENDER_PROGRAM_DLL_H_ void SetTransformPosition(Transform* transform, float* pos);
	_RENDER_PROGRAM_DLL_H_ void SetTransformSize(Transform* transform, float* size);
	_RENDER_PROGRAM_DLL_H_ void SetTransformRotation(Transform* transform, float* rotation);

	//DepthTest functions
	_RENDER_PROGRAM_DLL_H_ void OpenGLEnable(int glEnum);
	_RENDER_PROGRAM_DLL_H_ void OpenGLDisable(int glEnum);
	_RENDER_PROGRAM_DLL_H_ void OpenGLClear(int glEnum);
	_RENDER_PROGRAM_DLL_H_ void OpenGLStencilMask(int mask);
	_RENDER_PROGRAM_DLL_H_ void OpenGLStencilFunc(int func, int ref, int mask);
	_RENDER_PROGRAM_DLL_H_ void OpenGLStencilOp(int sfail, int dpfail, int dppass);
	_RENDER_PROGRAM_DLL_H_ void OpenGLBlendFunc(int sfactor, int dfactor);
	_RENDER_PROGRAM_DLL_H_ void OpenGLDepthFunc(int func);

	//TextRenderer function
	_RENDER_PROGRAM_DLL_H_ TextRenderer* NewTextRenderer();
	_RENDER_PROGRAM_DLL_H_ void LoadFontToTextRenderer(TextRenderer* textRenderer, Texture* m_texture, int widthRes, int heightRes, int cellHeight, int cellWidth, int initialASCII);
	_RENDER_PROGRAM_DLL_H_ void SetupTextQuad(TextRenderer* textRenderer);
	_RENDER_PROGRAM_DLL_H_ void RenderText(TextRenderer* textRenderer, Shader& s, const char*, float x, float y, float scale, float* color);

	//LineRenderer functions
	_RENDER_PROGRAM_DLL_H_ LineRenderer* NewLineRenderer();
	_RENDER_PROGRAM_DLL_H_ void LineRendererSetup(LineRenderer* lineRenderer);
	_RENDER_PROGRAM_DLL_H_ void LineRendererSetNewPosition(LineRenderer* lineRenderer, float* from, float* to);
	_RENDER_PROGRAM_DLL_H_ void LineRendererSetNewColor(LineRenderer* lineRenderer, float* color);
	_RENDER_PROGRAM_DLL_H_ void LineRender(LineRenderer* lineRenderer, Shader& s, float lineWidth);
	_RENDER_PROGRAM_DLL_H_ void LineRendererCleanUp(LineRenderer* lineRenderer);

	//Skybox functions
	_RENDER_PROGRAM_DLL_H_ Cubemaps* NewCubemap();
	_RENDER_PROGRAM_DLL_H_ void LoadTextureToCubemap(Cubemaps* cubemaps,Mesh* mesh, const char** texturePaths);
	_RENDER_PROGRAM_DLL_H_ void RenderCubemap(Cubemaps* cubemaps);

#endif