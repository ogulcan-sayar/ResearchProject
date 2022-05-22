#ifndef MODEL_H
#define	MODEL_H

#include "Graphics/Objects/Mesh.h"
#include <Graphics/Rendering/Material.h>
#include <map>



struct BoneInfo
{
	/*id is index in finalBoneMatrices*/
	int id;

	/*offset matrix transforms vertex from model space to bone space*/
	glm::mat4 offset;
};


class Model {

public:
	Model* parent;
	std::vector<Model*> children;
	int childCount;

	std::vector<Mesh> meshes;
	std::vector<Material*> materials;

	//root only
	std::map<std::string, BoneInfo> m_BoneInfoMap;
	int m_BoneCounter = 0;
	int totalMeshCount;
	int totalMaterialCount;


	Model();
	int GetChildCount();
	Model* GetChildModel(int childIdx);

	int GetMeshCount();
	Mesh* GetMesh(int meshIdx);

	int GetMaterialCount();
	Material* GetMaterial(int materialIdx);

};

#endif