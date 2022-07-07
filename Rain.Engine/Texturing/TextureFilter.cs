// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Texturing;

public enum TextureFilter
{
	Nearest,

	Linear,

	NearestMipmap = OpenTK.Graphics.OpenGL.TextureMinFilter.NearestMipmapNearest,

	NearestMipmapFiltered = OpenTK.Graphics.OpenGL.TextureMinFilter.LinearMipmapNearest
}