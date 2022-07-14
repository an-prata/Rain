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

	public Vertex Location { get => this; }

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
	/// Creates a new vertex from an array fo floats. 
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
	{
		var difference = Location - other.Location;
		return Math.Sqrt(Math.Pow(difference.X, 2) + Math.Pow(difference.Y, 2) + Math.Pow(difference.Z, 2));
	}
	
	/// <summary> 
	/// Produces an OpenTK Vector3 object from the Vertex. 
	/// </summary>
	/// 
	/// <returns> 
	/// An OpenTK Vector3. 
	/// </returns>
	public Vector3 ToVector3() => new(X, Y, Z);

	/// <summary>
	/// Gets the maginitude of this <c>Vertex</c> as if it were a three element Vector.
	/// </summary>
	/// 
	/// <remarks>
	/// Notated as |vector|.
	/// </remarks>
	public double GetMagnitude()
		=> Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

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

	/// <summary>
	/// Gets a <c>Vertex</c> representing the origin in three dimensional space.
	/// </summary>
	public static Vertex GetOrigin()
		=> new(0.0f, 0.0f, 0.0f, 1.0f);

	public override int GetHashCode()
		=> Array.GetHashCode();

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

        return Equals(obj);
	}

	public static Vertex operator +(Vertex a, Vertex b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);

	public static Vertex operator -(Vertex a, Vertex b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);

	public static Vertex operator *(Vertex a, Vertex b) 
		=> CrossProduct(a, b);

	public static bool operator ==(Vertex a, Vertex b) => a.Equals(b);

	public static bool operator !=(Vertex a, Vertex b) => !a.Equals(b);
} 