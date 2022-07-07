// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Graphics.OpenGL;

namespace Rain.Engine.Buffering;

public enum BufferType
{
	VertexBuffer = BufferTarget.ArrayBuffer,

	ElementBuffer = BufferTarget.ElementArrayBuffer
}