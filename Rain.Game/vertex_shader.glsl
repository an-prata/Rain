// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

#version 450 core

layout (location = 0) in vec3 vertexPosition;
layout (location = 1) in vec4 color;
layout (location = 2) in vec2 texturePosition;

out vec4 fragmentColor;
out vec2 textureCoordinate;

uniform mat4 perspectiveProjection;
uniform mat4 cameraTransform;

void main()
{
    textureCoordinate = texturePosition;
    fragmentColor = color;
    gl_Position = vec4(vertexPosition, 1.0) * cameraTransform * perspectiveProjection;
}