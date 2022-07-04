#version 330 core

out vec4 color;

in vec4 fragmentColor;
in vec2 textureCoordinate;

uniform float opacity0;
uniform float opacity1;
uniform float opacity2;
uniform float opacity3;
uniform float opacity4;
uniform float opacity5;
uniform float opacity6;
uniform float opacity7;
uniform float opacity8;
uniform float opacity9;
uniform float opacity10;
uniform float opacity11;
uniform float opacity12;
uniform float opacity13;
uniform float opacity14;
uniform float opacity15;

uniform sampler2D texture0;
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;
uniform sampler2D texture4;
uniform sampler2D texture5;
uniform sampler2D texture6;
uniform sampler2D texture7;
uniform sampler2D texture8;
uniform sampler2D texture9;
uniform sampler2D texture10;
uniform sampler2D texture11;
uniform sampler2D texture12;
uniform sampler2D texture13;
uniform sampler2D texture14;
uniform sampler2D texture15;

void main()
{
    color = texture(texture0, textureCoordinate) * fragmentColor;
} 