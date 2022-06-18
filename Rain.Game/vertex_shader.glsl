#version 330 core

layout (location = 0) in vec4 vertexPosition; // the position variable has attribute position 0
layout (location = 1) in vec4 color;

out vec4 fragmentColor; // specify a color output to the fragment shader

void main()
{
    gl_Position = vertexPosition; 
    fragmentColor = color;
}