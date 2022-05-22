#ifndef MODELLOADER_H
#define	MODELLOADER_H

#include "GLAD/glad.h"
#include <GLFW/glfw3.h>

#include "ASSIMP/Importer.hpp"
#include "ASSIMP/scene.h"
#include "ASSIMP/postprocess.h"

#include <GLM/glm.hpp>
#include <GLM/gtc/matrix_transform.hpp>
#include <vector>
#include "Graphics/Objects/Mesh.h"
#include "Graphics/Rendering/Material.h"

#include <map>
#include <AssimpGLMHelpers.h>
#include <Graphics/Objects/Model.h>

class ModelLoader {

private:

	void SetVertexBoneDataToDefault(Vertex& vertex);
	void ExtractBoneWeightForVertices(std::vector<Vertex>& vertices, aiMesh* mesh, Model* rootModel);
	void SetVertexBoneData(Vertex& vertex, int boneID, float weight);

public:

	ModelLoader(bool noTex = false);

	Model* LoadModel(std::string path);


protected:
	bool noTex;

	
	std::string directory;
	std::vector<Texture> textureLoaded;

	void ProcessNode(aiNode* node, const aiScene* scene, Model* currentModel, Model* rootModel);
	Mesh ProcessMesh(aiMesh* mesh, Model* rootModel);
	Material& LoadMaterials(aiMesh* mesh, const aiScene* scene);
	std::vector<Texture> LoadTextures(aiMaterial* mat, aiTextureType type);

};

#endif 
