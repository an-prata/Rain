// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

#version 330 core

layout (location = 0) in vec4 vertexPosition;
layout (location = 1) in vec4 color;
layout (location = 2) in vec2 texturePosition;

out vec4 fragmentColor;
out vec2 textureCoordinate;

uniform mat4 perspectiveProjection;

void main()
{
    textureCoordinate = texturePosition;
    fragmentColor = color;
    gl_Position = vertexPosition * perspectiveProjection; 
}