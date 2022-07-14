// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Buffering;

namespace Rain.Engine.Texturing;

/// <summary>
/// A two dimensional face with a set of one or more <c>Textures</c>.
/// </summary>
public struct TexturedFace : IBufferable
{
	/// <summary>
	/// The Face that will be textured.
	/// </summary>
	public ITwoDimensional Face { get; set; }

	/// <summary>
	/// The <c>Textures</c> that will be rendered onto <c>Face</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// <c>Texture</c>s are applied in ascending order.
	/// </remarks>
	public Texture[] Textures { get; set; }

	public TexturedFace(TexturedFace face)
	{
		Face = face.Face;
		Textures = face.Textures;
	}

	public int GetBufferSize(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
			return Face.Points.Length * Point.BufferSize;

		return Face.Elements.Length;
	}

	public Array GetBufferableArray(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
		{
			var bufferableArray = new float[Face.Points.Length * Point.BufferSize];

			for (var point = 0; point < Face.Points.Length; point++)
			{
				var pointArray = (float[])Face.Points[point].GetBufferableArray(bufferType);

				for (var i = 0; i < pointArray.Length; i++)
					bufferableArray[(point * Point.BufferSize) + i] = pointArray[i];
			}

			return bufferableArray;
		}
		else
		{
			return Face.Elements;
		}
	}
}