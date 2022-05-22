#include "RenderProgramDLL.h"

#pragma region ScreenFunctions

Screen* CreateScreen(int width, int height)
{
	Screen* screen = new Screen(width, height);

	screen->ConfigureGLFW();

	if (!screen->Initialize())
	{
		//return;
	}

	// glad: load all OpenGL function pointers
	if (screen->CheckGladInitialization())
	{
		//return;
	}

	return screen;
}

_RENDER_PROGRAM_DLL_H_ void ScreenSetParameters(Screen* screen)
{
	screen->SetParameters();
}

bool ScreenShouldClose(Screen* screen)
{
	return screen->ShouldClose();
}

void ScreenUpdate(Screen* screen)
{
	screen->Update();
}

void ScreenNewFrame(Screen* screen)
{
	screen->NewFrame();
}

void ScreenTerminate(Screen* screen)
{
	screen->Terminate();
}

void ScreenProcessInput(Screen* screen)
{
	screen->ProcessInput();
}

void SetClearColor(Screen* screen, float* clearColor)
{
	screen->clearColor = glm::vec4(clearColor[0], clearColor[1], clearColor[2], clearColor[3]);
}

float ScreenGetWidth(Screen* screen)
{
	return screen->SCR_WIDTH;
}

float ScreenGetHeight(Screen* screen)
{
	return screen->SCR_HEIGHT;
}

#pragma endregion

#pragma region ShaderFunction

Shader* NewShader(const char* vertexShaderPath, const char* fragShaderPath)
{
	Shader* shader = new Shader(vertexShaderPath, fragShaderPath);
	return shader;
}

void ShaderSetInt(Shader* shader, const char* name, int value)
{
	glUniform1i(glGetUniformLocation(shader->id, name), value);
}

void ShaderSetFloat(Shader* shader, const char* name, float value)
{
	glUniform1f(glGetUniformLocation(shader->id, name), value);
}

void ShaderSet3Float(Shader* shader, const char* name, float value, float value1, float value2)
{
	glUniform3f(glGetUniformLocation(shader->id, name), value, value1, value2);
}

void ShaderSet4Float(Shader* shader, const char* name, float value, float value1, float value2, float value3)
{
	glUniform4f(glGetUniformLocation(shader->id, name), value, value1, value2, value3);
}

void ShaderSetBool(Shader* shader, const char* name, bool value)
{
	glUniform1i(glGetUniformLocation(shader->id, name), (int)value);
}

void ShaderSetMat4(Shader* shader, const char* name, glm::mat4* val)
{
	glUniformMatrix4fv(glGetUniformLocation(shader->id, name), 1, GL_FALSE, glm::value_ptr(*val));
}

void ShaderActivate(Shader* shader)
{
	if (shader != NULL) {
		shader->Activate();
	}
}



#pragma endregion

#pragma region TextureFunctions

Texture* NewTexture(const char* directory, const char* name, int type)
{
	Texture* texture = new Texture(directory, name, (aiTextureType)type);
	return texture;
}

void TextureLoad(Texture* texture, bool flip)
{
	texture->Load(flip);
}

void TextureSetWrapParameters(Texture* texture, int wrapSParameter, int wrapTParameter)
{
	texture->SetWrapParameters(wrapSParameter, wrapTParameter);
}


#pragma endregion

#pragma region MeshFunctions
Mesh* CreateMesh()
{
	Mesh* mesh = new Mesh();
	return mesh;
}

void MeshSetVerticesPos(Mesh* mesh, float* pos, int sizeOfVertices)
{
	mesh->verticesSize = sizeOfVertices;

	std::vector<Vertex> ret(sizeOfVertices);

	for (int i = 0; i < sizeOfVertices; i++)
	{
		ret[i].pos = glm::vec3(
			pos[i * 3 + 0],
			pos[i * 3 + 1],
			pos[i * 3 + 2]
		);
	}

	mesh->vertices = ret;
}

