// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

#version 450 core

out vec4 color;

in vec4 fragmentColor;
in vec2 textureCoordinate;

uniform bool textured;

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
    vec4 texel0 = texture(texture0, textureCoordinate);

    if (!textured)
    {
        color = fragmentColor;
    }
    else
    {
        texel0 = mix(texel0, texture(texture1, textureCoordinate), opacity1);
        texel0 = mix(texel0, texture(texture2, textureCoordinate), opacity2);
        texel0 = mix(texel0, texture(texture3, textureCoordinate), opacity3);
        texel0 = mix(texel0, texture(texture4, textureCoordinate), opacity4);
        texel0 = mix(texel0, texture(texture5, textureCoordinate), opacity5);
        texel0 = mix(texel0, texture(texture6, textureCoordinate), opacity6);
        texel0 = mix(texel0, texture(texture7, textureCoordinate), opacity7);
        texel0 = mix(texel0, texture(texture8, textureCoordinate), opacity8);
        texel0 = mix(texel0, texture(texture9, textureCoordinate), opacity9);
        texel0 = mix(texel0, texture(texture10, textureCoordinate), opacity10);
        texel0 = mix(texel0, texture(texture11, textureCoordinate), opacity11);
        texel0 = mix(texel0, texture(texture12, textureCoordinate), opacity12);
        texel0 = mix(texel0, texture(texture13, textureCoordinate), opacity13);
        texel0 = mix(texel0, texture(texture14, textureCoordinate), opacity14);
        texel0 = mix(texel0, texture(texture15, textureCoordinate), opacity15);

        color = texel0 * fragmentColor;
    }
} 