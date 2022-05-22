#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING
#include <iostream>
#include <fstream>
#include <sstream>
#include <streambuf>
#include <string>
#include"RenderProgram.h"

#include "STB/stb_image.h"

#include "GLAD/glad.h"
#include "GLFW/glfw3.h"

#include "GLM/glm.hpp"
#include "GLM/gtc/type_ptr.hpp"
#include "GLM/gtc/matrix_transform.hpp"

#include "Graphics/Rendering/Shader.h"
#include "Graphics/Rendering/Texture.h"

#include "IO/KeyboardInput.h"
#include "IO/MouseInput.h"

#include "IO/Camera.h"
#include "Screen.h"
#include "Graphics/Objects/ModelLoader.h"
#include "Graphics/Model/Cube.h"
#include "Graphics/Model/Lamp.h"

#include "Graphics/Rendering/Light.h"
#include "MeshRenderer.h"

//#include <experimental/filesystem> current path i bulmak istiyosan aç

void ProcessInput(double dt, Screen screen);

// settings
//unsigned int SCR_WIDTH = 800;
//unsigned int SCR_HEIGHT = 600;



float mixVal = 0.5f;

Camera cameras[2] = {
	Camera(glm::vec3(0.0f, 0.0f,3.0f)),
	Camera(glm::vec3(0, 0, 15.0f))
};

int activeCamera = 0;

//Camera camera = Camera(glm::vec3(0.0f, 0.0f, 3.0f));
float deltaTime = 0.0f;
float lastFrame = 0.0f;

bool spotLightOn = true;

void CustomRender::Render() {

	/*Screen screen= Screen(800,600);

	glfwInit();
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

#ifdef __APPLE__
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
#endif

	if (!screen.Initialize())
	{
		return;
	}


	// glad: load all OpenGL function pointers
	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
	{
		std::cout << "Failed to initialize GLAD" << std::endl;
		return;
	}


	screen.SetParameters();

	//shaders compile
	Shader shader("D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs", "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/lit.fs");
	Shader lampShader("D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/object.vs" , "D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Shaders/unlit.fs");

	LitMaterial objectMaterial;
	UnlitMaterial lampMaterial{ glm::vec3(1) };

	objectMaterial.shader = &shader;
	lampMaterial.shader = &lampShader;

	ModelLoading trolModel;
	trolModel.LoadModel("D:/GitRepos/SimulationResearchProject/SimulationResearchProject/Test/DLLTest/DLLTest/Assets/Models/Trol/scene.gltf");
	Transform trolTransform = { glm::vec3(0.0,0.0,0.0f), glm::vec3(0.05f), glm::vec3(0,0,0) };
	MeshRenderer trolMeshRenderer;
	trolMeshRenderer.material = trolModel.materials[0];
	trolMeshRenderer.material->shader = &shader;
	trolMeshRenderer.mesh = trolModel.meshes[0];
	trolMeshRenderer.Setup();

	DirectionalLight dirLight = { glm::vec3(-0.2f, -1.0f, -0.3f), 
		glm::vec4(0.1f,0.1f,0.1f,1.0f), 
		glm::vec4(0.4f,0.4f,0.4f,1.0f), 
		glm::vec4(0.75f,0.75f,0.75f,1.0f) };


	Transform lambTransforms[] = {
		{glm::vec3(0.7f, 0.2f, 2.0f), glm::vec3(.25f)},
		{glm::vec3(2.3f, -3.3f, -4.0f), glm::vec3(.25f)},
		{glm::vec3(-4.0f, 2.0f, -12.0f), glm::vec3(.25f)},
		{glm::vec3(0.0f, 0.0f, -3.0f), glm::vec3(.25f)}
	};
	MeshRenderer lambMeshRenderers[4];

	Lamp lamps[4];
	for (int i = 0; i < 4; i++) {

		lamps[i] = Lamp(glm::vec4(0.05f, 0.05f, 0.05f,1.0f), glm::vec4(0.8f, 0.8f, 0.8f,1.0f), glm::vec4(1.0f),
			1.0f, 0.07f, 0.032f,
			lambTransforms[i].position);

		lamps[i].Initialize();
		lambMeshRenderers[i].mesh = lamps[i].meshes[0];
		lambMeshRenderers[i].material = &lampMaterial;
		lambMeshRenderers[i].Setup();
	}


	SpotLight spotLight = {
		cameras[activeCamera].cameraPos, cameras[activeCamera].cameraFront,
		glm::cos(glm::radians(5.0f)), glm::cos(glm::radians(10.0f)),
		1.0f, 0.07f, 0.032f,
		glm::vec4(0.0f,0.0f,0.0f,1.0f), glm::vec4(1.0f), glm::vec4(1.0f)
	};

	// render loop
	while (!screen.ShouldClose())
	{
		double currentTime = glfwGetTime();
		deltaTime = currentTime - lastFrame;
		lastFrame = currentTime;

		// input
		ProcessInput(deltaTime,screen);

		// render
		// ------
		screen.Update();

		shader.Activate();
		shader.Set3Float("viewPos", cameras[activeCamera].cameraPos);

		dirLight.direction =glm::vec3(glm::rotate(glm::mat4(1.0f), glm::radians(90.0f * deltaTime) , glm::vec3(1.0f, 0.0f, 0.0f)) * glm::vec4(dirLight.direction, 1.0f));
		dirLight.Render(shader);

		if (spotLightOn) {
			spotLight.position = cameras[activeCamera].cameraPos;
			spotLight.direction = cameras[activeCamera].cameraFront;
			spotLight.Render(shader, 0);
			shader.SetInt("numbSpotLights", 1);
		}
		else {
			shader.SetInt("numbSpotLights", 0);
		}


		for (int i = 0; i < 4; i++) 
		{
			lamps[i].pointLight.Render(shader, i);
		}
		shader.SetInt("numbPointLights", 4);

		//create transformations for screen
		glm::mat4 view = glm::mat4(1.0f);
		glm::mat4 projection = glm::mat4(1.0f);
		view = cameras[activeCamera].GetViewMatrix();
		projection = glm::perspective(glm::radians(cameras[activeCamera].GetZoom()), (float)screen.SCR_WIDTH / (float)screen.SCR_HEIGHT, 0.1f, 100.0f);

		shader.SetMat4("view", view);
		shader.SetMat4("projection", projection);

		trolMeshRenderer.Render(trolTransform);
	

		lampShader.Activate();
		lampShader.SetMat4("view", view);
		lampShader.SetMat4("projection", projection);
		for (int i = 0; i < 4; i++) {
			lambMeshRenderers[i].Render(lambTransforms[i]);
		}

		screen.NewFrame();
	}

	trolMeshRenderer.CleanUp();

	for (int i = 0; i < 4; i++) {
		lambMeshRenderers[i].CleanUp();
	}

	glfwTerminate();*/
}