void MeshSetIndices(Mesh* mesh, int* _indices)
{
	std::vector<unsigned int> ret(mesh->verticesSize);
	for (unsigned int i = 0; i < mesh->verticesSize; i++) {
		ret[i] = _indices[i];
	}
	mesh->indices = ret;
}

void MeshSetVerticesNormal(Mesh* mesh, float* normal)
{
	for (int i = 0; i < mesh->verticesSize; i++)
	{
		mesh->vertices[i].normal = glm::vec3(
			normal[i * 3 + 0],
			normal[i * 3 + 1],
			normal[i * 3 + 2]
		);
	}
}

void MeshSetVerticesTexCoord(Mesh* mesh, float* texCoord)
{
	for (int i = 0; i < mesh->verticesSize; i++)
	{
		mesh->vertices[i].texCoord = glm::vec2(
			texCoord[i * 2 + 0],
			texCoord[i * 2 + 1]
		);
	}
}

void MeshGetVerticesPos(Mesh* mesh, float* pos)
{
	for (int i = 0;i< mesh->vertices.size(); i++) {
		pos[i*3+0] = mesh->vertices[i].pos.x;
		pos[i*3+1] = mesh->vertices[i].pos.y;
		pos[i*3+2] = mesh->vertices[i].pos.z;
	}
}

void MeshGetIndices(Mesh* mesh, int* indices)
{
	for (unsigned int i = 0; i < mesh->verticesSize; i++) {
		indices[i] = mesh->indices[i];
	}
}

void MeshGetVerticesNormal(Mesh* mesh, float* normal)
{
	for (int i = 0; i < mesh->vertices.size(); i++) {
		normal[i * 3 + 0] = mesh->vertices[i].normal.x;
		normal[i * 3 + 1] = mesh->vertices[i].normal.y;
		normal[i * 3 + 2] = mesh->vertices[i].normal.z;
	}
}

void MeshGetVerticesTexCoord(Mesh* mesh, float* texCoord)
{
	for (int i = 0; i < mesh->vertices.size(); i++) {
		texCoord[i * 2 + 0] = mesh->vertices[i].texCoord.x;
		texCoord[i * 2 + 1] = mesh->vertices[i].texCoord.y;
	}
}

#pragma endregion


#pragma region ModelLoaderFunctions

Model* LoadModel(const char* path)
{
	ModelLoader* modelLoader = new ModelLoader();
	Model* model = modelLoader->LoadModel(path);
	return model;
}

#pragma endregion

#pragma region ModelFunctions

int GetModelChilCount(Model* model)
{
	return model->GetChildCount();
}

int GetMeshCount(Model* model)
{
	return model->GetMeshCount();
}

int GetMaterialCount(Model* model)
{
	return model->GetMaterialCount();
}

Mesh* GetIdxMeshesFromModel(Model* model, int idx)
{
	return model->GetMesh(idx);
}

Material* GetIdxMaterialFromModel(Model* model, int idx)
{
	return model->GetMaterial(idx);
}

Model* GetChildModel(Model* model, int idx)
{
	return model->GetChildModel(idx);
}

int GetTotalMeshCount(Model* model)
{
	return model->totalMeshCount;
}

int GetTotalMaterialCount(Model* model)
{
	return model->totalMaterialCount;
}

#pragma endregion

#pragma region AnimationFunctions

Animation* GetAnimationFromPath(const char* animationPath, Model* model)
{
	Animation* animation = new Animation(animationPath, model);
	return animation;
}

Animator* NewAnimator(Animation* animation)
{
	Animator* animator = new Animator(animation);
	return animator;
}

void UpdateAnimation(Animator* animator, float dt)
{
	animator->UpdateAnimation(dt);
}

void SetBoneMatrixToShader(Animator* animator, Shader* shader)
{
	shader->Activate();
	auto transforms = animator->GetFinalBoneMatrices();
	for (int i = 0; i < transforms.size(); ++i)
		shader->SetMat4("finalBonesMatrices[" + std::to_string(i) + "]", transforms[i]);
}


