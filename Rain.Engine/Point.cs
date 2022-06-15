using System.Numerics;

namespace Rain.Engine;

/// <summary> A colored point in 3D space that can be rendered by the GPU. </summary>
public struct Point<T> where T : INumber<T>
{
	/// <summary> The length of any array outputed by <c>Point<T>.Array</c>. </summary>
	public const int SizeInT = Color<T>.SizeInT + Vertex<T>.SizeInT;

	/// <summary> The color of the point. </summary>
	public Color<T> Color { get; set; }

	/// <summary> The location of the point in 3D space. </summary>
	public Vertex<T> Vertex { get; set; }

	/// <summary> An array representing the Vertex and Color data of this point. </summary>
	public T[] Array 
	{ 
		get 
		{
			T[] vertexData = new T[SizeInT];

			for (var i = 0; i < Vertex.Array.Length; i++)
				vertexData[i] = Vertex.Array[i];

			for (var i = 0; i < Color.Array.Length; i++)
				vertexData[i + Vertex<T>.SizeInT] = Color.Array[i];

			return vertexData;
		}
	}

	public Point(Vertex<T> vertex, Color<T> color)
	{
		Vertex = vertex;
		Color = color;
	}
}