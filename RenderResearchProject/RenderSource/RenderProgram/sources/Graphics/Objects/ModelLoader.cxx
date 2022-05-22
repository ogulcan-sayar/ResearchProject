#include "Graphics/Objects/ModelLoader.h"

ModelLoader::ModelLoader(bool noTex)
	:noTex(noTex) {}


Model* ModelLoader::LoadModel(std::string path) {
	Assimp::Importer import;
	const aiScene* scene = import.ReadFile(path, aiProcess_Triangulate | aiProcess_FlipUVs);

	if (!scene || scene->mFlags & AI_SCENE_FLAGS_INCOMPLETE || !scene->mRootNode) {
		std::cout << "Could not load model at " << path << std::endl << import.GetErrorString() << std::endl;
		return NULL;
	}

	Model* rootModel = new Model();
	rootModel->parent = NULL;
	

	directory = path.substr(0, path.find_last_of("\\"));
	ProcessNode(scene->mRootNode, scene, rootModel, rootModel);

	return rootModel;
}

void ModelLoader::ProcessNode(aiNode* node, const aiScene* scene, Model* currentModel, Model* rootModel) {
	//process all meshes

	currentModel->childCount = node->mNumChildren;

	for (unsigned int i = 0; i < node->mNumMeshes; i++) {
		aiMesh* mesh = scene->mMeshes[node->mMeshes[i]];
		currentModel->meshes.push_back(ProcessMesh(mesh, rootModel));
		rootModel->totalMeshCount++;
		currentModel->materials.push_back(&LoadMaterials(mesh, scene));
		rootModel->totalMaterialCount++;
	}

	for (unsigned int i = 0; i < node->mNumChildren; i++) {
		Model* childModel = new Model();
		childModel->parent = currentModel;
		currentModel->children.push_back(childModel);

		ProcessNode(node->mChildren[i], scene, childModel,rootModel);
	}
}

Mesh ModelLoader::ProcessMesh(aiMesh* mesh, Model* rootModel) {
	std::vector<Vertex> vertices;
	std::vector<unsigned int> indices;

	// vertices
	for (unsigned int i = 0; i < mesh->mNumVertices; i++) {
		Vertex vertex;

		SetVertexBoneDataToDefault(vertex);

		// position
		vertex.pos = glm::vec3(
			mesh->mVertices[i].x,
			mesh->mVertices[i].y,
			mesh->mVertices[i].z
		);

		// normal vectors
		vertex.normal = glm::vec3(
			mesh->mNormals[i].x,
			mesh->mNormals[i].y,
			mesh->mNormals[i].z
		);

		// textures
		if (mesh->mTextureCoords[0]) {
			vertex.texCoord = glm::vec2(
				mesh->mTextureCoords[0][i].x,
				mesh->mTextureCoords[0][i].y
			);
		}
		else {
			vertex.texCoord = glm::vec2(0.0f);
		}

		vertices.push_back(vertex);
	}

	// process indices
	for (unsigned int i = 0; i < mesh->mNumFaces; i++) {
		aiFace face = mesh->mFaces[i];
		for (unsigned int j = 0; j < face.mNumIndices; j++) {
			indices.push_back(face.mIndices[j]);
		}
	}

	ExtractBoneWeightForVertices(vertices, mesh, rootModel);

	return Mesh(vertices, indices);
}

