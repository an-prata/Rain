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

	public float GetSlope(Vertex vertex, Axes axis)
	{
		return axis switch
		{
			Axes.X => vertex.X / X,
			Axes.Y => vertex.Y / Y,
			Axes.Z => vertex.Z / Z,
			_ => throw new Exception($"{axis} is not a valid Axis.")
		};
	}

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
		=> new((a.Y * b.Z) - (a.Z * b.Y), (a.Z * b.X) - (a.X * b.Z), (a.X * b.Y) - (a.Y * b.X), 1);

	public static bool operator ==(Vertex a, Vertex b) => a.Equals(b);

	public static bool operator !=(Vertex a, Vertex b) => !a.Equals(b);
} 