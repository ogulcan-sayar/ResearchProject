#version 330 core

#define MAX_POINT_LIGHTS 20
#define MAX_SPOT_LIGHTS 5

struct Material{
	vec4 diffuse;
	vec4 specular;
	float shininess;
};

struct PointLight{
	vec3 position;
	float k0;
	float k1;
	float k2;
	vec4 ambient;
	vec4 diffuse;
	vec4 specular;
};

struct DirectionalLight{
	vec3 direction;
	
	vec4 ambient;
	vec4 diffuse;
	vec4 specular;
};

struct SpotLight{
	vec3 direction;
	vec3 position;
	
	float k0;
	float k1;
	float k2;
	float cutOff;
	float outerCutOff;
	
	vec4 ambient;
	vec4 diffuse;
	vec4 specular;
};

uniform sampler2D diffuse0;
uniform sampler2D specular0;

uniform PointLight pointLight[MAX_POINT_LIGHTS];
uniform SpotLight spotLight[MAX_SPOT_LIGHTS];
uniform DirectionalLight directionalLight;

uniform int numbPointLights;
uniform int numbSpotLights;

uniform Material material;
uniform int noTex;

out vec4 FragColor;

in vec3 fragPos;
in vec3 normal;
in vec2 texCoord;
uniform vec3 viewPos;

vec4 CalcPointLight(int idx, vec3 norm, vec3 viewDir, vec4 diffMap, vec4 specMap);
vec4 CalcDirectionalLight(vec3 norm, vec3 viewDir, vec4 diffMap, vec4 specMap);
vec4 CalcSpotLight(int idx, vec3 norm, vec3 viewDir, vec4 diffMap, vec4 specMap);

void main(){
	vec3 norm = normalize(normal);
	vec3 viewDir = normalize(viewPos - fragPos);

	vec4 diffMap;
	vec4 specMap;

	if(noTex == 1){
		diffMap = material.diffuse;
		specMap = material.specular;
	}
	else{
		diffMap = texture(diffuse0, texCoord);
		specMap = texture(specular0, texCoord);
	}
	
	//placeholder
	vec4 result;
	
	result = CalcDirectionalLight(norm, viewDir, diffMap, specMap);
	
	for(int i = 0; i < numbPointLights; i++) {
	
		result += CalcPointLight(i, norm, viewDir, diffMap, specMap);
	}
	
	for(int i = 0; i < numbSpotLights; i++) {
	
		result += CalcSpotLight(i, norm, viewDir, diffMap, specMap);
	}
	
	
	FragColor = result;
}

vec4 CalcPointLight(int idx, vec3 norm, vec3 viewDir, vec4 diffMap, vec4 specMap){
	
	//ambient
	vec4 ambient = pointLight[idx].ambient * diffMap;
	
	//diffuse
	vec3 lightDir = normalize(pointLight[idx].position - fragPos);
	float diff = max(dot(norm, lightDir), 0.0);
	vec4 diffuse = pointLight[idx].diffuse * (diff * diffMap);	
	
	//specular
	vec3 reflectDir = reflect(-lightDir,norm);
	float spec = pow(max(dot(viewDir,reflectDir),0.0), material.shininess * 128);
	vec4 specular = pointLight[idx].specular * (spec * specMap);	
	
	float distance = length(pointLight[idx].position - fragPos);
	float attenuation= 1.0 / (pointLight[idx].k0 + pointLight[idx].k1 * distance + pointLight[idx].k2 * (distance * distance));
	return vec4(ambient + diffuse + specular) * attenuation;
}

vec4 CalcDirectionalLight(vec3 norm, vec3 viewDir, vec4 diffMap, vec4 specMap){
	
	//ambient
	vec4 ambient = directionalLight.ambient * diffMap;
	
	//diffuse
	vec3 lightDir = normalize(-directionalLight.direction);
	float diff = max(dot(norm, lightDir), 0.0);
	vec4 diffuse = directionalLight.diffuse * (diff * diffMap);	
	
	//specular
	vec3 reflectDir = reflect(-lightDir,norm);
	float spec = pow(max(dot(viewDir,reflectDir),0.0), material.shininess * 128);
	vec4 specular = directionalLight.specular * (spec * specMap);	
	
	return vec4(ambient + diffuse + specular);
}

vec4 CalcSpotLight(int idx, vec3 norm, vec3 viewDir, vec4 diffMap, vec4 specMap){
	
	//ambient
	vec4 ambient = spotLight[idx].ambient * diffMap;
	
	//diffuse
	vec3 lightDir = normalize(spotLight[idx].position - fragPos);
	float theta = dot(lightDir, normalize(-spotLight[idx].direction));
	
	if(theta > spotLight[idx].outerCutOff) {
		
		float diff = max(dot(norm, lightDir), 0.0);
		vec4 diffuse = spotLight[idx].diffuse * (diff * diffMap);	
		
		vec3 reflectDir = reflect(-lightDir,norm);
		float spec = pow(max(dot(viewDir, reflectDir),0.0), material.shininess * 128);
		vec4 specular = spotLight[idx].specular * (spec * specMap);
		
		float intensity = (theta - spotLight[idx].outerCutOff) / (spotLight[idx].cutOff - spotLight[idx].outerCutOff);
		intensity = clamp(intensity, 0.0, 1.0);
		diffuse *= intensity;
		specular *= intensity; 
		
		float distance = length(spotLight[idx].position - fragPos);
		float attenuation= 1.0 / (spotLight[idx].k0 + spotLight[idx].k1 * distance + spotLight[idx].k2 * (distance * distance));
	
		return vec4(ambient + diffuse + specular) * attenuation;
	}
	else {
	
		return ambient;
	}
	
}