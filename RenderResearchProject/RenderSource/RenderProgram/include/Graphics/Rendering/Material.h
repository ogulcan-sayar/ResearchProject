#ifndef MATERIAL_H
#define MATERIAL_H

#include <glm/glm.hpp>

#include <vector>
#include "Graphics/Rendering/Texture.h"
#include "Graphics/Rendering/Shader.h"

/*
    material structure to contain lighting values for different materials
*/


class Material {
public:

    Shader* shader;
    bool transparent;
    virtual void ConfigurationShader() = 0;
};

class LitMaterial : public Material {
public:

    LitMaterial();
    LitMaterial(glm::vec4 ambient, glm::vec4 diffuse, glm::vec4 specular, float shininess);
    
    void ConfigurationShader() override;

    // lighting values
    glm::vec4 ambient;
    glm::vec4 diffuse;
    glm::vec4 specular;
    float shininess;

    std::vector<Texture> textures;
    
    /*
        static instances of common materials
    */

    static LitMaterial emerald;
    static LitMaterial jade;
    static LitMaterial obsidian;
    static LitMaterial pearl;
    static LitMaterial ruby;
    static LitMaterial turquoise;
    static LitMaterial brass;
    static LitMaterial bronze;
    static LitMaterial chrome;
    static LitMaterial copper;
    static LitMaterial gold;
    static LitMaterial silver;
    static LitMaterial black_plastic;
    static LitMaterial cyan_plastic;
    static LitMaterial green_plastic;
    static LitMaterial red_plastic;
    static LitMaterial white_plastic;
    static LitMaterial yellow_plastic;
    static LitMaterial black_rubber;
    static LitMaterial cyan_rubber;
    static LitMaterial green_rubber;
    static LitMaterial red_rubber;
    static LitMaterial white_rubber;
    static LitMaterial yellow_rubber;

    // function to mix two materials with a proportion
    static LitMaterial mix(LitMaterial m1, LitMaterial m2, float mix = 0.5f);
};

class UnlitMaterial : public Material {
public:

    UnlitMaterial();
    UnlitMaterial(glm::vec4 color);

    void ConfigurationShader() override;

    glm::vec4 color;
    std::vector<Texture> textures;
};

#endif