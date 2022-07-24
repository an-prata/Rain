// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using OpenTK.Mathematics;

namespace Rain.Engine.Geometry;

/// <summary> 
/// Represents the location of a point in 3D space. 
/// </summary>
public struct Vertex : ISpacial, IEquatable<Vertex>
{
	/// <summary> 
	/// The length of any array outputed by <c>Vertex.Array</c>. 
	/// </summary>
	public const int BufferSize = 4;

	/// <summary> 
	/// The Vertex's X coordinate. 
	/// </summary>
	public float X { get; set; }

	/// <summary> 
	/// The Vertex's Y coordinate. 
	/// </summary>
	public float Y { get; set; }

	/// <summary> 
	/// The Vertex's Z coordinate. 
	/// </summary>
	public float Z { get; set; }

	/// <summary> 
	/// The Vertex's W coordinate. 
	/// </summary>
	public float W { get; set; }

	public Vertex Location { get => this; }

	/// <summary>
	/// The magnitude of this <c>Vertex</c> as if it were a three element vector.
	/// </summary>
	/// 
	/// <remarks>
	/// Calculated as the square root of the sum of each X, Y, and Z coordinate.
	/// </remarks>
	public double Maginitude { get => Math.Sqrt((X * X) + (Y * Y) + (Z * Z)); }

	/// <summary>
	/// Gets a new <c>float[]</c> representing the current <c>Vertex</c>.
	/// </summary>
	/// 
	/// <value>
	/// A float array with values arranged as X, Y, Z, then W.
	/// </value>
	public float[] Array { get => new float[BufferSize] {X, Y, Z, W}; }

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
	/// 
	/// <param name="z"> 
	/// The Z coordinate. 
	/// </param>
	public Vertex(float x, float y, float z)
	{
		X = x;
		Y = y;
		Z = z;
		W = 1.0f;
	}

	/// <summary> 
	/// Creates a new Vertex from X, Y, Z, and W coordinates. 
	/// </summary>
	/// 
	/// <param name="x"> 
	/// The X coordinate. 
	/// </param>
	/// 
	/// <param name="y"> 
	/// The Y coordinate. 
	/// </param>
	/// 
	/// <param name="z"> 
	/// The Z coordinate. 
	/// </param>
	/// 
	/// <param name="w"> 
	/// The W coordinate. 
	/// </param>
	public Vertex(float x, float y, float z, float w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	/// <summary> 
	/// Creates a new vertex from an array of floats. 
	/// </summary>
	/// 
	/// <remarks> 
	/// Note that this array must have length equal to <c>Vertex.BufferSize</c>. 
	/// </remarks>
	/// 
	/// <param name="vertexArray"> 
	/// The array to assemble a Vertex from. 
	/// </param>
	public Vertex(float[] vertexArray)
	{
		if (vertexArray.Length != BufferSize)
			throw new ArgumentException($"{nameof(vertexArray)} must be of length {BufferSize}");
		
		X = vertexArray[0];
		Y = vertexArray[1];
		Z = vertexArray[2];
		W = vertexArray[3];
	}

	/// <summary> 
	/// Creates a new Vertex instance from an OpenTK Vector3. 
	/// </summary>
	/// 
	/// <param name="vector"> 
	/// The Vector3 to create a Vertex from. 
	/// </param>
	public Vertex(Vector3 vector)
	{
		X = vector.X;
		Y = vector.Y;
		Z = vector.Z;
		W = 1.0f;
	}

	public double GetDistanceBetween(ISpacial other)
		=> (Location - other.Location).Maginitude;
	
	/// <summary> 
	/// Produces an OpenTK Vector3 object from the Vertex. 
	/// </summary>
	/// 
	/// <returns> 
	/// An OpenTK Vector3. 
	/// </returns>
	public Vector3 ToVector3() => new(X, Y, Z);
	
	/// <summary>
	/// Gets a <c>Vertex</c> directly between this and another.
	/// </summary>
	public Vertex GetMidPoint(Vertex vertex)
		=> new((X + vertex.X) / 2, (Y + vertex.Y) / 2, (Z + vertex.Z) / 2);

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
	public static double ScalaarProduct(Vertex a, Vertex b)
		=> (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
	
	/// <summary>
	/// Calculates the product of multiplication as if both <c>Vertex</c> objects were column Vectors.
	/// </summary>
	/// 
	/// <remarks>
	/// This is the same as a vector product.
	/// </remarks>
	public static Vertex CrossProduct(Vertex a, Vertex b)
		=> new((a.Y * b.Z) - (a.Z * b.Y), (a.Z * b.X) - (a.X * b.Z), (a.X * b.Y) - (a.Y * b.X), 1);

	public override int GetHashCode()
		=> (X, Y, Z, W).GetHashCode();

	public bool Equals(Vertex vertex)
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

		if (obj.GetType() != typeof(Vertex))
			return false;

        return Equals((Vertex)obj);
	}

	public static Vertex operator -(Vertex a) => new(-a.X, -a.Y, -a.Z, a.W);

	// Vertex to float scalaar operations.
	public static Vertex operator +(Vertex a, float b) => new(a.X + b, a.Y + b, a.Z + b, 1.0f);

	public static Vertex operator -(Vertex a, float b) => new(a.X - b, a.Y - b, a.Z - b, 1.0f);

	public static Vertex operator *(Vertex a, float b) => new(a.X * b, a.Y * b, a.Z * b, 1.0f);

	public static Vertex operator /(Vertex a, float b) => new(a.X / b, a.Y / b, a.Z / b, 1.0f);

	// Vertex to double scalaar operations.
	public static Vertex operator +(Vertex a, double b) 
		=> new((float)(a.X + b), (float)(a.Y + b), (float)(a.Z + b), 1.0f);

	public static Vertex operator -(Vertex a, double b) 
		=> new((float)(a.X - b), (float)(a.Y - b), (float)(a.Z - b), 1.0f);

	public static Vertex operator *(Vertex a, double b) 
		=> new((float)(a.X * b), (float)(a.Y * b), (float)(a.Z * b), 1.0f);

	public static Vertex operator /(Vertex a, double b) 
		=> new((float)(a.X / b), (float)(a.Y / b), (float)(a.Z / b), 1.0f);

	// Vertex to Vertex vector style operations.
	public static Vertex operator +(Vertex a, Vertex b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, 1.0f);

	public static Vertex operator -(Vertex a, Vertex b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, 1.0f);

	public static Vertex operator *(Vertex a, Vertex b) 
		=> CrossProduct(a, b);

	public static bool operator ==(Vertex a, Vertex b) => a.Equals(b);

	public static bool operator !=(Vertex a, Vertex b) => !a.Equals(b);
} 