#pragma endregion


#pragma region InputFunctions

bool GetKeyDown(int keyCode)
{
	return KeyboardInput::KeyWentDown(keyCode);
}

bool GetKeyUp(int keyCode)
{
	return KeyboardInput::KeyWentUp(keyCode);
}

bool GetKey(int keyCode)
{
	return KeyboardInput::Key(keyCode);
}

bool GetMouseKeyDown(int keyCode)
{
	return MouseInput::ButtonWentDown(keyCode);
}

bool GetMouseKeyUp(int keyCode)
{
	return MouseInput::ButtonWentUp(keyCode);
}

bool GetMouseKey(int keyCode)
{
	return MouseInput::Button(keyCode);
}

double GetMouseX()
{
	return MouseInput::GetMouseX();
}

double GetMouseY()
{
	return  MouseInput::GetMouseY();
}

double GetDx()
{
	return MouseInput::GetDx();
}

double GetDy()
{
	return MouseInput::GetDy();
}

double GetScrollDx()
{
	return MouseInput::GetScrollDx();
}

double GetScrollDy()
{
	return MouseInput::GetScrollDy();
}


#pragma endregion

#pragma region GLMatrixFunctions

glm::mat4* ReturnMat4(float value)
{
	glm::mat4* mat4 = new glm::mat4(value);
	return mat4;
}

glm::mat3* ReturnMat3(float value)
{
	glm::mat3* mat3 = new glm::mat3(value);
	return mat3;
}

glm::mat4* ReturnMat4FromMat4(glm::mat4* mat4)
{
	return new glm::mat4(glm::mat3(*mat4));
}



#pragma endregion

#pragma region GLMathFunction

glm::mat4* LookAt(float* cameraPos, float* cameraFront, float* cameraUp)
{
	glm::vec3 camPos = glm::vec3(cameraPos[0], cameraPos[1], cameraPos[2]);
	glm::mat4* mat4 = new glm::mat4(glm::lookAt(camPos, camPos + glm::vec3(cameraFront[0], cameraFront[1], cameraFront[2]), glm::vec3(cameraUp[0], cameraUp[1], cameraUp[2])));
	return mat4;
}

glm::mat4* Perspective(float fovy, float aspect, float near, float far)
{
	glm::mat4* mat4 = new glm::mat4(glm::perspective(glm::radians(fovy), aspect, near, far));
	return mat4;
}

glm::mat4* Orthographic()
{
	glm::mat4* mat4 = new glm::mat4(glm::ortho(0.0f, 800.0f, 0.0f, 600.0f));
	return mat4;
}

void Rotate(glm::mat4* modelMatrix, float degree, float* axisOfRotation, float* newDirection) {

	glm::vec3 rotate = glm::vec3(glm::rotate(*modelMatrix, glm::radians(degree), glm::vec3(axisOfRotation[0], axisOfRotation[1], axisOfRotation[2])) * glm::vec4(newDirection[0], newDirection[1], newDirection[2], 1.0f));
	newDirection[0] = rotate.x;
	newDirection[1] = rotate.y;
	newDirection[2] = rotate.z;
}

void GetRayFromScreenSpace(float* screenPos, glm::mat4 projectionMat, glm::mat4 viewMat, float width, float height, float* result)
{
	glm::vec2 screenPosVec = glm::vec2(screenPos[0], screenPos[1]);

	float x = (2.0f * screenPosVec.x) / width - 1.0f;
	float y = 1.0f - (2.0f * screenPosVec.y) / height;
	float z = 1.0f;
	glm::vec3 ray_nds = glm::vec3(x, y, z);

	glm::vec4 ray_clip = glm::vec4(ray_nds.x,ray_nds.y, -1.0, 1.0);

	glm::vec4 ray_eye = glm::inverse(projectionMat) * ray_clip;
	ray_eye = glm::vec4(ray_eye.x,ray_eye.y, -1.0, 0.0);

	glm::vec3 ray_wor = (glm::inverse(viewMat) * ray_eye);
	// don't forget to normalise the vector at some point
	ray_wor = glm::normalize(ray_wor);

	result[0] = ray_wor.x;
	result[1] = ray_wor.y;
	result[2] = ray_wor.z;
}


