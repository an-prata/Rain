using static System.Math;

namespace Rain.Engine;

public static class VertexExtensions
{
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