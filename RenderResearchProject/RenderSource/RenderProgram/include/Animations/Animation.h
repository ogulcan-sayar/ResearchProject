#ifndef ANIMATION_H
#define	ANIMATION_H

#include "GLAD/glad.h"
#include <GLFW/glfw3.h>

#include "ASSIMP/Importer.hpp"
#include "ASSIMP/scene.h"
#include "ASSIMP/postprocess.h"

#include <GLM/glm.hpp>
#include <GLM/gtx/quaternion.hpp>
#include <GLM/gtc/matrix_transform.hpp>

#include <vector>;
#include <string>;
#include <AssimpGLMHelpers.h>
#include <Animations/Bones.h>
#include <map>
#include <Graphics/Objects/Model.h>

struct AssimpNodeData
{
    glm::mat4 transformation;
    std::string name;
    int childrenCount;
    std::vector<AssimpNodeData> children;
};

class Animation{

public:
    Animation() = default;
    Animation(const std::string& animationPath, Model* model);
    ~Animation();

    Bone* FindBone(const std::string& name);
    float GetTicksPerSecond();
    float GetDuration();
    const AssimpNodeData& GetRootNode();
    const std::map<std::string, BoneInfo>& GetBoneIDMap();

private:
    void ReadMissingBones(const aiAnimation* animation, Model& model);
    void ReadHeirarchyData(AssimpNodeData& dest, const aiNode* src);

    float m_Duration;
    int m_TicksPerSecond;
    std::vector<Bone> m_Bones;
    AssimpNodeData m_RootNode;
    std::map<std::string, BoneInfo> m_BoneInfoMap;
};



#endif