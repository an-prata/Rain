#version 330 core

layout (location = 0) in vec3 vertexPosition; // the position variable has attribute position 0
layout (location = 1) in vec4 color;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec4 fragmentColor; // specify a color output to the fragment shader

void main()
{
    gl_Position = vec4(vertexPosition, 1.0) * model * view * projection; // see how we directly give a vec3 to vec4's constructor
    fragmentColor = color; // set the output variable to a dark-red color
}