// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Texturing;

public enum TextureWrapMode
{
	Clamp = OpenTK.Graphics.OpenGL.TextureWrapMode.ClampToEdge,

	Repeat = OpenTK.Graphics.OpenGL.TextureWrapMode.Repeat,

	MirroredRepeat = OpenTK.Graphics.OpenGL.TextureWrapMode.MirroredRepeat,
}