Material& ModelLoader::LoadMaterials(aiMesh* mesh, const aiScene* scene)
{
	std::vector<Texture> textures;
	LitMaterial* returnMaterial = new LitMaterial();

	// process materials
	if (mesh->mMaterialIndex >= 0) {
		aiMaterial* material = scene->mMaterials[mesh->mMaterialIndex];

		if (noTex) {
			// diffuse color
			aiColor4D diff(1.0f);
			aiGetMaterialColor(material, AI_MATKEY_COLOR_DIFFUSE, &diff);

			// specular color;
			aiColor4D spec(1.0f);
			aiGetMaterialColor(material, AI_MATKEY_COLOR_SPECULAR, &spec);

			glm::vec4 diffuse = { diff.r,diff.g,diff.b, diff.a };
			returnMaterial->diffuse = diffuse;

			glm::vec4 specular = { spec.r,spec.g,spec.b, spec.a };
			returnMaterial->specular = specular;

			return *returnMaterial;
		}

		// diffuse maps
		std::vector<Texture> diffuseMaps = LoadTextures(material, aiTextureType_DIFFUSE);
		textures.insert(textures.end(), diffuseMaps.begin(), diffuseMaps.end());

		// specular maps
		std::vector<Texture> specularMaps = LoadTextures(material, aiTextureType_SPECULAR);
		textures.insert(textures.end(), specularMaps.begin(), specularMaps.end());

		returnMaterial->textures = textures;
		return *returnMaterial;
	}
}



std::vector<Texture> ModelLoader::LoadTextures(aiMaterial* mat, aiTextureType type) {
	std::vector<Texture> textures;

	for (unsigned int i = 0; i < mat->GetTextureCount(type); i++) {
		aiString str;
		mat->GetTexture(type, i, &str);

		// prevent duplicate loading
		bool skip = false;
		for (unsigned int j = 0; j < textureLoaded.size(); j++) {
			if (std::strcmp(textureLoaded[j].path.data(), str.C_Str()) == 0) {
				textures.push_back(textureLoaded[j]);
				skip = true;
				break;
			}
		}

		if (!skip) {
			//not loaded yet
			Texture tex(directory, str.C_Str(), type);
			tex.Load(false);
			textures.push_back(tex);
			textureLoaded.push_back(tex);
		}
	}

	return textures;
}

void ModelLoader::SetVertexBoneDataToDefault(Vertex& vertex)
{
	for (int i = 0; i < MAX_BONE_INFLUENCE; i++)
	{
		vertex.m_BoneIDs[i] = -1;
		vertex.m_Weights[i] = 0.0f;
	}
}

void ModelLoader::SetVertexBoneData(Vertex& vertex, int boneID, float weight)
{
	if (weight < 0.01) return;
		
	for (int i = 0; i < MAX_BONE_INFLUENCE; ++i)
	{
		
		if (vertex.m_BoneIDs[i] < 0)
		{
			vertex.m_Weights[i] = weight;
			vertex.m_BoneIDs[i] = boneID;
			break;
		}
		/*else if (vertex.m_Weights[i] < weight) {
			vertex.m_Weights[i] = weight;
			vertex.m_BoneIDs[i] = boneID;
			break;
		}*/
	}
}

void ModelLoader::ExtractBoneWeightForVertices(std::vector<Vertex>& vertices, aiMesh* mesh, Model* rootModel)
{
	for (int boneIndex = 0; boneIndex < mesh->mNumBones; ++boneIndex)
	{
		int boneID = -1;
		std::string boneName = mesh->mBones[boneIndex]->mName.C_Str();
		if (rootModel->m_BoneInfoMap.find(boneName) == rootModel->m_BoneInfoMap.end())
		{
			BoneInfo newBoneInfo;
			newBoneInfo.id = rootModel->m_BoneCounter;
			newBoneInfo.offset = AssimpGLMHelpers::ConvertMatrixToGLMFormat(
				mesh->mBones[boneIndex]->mOffsetMatrix);
			rootModel->m_BoneInfoMap[boneName] = newBoneInfo;
			boneID = rootModel->m_BoneCounter;
			rootModel->m_BoneCounter++;
		}
		else
		{
			boneID = rootModel->m_BoneInfoMap[boneName].id;
		}

		assert(boneID != -1);
		auto weights = mesh->mBones[boneIndex]->mWeights;
		int numWeights = mesh->mBones[boneIndex]->mNumWeights;

		for (int weightIndex = 0; weightIndex < numWeights; ++weightIndex)
		{
			int vertexId = weights[weightIndex].mVertexId;
			float weight = weights[weightIndex].mWeight;
			assert(vertexId <= vertices.size());
			SetVertexBoneData(vertices[vertexId], boneID, weight);
		}
	}
}
