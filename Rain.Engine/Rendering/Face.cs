// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;
using Rain.Engine.Buffering;

namespace Rain.Engine.Rendering;

/// <summary>
/// A two dimensional face.
/// </summary>
public class Face : TwoDimensionalBase, ITwoDimensional
{
	private readonly TwoDimensionalBase face;

	public override uint[] Elements => face.Elements;

	public override Point[] Points 
	{
		get => face.Points;
		set => face.Points = value;
	}

	public override int Sides { get => face.Sides; }

	public Face(TwoDimensionalBase face) :
		base(face.Width, face.Height, face.RotationX, face.RotationY, face.RotationZ)
	{
		face.CopyTo(out this.face);
	}

	private Face(Face face) :
		base(face.Width, face.Height, face.RotationX, face.RotationY, face.RotationZ)
	{
		face.face.CopyTo(out this.face);
	}

	public TwoDimensionalBase GetTwoDimensional()
		=> face;

	public int GetBufferSize(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
			return face.Points.Length * Point.BufferSize;

		return face.Elements.Length;
	}

	public Array GetBufferableArray(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
		{
			var bufferableArray = new float[face.Points.Length * Point.BufferSize];

			for (var point = 0; point < face.Points.Length; point++)
			{
				var pointArray = (float[])face.Points[point].GetBufferableArray(bufferType);

				for (var i = 0; i < pointArray.Length; i++)
					bufferableArray[(point * Point.BufferSize) + i] = pointArray[i];
			}

			return bufferableArray;
		}
		else
		{
			return face.Elements;
		}
	}

	/// <summary>
	/// Copies data from this <c>Face</c> to another.
	/// </summary>
	/// 
	/// <param name="face">
	/// The <c>Face</c> to copy data to.
	/// </param>
	public override void CopyTo(out TwoDimensionalBase face)
		=> face = new Face(this);
}