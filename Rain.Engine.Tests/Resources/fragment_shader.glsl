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