void ProcessInput(double dt, Screen screen)
{
	if (KeyboardInput::Key(GLFW_KEY_ESCAPE)) {
		screen.SetShouldClose(true);
	}


	if (KeyboardInput::KeyWentDown(GLFW_KEY_L)) {
		spotLightOn = !spotLightOn;
	}

	if (KeyboardInput::KeyWentDown(GLFW_KEY_TAB))
	{
		activeCamera += (activeCamera == 0) ? 1 : -1;
	}

	if (KeyboardInput::Key(GLFW_KEY_SPACE)) {

		cameras[activeCamera].UpdateCameraPos(CameraDirection::UP, dt);
	}
	if (KeyboardInput::Key(GLFW_KEY_LEFT_SHIFT)) {
		cameras[activeCamera].UpdateCameraPos(CameraDirection::DOWN, dt);
	}
	if (KeyboardInput::Key(GLFW_KEY_D)) {

		cameras[activeCamera].UpdateCameraPos(CameraDirection::RIGHT, dt);
	}
	if (KeyboardInput::Key(GLFW_KEY_A)) {

		cameras[activeCamera].UpdateCameraPos(CameraDirection::LEFT, dt);
	}
	if (KeyboardInput::Key(GLFW_KEY_W)) {

		cameras[activeCamera].UpdateCameraPos(CameraDirection::FORWARD, dt);
	}
	if (KeyboardInput::Key(GLFW_KEY_S)) {

		cameras[activeCamera].UpdateCameraPos(CameraDirection::BACKWARD, dt);
	}

	double dx = MouseInput::GetDx();
	double dy = MouseInput::GetDy();
	if (dx != 0 || dy != 0)
	{

		cameras[activeCamera].UpdateCameraDirection(dx, dy);
	}

	double scrollDy = MouseInput::GetScrollDy();
	if (scrollDy != 0)
	{
		cameras[activeCamera].UpdateCameraZoom(scrollDy);
	}


}


CustomRender::CustomRender()
{
}

CustomRender::~CustomRender()
{
}
