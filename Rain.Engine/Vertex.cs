using System.Numerics;

namespace Rain.Engine;

/// <summary> Represents the location of a point in 3D space. </summary>
public struct Vertex<T> where T : INumber<T>
{
	/// <summary> The Vertex's X cooridinate. </summary>
	public T X { get; set; }

	/// <summary> The Vertex's Y cooridinate. </summary>
	public T Y { get; set; }

	/// <summary> The Vertex's Z cooridinate. </summary>
	public T Z { get; set; }

	public Vertex(T x, T y, T z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public static Vertex<T> operator +(Vertex<T> a, Vertex<T> b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

	public static Vertex<T> operator -(Vertex<T> a, Vertex<T> b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

	public static Vertex<T> operator *(Vertex<T> a, Vertex<T> b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

	public static Vertex<T> operator /(Vertex<T> a, Vertex<T> b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
} 