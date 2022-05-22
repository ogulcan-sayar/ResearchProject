#version 330 core

out vec4 FragColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform int noTex;
uniform vec3 color;

void main(){

	vec4 currentColor;

	if(noTex==1){
		currentColor = vec4(color,1.0f);
	}
	else{
		currentColor = texture(texture0,texCoord);
		if(currentColor.a < 0.1f) discard;
	}

	FragColor = currentColor;

}