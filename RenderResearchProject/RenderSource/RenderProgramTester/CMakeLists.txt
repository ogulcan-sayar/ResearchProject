SET (CMAKE_MODULE_PATH "${CMAKE_CURRENT_SOURCE_DIR}/../CMake")

ADD_EXECUTABLE(RenderProgramTester RenderProgramTester.cxx)

include_directories("${CMAKE_SOURCE_DIR}/RenderProgram")

TARGET_LINK_LIBRARIES(RenderProgramTester PRIVATE RenderProgram)