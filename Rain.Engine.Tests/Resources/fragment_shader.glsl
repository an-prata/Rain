// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

#version 330 core

out vec4 color;

in vec4 fragmentColor;
in vec2 textureCoordinate;

uniform sampler2D texture0;
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;

void main()
{
    color = texture(texture0, textureCoordinate) * fragmentColor;
} 