// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

/// <summary> 
/// Represents coordinates for texturing an object. 
/// </summary>
public class TextureCoordinate
{
	/// <summary> 
	/// The length of any array outputed by <c>TextureCoordinate.Array</c>. 
	/// </summary>
	public const int BufferSize = 2;

	/// <summary> 
	/// The TextureCoordinate's X cooridinate. 
	/// </summary>
	public float X { get; set; }

	/// <summary> 
	/// The TextureCoordinate's Y cooridinate. 
	/// </summary>
	public float Y { get; set; }

	/// <summary>
	/// Gets a <c>float[]</c> representing the current <c>TextureCoordinate</c>.
	/// </summary>
	/// 
	/// <value>
	/// An array of float in order X then Y.
	/// </value>
	public float[] Array { get => new float[BufferSize] { X, Y }; }

	/// <summary> 
	/// Creates a new TextureCoordinate from X and Y cooridinates. 
	/// </summary>
	/// 
	/// <param name="x"> 
	/// The X coordinate. 
	/// </param>
	/// 
	/// <param name="y"> 
	/// The Y coordinate. 
	/// </param>
	public TextureCoordinate(float x, float y)
	{
		X = x;
		Y = y;
	}

	/// <summary> 
	/// Creates a new coordinate from an array of floats. 
	/// </summary>
	/// 
	/// <remarks> 
	/// Note that this array must have length equal to <c>TextureCoordinate.BufferSize</c>. 
	/// </remarks>
	/// 
	/// <param name="coordinateArray"> 
	/// The array to assemble a TextureCoordinate from. 
	/// </param>
	public TextureCoordinate(float[] coordinateArray)
	{
		if (coordinateArray.Length != BufferSize)
			throw new Exception($"{nameof(coordinateArray)} was not of correct length {BufferSize}");
		
		X = coordinateArray[0];
		Y = coordinateArray[1];
	}

	public override int GetHashCode()
		=> Array.GetHashCode();
	
	public bool Equals(TextureCoordinate obj)
	{
		for (var i = 0; i < BufferSize; i++)
			if (Array[i] != obj.Array[i])
				return false;
	
		return true;
	}

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != typeof(TextureCoordinate))
			return false;

        return (TextureCoordinate)obj == this;
	}

	public static TextureCoordinate operator +(TextureCoordinate a, TextureCoordinate b) => new(a.X + b.X, a.Y + b.Y);

	public static TextureCoordinate operator -(TextureCoordinate a, TextureCoordinate b) => new(a.X - b.X, a.Y - b.Y);

	public static TextureCoordinate operator *(TextureCoordinate a, TextureCoordinate b) => new(a.X * b.X, a.Y * b.Y);

	public static TextureCoordinate operator /(TextureCoordinate a, TextureCoordinate b) => new(a.X / b.X, a.Y / b.Y);

	public static bool operator ==(TextureCoordinate a, TextureCoordinate b) => a.Equals(b);

	public static bool operator !=(TextureCoordinate a, TextureCoordinate b) => !a.Equals(b);
}