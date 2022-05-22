#version 330 core

out vec4 FragColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform int noTex;
uniform int transparent;
uniform vec4 color;

void main(){

	vec4 currentColor =color;


	if(noTex!=1){
		currentColor = texture(texture0,texCoord)*currentColor;
	}

	if(transparent != 1) currentColor.a = 1.0f;

	FragColor = currentColor;

}