#ifndef CUBE_HPP
#define CUBE_HPP

#include "Graphics/Objects/ModelLoader.h"
#include "Graphics/Rendering/Material.h"

class Cube : public ModelLoader {
public:
	
	LitMaterial material;

	Cube() {}

	void Initialize() {
		/*int numbOfVertices = 36;

		float vertices[] = {
			//positions					normal				textcoords
			-0.5f, -0.5f, -0.5f,	0.0f, 0.0f,	-1.0f,	  0.0f, 0.0f,
			 0.5f, -0.5f, -0.5f,	0.0f, 0.0f,	-1.0f,	  1.0f, 0.0f,
			 0.5f,  0.5f, -0.5f,	0.0f, 0.0f,	-1.0f,	  1.0f, 1.0f,
			 0.5f,  0.5f, -0.5f,	0.0f, 0.0f,	-1.0f,	  1.0f, 1.0f,
			-0.5f,  0.5f, -0.5f,	0.0f, 0.0f,	-1.0f,	  0.0f, 1.0f,
			-0.5f, -0.5f, -0.5f,	0.0f, 0.0f,	-1.0f,	  0.0f, 0.0f,
														  
			-0.5f, -0.5f,  0.5f,	0.0f, 0.0f,	 1.0f,	  0.0f, 0.0f,
			 0.5f, -0.5f,  0.5f,	0.0f, 0.0f,	 1.0f,	  1.0f, 0.0f,
			 0.5f,  0.5f,  0.5f,	0.0f, 0.0f,	 1.0f,	  1.0f, 1.0f,
			 0.5f,  0.5f,  0.5f,	0.0f, 0.0f,	 1.0f,	  1.0f, 1.0f,
			-0.5f,  0.5f,  0.5f,	0.0f, 0.0f,	 1.0f,	  0.0f, 1.0f,
			-0.5f, -0.5f,  0.5f,	0.0f, 0.0f,	 1.0f,	  0.0f, 0.0f,
														  
			-0.5f,  0.5f,  0.5f,	-1.0f, 0.0f, 0.0f,	  1.0f, 0.0f,
			-0.5f,  0.5f, -0.5f,	-1.0f, 0.0f, 0.0f,	  1.0f, 1.0f,
			-0.5f, -0.5f, -0.5f,	-1.0f, 0.0f, 0.0f,	  0.0f, 1.0f,
			-0.5f, -0.5f, -0.5f,	-1.0f, 0.0f, 0.0f,	  0.0f, 1.0f,
			-0.5f, -0.5f,  0.5f,	-1.0f, 0.0f, 0.0f,	  0.0f, 0.0f,
			-0.5f,  0.5f,  0.5f,	-1.0f, 0.0f, 0.0f,	  1.0f, 0.0f,
														  
			 0.5f,  0.5f,  0.5f,	+1.0f, 0.0f, 0.0f,	  1.0f, 0.0f,
			 0.5f,  0.5f, -0.5f,	+1.0f, 0.0f, 0.0f,	  1.0f, 1.0f,
			 0.5f, -0.5f, -0.5f,	+1.0f, 0.0f, 0.0f,	  0.0f, 1.0f,
			 0.5f, -0.5f, -0.5f,	+1.0f, 0.0f, 0.0f,	  0.0f, 1.0f,
			 0.5f, -0.5f,  0.5f,	+1.0f, 0.0f, 0.0f,	  0.0f, 0.0f,
			 0.5f,  0.5f,  0.5f,	+1.0f, 0.0f, 0.0f,	  1.0f, 0.0f,
														  
			-0.5f, -0.5f, -0.5f,	0.0f, -1.0f, 0.0f,	  0.0f, 1.0f,
			 0.5f, -0.5f, -0.5f,	0.0f, -1.0f, 0.0f,	  1.0f, 1.0f,
			 0.5f, -0.5f,  0.5f,	0.0f, -1.0f, 0.0f,	  1.0f, 0.0f,
			 0.5f, -0.5f,  0.5f,	0.0f, -1.0f, 0.0f,	  1.0f, 0.0f,
			-0.5f, -0.5f,  0.5f,	0.0f, -1.0f, 0.0f,	  0.0f, 0.0f,
			-0.5f, -0.5f, -0.5f,	0.0f, -1.0f, 0.0f,	  0.0f, 1.0f,
														  
			-0.5f,  0.5f, -0.5f,	0.0f, +1.0f, 0.0f,	  0.0f, 1.0f,
			 0.5f,  0.5f, -0.5f,	0.0f, +1.0f, 0.0f,	  1.0f, 1.0f,
			 0.5f,  0.5f,  0.5f,	0.0f, +1.0f, 0.0f,	  1.0f, 0.0f,
			 0.5f,  0.5f,  0.5f,	0.0f, +1.0f, 0.0f,	  1.0f, 0.0f,
			-0.5f,  0.5f,  0.5f,	0.0f, +1.0f, 0.0f,	  0.0f, 0.0f,
			-0.5f,  0.5f, -0.5f,	0.0f, +1.0f, 0.0f,	  0.0f, 1.0f
		};

		std::vector<unsigned int> indices(numbOfVertices);
		for (unsigned int i = 0; i < numbOfVertices; i++) {
			indices[i] = i;
		}

		/*std::string tex0Path = FilePath::ImagePath + "image3.jpg";
		Texture tex0(tex0Path.c_str(), "material.diffuse");
		tex0.Load();

		std::string tex1Path = FilePath::ImagePath + "image3_specular.png";
		Texture tex1(tex1Path.c_str(), "material.specular");
		tex1.Load();

		meshes.push_back(Mesh(Vertex::SetVertices(vertices, numbOfVertices), indices));*/
	}
};

#endif