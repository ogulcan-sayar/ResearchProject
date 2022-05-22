#include "Graphics/Objects/Model.h"

Model::Model()
{
	totalMaterialCount = 0;
	totalMeshCount = 0;
}

int Model::GetChildCount()
{
	return childCount;
}

Model* Model::GetChildModel(int childIdx)
{
	if (childIdx >= childCount) {
		std::cout<<"There is no child that index!"<<std::endl;
		return NULL;
	}
	return children[childIdx];
}

int Model::GetMeshCount()
{
	return static_cast<int>(meshes.size());
}

Mesh* Model::GetMesh(int meshIdx)
{
	if (meshIdx >= meshes.size()) {
		std::cout << "There is no mesh that index!" << std::endl;
		return NULL;
	}
	return &meshes[meshIdx];
}

int Model::GetMaterialCount()
{
	return static_cast<int>(materials.size());
}

Material* Model::GetMaterial(int materialIdx)
{
	if (materialIdx >= materials.size()) {
		std::cout << "There is no material that index!" << std::endl;
		return NULL;
	}
	return materials[materialIdx];
}
