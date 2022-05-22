#ifndef CAMERA_H
#define CAMERA_H

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>

enum class CameraDirection {
	NONE,
	FORWARD,
	BACKWARD,
	LEFT,
	RIGHT,
	UP,
	DOWN
};

class Camera {
public:
	glm::vec3 cameraPos;

	glm::vec3 cameraFront;
	glm::vec3 cameraUp;
	glm::vec3 cameraRight;

	glm::vec3 worldUp;

	float yaw;
	float pitch;
	float speed;
	float zoom;

	Camera(glm::vec3 position);

	void UpdateCameraDirection(double dx, double dy);
	void UpdateCameraPos(CameraDirection dir, double dt);
	void UpdateCameraZoom(double dy);

	float GetZoom();

	glm::mat4 GetViewMatrix();

private:
	void UpdateCameraVectors();



};

#endif 
