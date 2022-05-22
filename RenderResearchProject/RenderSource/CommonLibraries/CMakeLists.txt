SET (CMAKE_MODULE_PATH "${CMAKE_CURRENT_SOURCE_DIR}/../CMake")

find_package(OpenGL REQUIRED)

include_directories(include)
link_directories(lib)

#file( GLOB_RECURSE include "[^.]*.h" "[^.]*.hpp")
#file( GLOB_RECURSE sources "[^.]*.c")

set(GLFW ${OPENGL_gl_LIBRARY} ${CMAKE_CURRENT_SOURCE_DIR}/lib/glfw3.lib)
set(GLSRC ${CMAKE_SOURCE_DIR}/CommonLibraries/sources/glad.c)
set(STB ${CMAKE_SOURCE_DIR}/CommonLibraries/include/STB/stb.cpp)
set(ASSIMP ${CMAKE_CURRENT_SOURCE_DIR}/lib/assimp-vc142-mt.lib)


set_property(GLOBAL PROPERTY GLFW "${GLFW}")
set_property(GLOBAL PROPERTY GLSRC "${GLSRC}")
set_property(GLOBAL PROPERTY STB "${STB}")
set_property(GLOBAL PROPERTY ASSIMP "${ASSIMP}")