#pragma endregion

#pragma region MaterialFunctions

Material* NewLitMaterial()
{
	LitMaterial* litMaterial = new LitMaterial();
	return litMaterial;
}

void SetShaderToMaterial(Material* material, Shader* shader)
{
	material->shader = shader;
}

Shader* GetShaderFromMaterial(Material* material)
{
	return material->shader;
}

void SetAmbientToMaterial(LitMaterial* material, float* ambient)
{
	material->ambient = glm::vec4(ambient[0], ambient[1], ambient[2], ambient[3]);
}

void SetDiffuseToMaterial(LitMaterial* material, float* diffuse)
{
	material->diffuse = glm::vec4(diffuse[0], diffuse[1], diffuse[2], diffuse[3]);
}

void SetSpecularToMaterial(LitMaterial* material, float* specular)
{
	material->specular = glm::vec4(specular[0], specular[1], specular[2], specular[3]);
}

void SetShininessToMaterial(LitMaterial* material, float shininess)
{
	material->shininess = shininess;
}

void GetAmbientFromMaterial(LitMaterial* material, float* ambient)
{
	for (int i = 0; i < 4; i++) {
		ambient[i] = material->ambient[i];
	}
}

void GetDiffuseFromMaterial(LitMaterial* material, float* diffuse)
{
	for (int i = 0; i < 4; i++) {
		diffuse[i] = material->diffuse[i];
	}
}

void GetSpecularFromMaterial(LitMaterial* material, float* specular)
{
	for (int i = 0; i < 4; i++) {
		specular[i] = material->specular[i];
	}
}

float GetShininessFromMaterial(LitMaterial* material)
{
	return material->shininess;
}

void AddTextureToMaterial(LitMaterial* material, Texture* texture)
{
	material->textures.push_back(*texture);
}

Material* NewUnlitMaterial()
{
	UnlitMaterial* unlitMaterial = new UnlitMaterial();
	return unlitMaterial;
}

void SetColorToMaterial(UnlitMaterial* material, float* color)
{
	material->color = glm::vec4(color[0], color[1], color[2], color[3]);
}

void GetColorFromMaterial(UnlitMaterial* material, float* color)
{
	for (int i = 0; i < 4; i++) {
		color[i] = material->color[i];
	}
}

void AddTextureToUnlitMaterial(UnlitMaterial* material, Texture* texture) {
	material->textures.push_back(*texture);
}

Texture* GetTextureFromUnlitMaterial(UnlitMaterial* material)
{
	if (material->textures.empty()) return NULL;
	return &material->textures[0];
}

void SetTransparent(Material* material, bool isTransparent)
{
	material->transparent = isTransparent;
}

#pragma endregion

#pragma region MeshRendererFunctions

MeshRenderer* NewMeshRenderer()
{
	return new MeshRenderer();
}

void SetMeshToMeshRenderer(MeshRenderer* meshRenderer, Mesh* mesh)
{
	meshRenderer->mesh = *mesh;
}

void SetupMeshRenderer(MeshRenderer* meshRenderer)
{
	meshRenderer->Setup();
}

void RenderMeshRenderer(MeshRenderer* meshRenderer, Transform* transform, Material* material)
{
	meshRenderer->Render(*transform,material);
}

void CleanUpMeshRenderer(MeshRenderer* meshRenderer)
{
	meshRenderer->CleanUp();
}


#pragma endregion

#pragma region TransformFunctions

Transform* NewTransform()
{
	return new Transform();
}

void SetTransformPosition(Transform* transform, float* pos)
{
	transform->position = glm::vec3(pos[0], pos[1], pos[2]);
}

