using System.Numerics;

namespace Rain.Engine;

/// <summary> Represents the location of a point in 3D space. </summary>
public struct Vertex<T> where T : INumber<T>
{
	/// <summary> The length of any array outputed by <c>Vertex<T>.Array</c>. </summary>
	public const int SizeInT = 3;

	/// <summary> The Vertex's X cooridinate. </summary>
	public T X { get; set; }

	/// <summary> The Vertex's Y cooridinate. </summary>
	public T Y { get; set; }

	/// <summary> The Vertex's Z cooridinate. </summary>
	public T Z { get; set; }

	public T[] Array { get => new T[3] {X, Y, Z}; }

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