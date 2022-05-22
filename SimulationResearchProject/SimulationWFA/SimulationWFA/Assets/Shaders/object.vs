#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoord;
layout(location = 3) in ivec4 boneIds; 
layout(location = 4) in vec4 weights;

const int MAX_BONES = 100;
const int MAX_BONE_INFLUENCE = 4;
uniform mat4 finalBonesMatrices[MAX_BONES];

out vec3 fragPos;
out vec3 normal;
out vec2 texCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform int animate;

void main(){

	vec4 totalPosition = vec4(aPos,1.0f);
	vec3 totalNormal = mat3(transpose(inverse(model))) * aNormal;

	if(animate == 1)
	{
		totalPosition = vec4(0.0f);
		totalNormal = vec3(0.0f);

		for(int i = 0 ; i < MAX_BONE_INFLUENCE ; i++)
    	{
        	if(boneIds[i] == -1){
            	continue;
        	}

        	if(boneIds[i] >=MAX_BONES) 
        	{
           	 	totalPosition = vec4(aPos,1.0f);
            	break;
        	}
        	vec4 localPosition = finalBonesMatrices[boneIds[i]] * vec4(aPos,1.0f);
        	totalPosition += localPosition * weights[i];

        	vec3 localNormal = mat3(finalBonesMatrices[boneIds[i]]) * aNormal;
        	totalNormal += localNormal; 
    	}
    }


	mat4 viewModel = view * model;
    gl_Position =  projection * viewModel * totalPosition;
    
    texCoord = aTexCoord;
    normal = totalNormal;
    fragPos = vec3(model*totalPosition);
}