void SetTransformSize(Transform* transform, float* size)
{
	transform->size = glm::vec3(size[0], size[1], size[2]);
}

void SetTransformRotation(Transform* transform, float* rotation)
{
	transform->rotation = glm::vec3(rotation[0], rotation[1], rotation[2]);
}

#pragma endregion

#pragma region OpenGLFunctions

void OpenGLEnable(int glEnum)
{
	OpenGLFunctions::GLEnable(glEnum);
}

void OpenGLDisable(int glEnum)
{
	OpenGLFunctions::GLDisable(glEnum);
}

void OpenGLClear(int glEnum)
{
	OpenGLFunctions::GLClear(glEnum);
}

void OpenGLStencilMask(int mask)
{
	OpenGLFunctions::GLStencilMask(mask);
}

void OpenGLStencilFunc(int func, int ref, int mask)
{
	OpenGLFunctions::GLStencilFunc(func, ref, mask);
}

void OpenGLStencilOp(int sfail, int dpfail, int dppass)
{
	OpenGLFunctions::GLStencilOp(sfail, dpfail, dppass);
}

void OpenGLBlendFunc(int sfactor, int dfactor)
{
	OpenGLFunctions::GLBlendFunc(sfactor, dfactor);
}

void OpenGLDepthFunc(int func)
{
	OpenGLFunctions::GLDepthFunc(func);
}

#pragma endregion

#pragma region TextRenderFunction

TextRenderer* NewTextRenderer()
{
	return new TextRenderer();
}

void LoadFontToTextRenderer(TextRenderer* textRenderer, Texture* m_texture, int widthRes, int heightRes, int cellHeight, int cellWidth, int initialASCII)
{
	textRenderer->LoadFont(m_texture, widthRes, heightRes, cellHeight, cellWidth, initialASCII);
}

void SetupTextQuad(TextRenderer* textRenderer)
{
	textRenderer->SetupTextQuad();
}

void RenderText(TextRenderer* textRenderer, Shader& s, const char* text, float x, float y, float scale, float* color)
{
	textRenderer->RenderText(s, text, x, y, scale, glm::vec3(color[0], color[1], color[2]));
}

#pragma endregion

#pragma region LineRendererFunctions

LineRenderer* NewLineRenderer()
{
	return new LineRenderer();
}

void LineRendererSetup(LineRenderer* lineRenderer)
{
	lineRenderer->Setup();
}

void LineRendererSetNewPosition(LineRenderer* lineRenderer, float* from, float* to)
{
	lineRenderer->SetNewLinePosition(glm::vec3(from[0], from[1], from[2]), glm::vec3(to[0], to[1], to[2]));
}

void LineRendererSetNewColor(LineRenderer* lineRenderer, float* color)
{
	lineRenderer->SetNewColor(glm::vec3(color[0], color[1], color[2]));
}

void LineRender(LineRenderer* lineRenderer, Shader& s,float lineWidth)
{
	lineRenderer->Render(s, lineWidth);
}

void LineRendererCleanUp(LineRenderer* lineRenderer)
{
	lineRenderer->CleanUp();
}

#pragma endregion

#pragma region Cubemap Functions


Cubemaps* NewCubemap()
{
	return new Cubemaps();
}

void LoadTextureToCubemap(Cubemaps* cubemaps, Mesh* mesh, const char** texturePaths)
{
	std::vector<std::string> faces;
	faces.push_back(texturePaths[0]);
	faces.push_back(texturePaths[1]);
	faces.push_back(texturePaths[2]);
	faces.push_back(texturePaths[3]);
	faces.push_back(texturePaths[4]);
	faces.push_back(texturePaths[5]);

	cubemaps->mesh = *mesh;
	cubemaps->LoadCubemaps(faces);
}

void RenderCubemap(Cubemaps* cubemaps)
{
	cubemaps->Render();
}

#pragma endregion







