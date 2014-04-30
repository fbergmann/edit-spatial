
set (HEADERS 

local_operator.hh componentparameters.hh initial_conditions.hh reactionadapter.hh

)
set (SOURCES 
%NAME%.cc
)

add_executable(%NAME% ${SOURCES})
target_link_dune_default_libraries("%NAME%")

# copy config file if we don't have it
if (NOT EXISTS "${CMAKE_CURRENT_BINARY_DIR}/%NAME%.conf")
configure_file(${CMAKE_CURRENT_SOURCE_DIR}/%NAME%.conf ${CMAKE_CURRENT_BINARY_DIR}/%NAME%.conf COPYONLY)
endif()

# create vtk dir, as otherwise the executable just falls over
if (NOT EXISTS "${CMAKE_CURRENT_BINARY_DIR}/vtk")
file(MAKE_DIRECTORY "${CMAKE_CURRENT_BINARY_DIR}/vtk")
endif()
