#version 330 core

struct Material{
	vec4 diffuse;
	vec4 specular;
	float shininess;
};

out vec4 FragColor;

uniform vec3 color;

void main(){
	FragColor = vec4(color,1.0f);

}