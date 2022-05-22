cmake_minimum_required(VERSION 2.10)
project (RenderProgram)

option(BUILD_SHARED_LIBS "Build using shared libraries" ON)

SET(DLL_INSTALL_FOLDER "D:/Choose ... path" CACHE PATH "Library install folder")

if (NOT IS_DIRECTORY "${DLL_INSTALL_FOLDER}")
	MESSAGE(FATAL_ERROR "Library install folder does not exist")
endif()

function( get_source include sources)
  file( GLOB_RECURSE include "[^.]*.h" )
  file( GLOB_RECURSE sources "[^.]*.cxx" )

  set( ${ARGV0} ${include} PARENT_SCOPE )
  set( ${ARGV1} ${sources} PARENT_SCOPE )
endfunction( )

function( create_build )
  file( GLOB_RECURSE components "${CMAKE_SOURCE_DIR}/*/CMakeLists.txt" )
  foreach( component ${components} )
    get_filename_component( path ${component} PATH )
    add_subdirectory( ${path} )
  endforeach( )
endfunction( )

create_build()
