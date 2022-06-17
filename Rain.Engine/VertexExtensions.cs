using OpenTK.Mathematics;

using static System.Math;

namespace Rain.Engine;

public static class VertexExtensions
{
	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<float> FromVector3(this Vertex<float> vertex, Vector3 vector)
	{
		vertex.X = vector.X;
		vertex.Y = vector.Y;
		vertex.Z = vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<double> FromVector3(this Vertex<double> vertex, Vector3 vector)
	{
		vertex.X = vector.X;
		vertex.Y = vector.Y;
		vertex.Z = vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<decimal> FromVector3(this Vertex<decimal> vertex, Vector3 vector)
	{
		vertex.X = (decimal)vector.X;
		vertex.Y = (decimal)vector.Y;
		vertex.Z = (decimal)vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<byte> FromVector3(this Vertex<byte> vertex, Vector3 vector)
	{
		vertex.X = (byte)vector.X;
		vertex.Y = (byte)vector.Y;
		vertex.Z = (byte)vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<ushort> FromVector3(this Vertex<ushort> vertex, Vector3 vector)
	{
		vertex.X = (ushort)vector.X;
		vertex.Y = (ushort)vector.Y;
		vertex.Z = (ushort)vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<uint> FromVector3(this Vertex<uint> vertex, Vector3 vector)
	{
		vertex.X = (uint)vector.X;
		vertex.Y = (uint)vector.Y;
		vertex.Z = (uint)vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<ulong> FromVector3(this Vertex<ulong> vertex, Vector3 vector)
	{
		vertex.X = (ulong)vector.X;
		vertex.Y = (ulong)vector.Y;
		vertex.Z = (ulong)vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<sbyte> FromVector3(this Vertex<sbyte> vertex, Vector3 vector)
	{
		vertex.X = (sbyte)vector.X;
		vertex.Y = (sbyte)vector.Y;
		vertex.Z = (sbyte)vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<short> FromVector3(this Vertex<short> vertex, Vector3 vector)
	{
		vertex.X = (short)vector.X;
		vertex.Y = (short)vector.Y;
		vertex.Z = (short)vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<int> FromVector3(this Vertex<int> vertex, Vector3 vector)
	{
		vertex.X = (int)vector.X;
		vertex.Y = (int)vector.Y;
		vertex.Z = (int)vector.Z;
		return vertex;
	}

	/// <summary> Makes a Vertex from an OpenTK Vector3 object. </summary>
	/// <param name="vector"> The Vector3 to make a Vertex from. </param>
	/// <returns> The created Vertex. </returns>
	public static Vertex<long> FromVector3(this Vertex<long> vertex, Vector3 vector)
	{
		vertex.X = (long)vector.X;
		vertex.Y = (long)vector.Y;
		vertex.Z = (long)vector.Z;
		return vertex;
	}

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<float> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<double> vertex)
		=> new((float)vertex.X, (float)vertex.Y, (float)vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<decimal> vertex)
		=> new((float)vertex.X, (float)vertex.Y, (float)vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<byte> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<ushort> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<uint> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<ulong> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<sbyte> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<short> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<int> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Produces an OpenTK Vector3 object from the Vertex. </summary>
	/// <returns> An OpenTK Vector3. </returns>
	public static Vector3 ToVector3(this Vertex<long> vertex)
		=> new(vertex.X, vertex.Y, vertex.Z);

	/// <summary> Finds the distance between two Vertex<float> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<float> a, Vertex<float> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<double> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<double> a, Vertex<double> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<float> instances. </summary>
	/// <remarks> Precision may be lost on this overload due the conversion from decimal to double</remarks>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<decimal> a, Vertex<decimal> b)
	{
		var difference = a - b;
		return Sqrt(Pow((double)difference.X, 2) + Pow((double)difference.Y, 2) + Pow((double)difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<byte> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<byte> a, Vertex<byte> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<ushort> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<ushort> a, Vertex<ushort> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<ulong> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<ulong> a, Vertex<ulong> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<sbyte> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<sbyte> a, Vertex<sbyte> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<short> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<short> a, Vertex<short> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<int> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<int> a, Vertex<int> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}

	/// <summary> Finds the distance between two Vertex<long> instances. </summary>
	/// <returns> The distance between the two vertices. </returns>
	public static double DistanceBetween(this Vertex<long> a, Vertex<long> b)
	{
		var difference = a - b;
		return Sqrt(Pow(difference.X, 2) + Pow(difference.Y, 2) + Pow(difference.Z, 2));
	}
}