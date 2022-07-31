// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine.Geometry;

namespace Rain.Engine.Geometry.TwoDimensional;

/// <summary> 
/// Represents the location of a point in 3D space. 
/// </summary>
public struct TwoDimensionalVertex : IEquatable<TwoDimensionalVertex>
{
	/// <summary> 
	/// The length of any array outputed by <c>Vertex.Array</c>. 
	/// </summary>
	public const int BufferSize = 2;

	/// <summary> 
	/// The Vertex's X coordinate. 
	/// </summary>
	public double X { get; set; }

	/// <summary> 
	/// The Vertex's Y coordinate. 
	/// </summary>
	public double Y { get; set; }

	/// <summary>
	/// The magnitude of this <c>Vertex</c> as if it were a three element vector.
	/// </summary>
	/// 
	/// <remarks>
	/// Calculated as the square root of the sum of each X, Y, and Z coordinate.
	/// </remarks>
	public double Maginitude { get => Math.Sqrt((X * X) + (Y * Y)); }

	/// <summary>
	/// Gets a new <c>double[]</c> representing the current <c>Vertex</c>.
	/// </summary>
	/// 
	/// <value>
	/// A double array with values arranged as X, Y, Z, then W.
	/// </value>
	public double[] Array { get => new double[BufferSize] {X, Y}; }

	/// <summary> 
	/// Creates a new Vertex from X, Y, and Z coordinates. 
	/// </summary>
	/// 
	/// <param name="x"> 
	/// The X coordinate. 
	/// </param>
	/// 
	/// <param name="y"> 
	/// The Y coordinate. 
	/// </param>
	public TwoDimensionalVertex(double x, double y)
	{
		X = x;
		Y = y;
	}

	/// <summary> 
	/// Creates a new vertex from an array of doubles. 
	/// </summary>
	/// 
	/// <remarks> 
	/// Note that this array must have length equal to <c>Vertex.BufferSize</c>. 
	/// </remarks>
	/// 
	/// <param name="vertexArray"> 
	/// The array to assemble a Vertex from. 
	/// </param>
	public TwoDimensionalVertex(double[] vertexArray)
	{
		if (vertexArray.Length != BufferSize)
			throw new ArgumentException($"{nameof(vertexArray)} must be of length {BufferSize}");
		
		X = vertexArray[0];
		Y = vertexArray[1];
	}

	public TwoDimensionalVertex(Angle angle)
	{
		// See differential equation definition section of https://en.wikipedia.org/wiki/Sine_and_cosine.
		// Specifically the gif (https://en.wikipedia.org/wiki/File:Circle_cos_sin.gif).

		X = Math.Cos(angle.Radians);
		Y = Math.Sin(angle.Radians);
	}

	public double GetDistanceBetween(TwoDimensionalVertex other)
		=> (this - other).Maginitude;
	
	/// <summary>
	/// Gets a <c>Vertex</c> directly between this and another.
	/// </summary>
	public TwoDimensionalVertex GetMidPoint(TwoDimensionalVertex vertex)
		=> new((X + vertex.X) / 2, (Y + vertex.Y) / 2);

	/// <summary>
	/// Calculates the product of multiplication as if the two <c>Vertex</c> objects were a column and row vector.
	/// </summary>
	/// 
	/// <remarks>
	/// This is the same as the dot product of two vectors and is notated with a dot at middle height between the two
	/// operands, hence the name dot product.
	/// </remarks>
	/// 
	/// <param name="a">
	/// The "row" Vector.
	/// </param>
	/// 
	/// <param name="b">
	/// The "column" Vector.
	/// </param>
	public static double ScalaarProduct(TwoDimensionalVertex a, TwoDimensionalVertex b) => (a.X * b.X) + (a.Y * b.Y);

	public override int GetHashCode() => (X, Y).GetHashCode();

	public bool Equals(TwoDimensionalVertex vertex)
	{
		for (var i = 0; i < BufferSize; i++)
			if (Array[i] != vertex.Array[i])
				return false;
	
		return true;
	}

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != typeof(TwoDimensionalVertex))
			return false;

        return Equals((TwoDimensionalVertex)obj);
	}

	public static TwoDimensionalVertex operator -(TwoDimensionalVertex a) => new(-a.X, -a.Y);

	// TwoDimensionalVertex to float scalaar operations.
	public static TwoDimensionalVertex operator +(TwoDimensionalVertex a, float b) => new(a.X + b, a.Y + b);

	public static TwoDimensionalVertex operator -(TwoDimensionalVertex a, float b) => new(a.X - b, a.Y - b);

	public static TwoDimensionalVertex operator *(TwoDimensionalVertex a, float b) => new(a.X * b, a.Y * b);

	public static TwoDimensionalVertex operator /(TwoDimensionalVertex a, float b) => new(a.X / b, a.Y / b);

	// Vertex to double scalaar operations.
	public static TwoDimensionalVertex operator +(TwoDimensionalVertex a, double b) 
		=> new(a.X + b, a.Y + b);

	public static TwoDimensionalVertex operator -(TwoDimensionalVertex a, double b) 
		=> new((float)(a.X - b), (float)(a.Y - b));

	public static TwoDimensionalVertex operator *(TwoDimensionalVertex a, double b) 
		=> new((float)(a.X * b), (float)(a.Y * b));

	public static TwoDimensionalVertex operator /(TwoDimensionalVertex a, double b) 
		=> new((float)(a.X / b), (float)(a.Y / b));

	// Vertex to Vertex vector style operations.
	public static TwoDimensionalVertex operator +(TwoDimensionalVertex a, TwoDimensionalVertex b) => new(a.X + b.X, a.Y + b.Y);

	public static TwoDimensionalVertex operator -(TwoDimensionalVertex a, TwoDimensionalVertex b) => new(a.X - b.X, a.Y - b.Y);

	public static bool operator ==(TwoDimensionalVertex a, TwoDimensionalVertex b) => a.Equals(b);

	public static bool operator !=(TwoDimensionalVertex a, TwoDimensionalVertex b) => !a.Equals(b);
} 