#version 330 core

out vec4 color;

in vec4 fragmentColor;
in vec2 textureCoordinate;

uniform sampler2D texture0;

void main()
{
    color = texture(texture0, textureCoordinate) * fragmentColor;
} 