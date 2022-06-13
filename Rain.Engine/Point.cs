using System.Numerics;

namespace Rain.Engine;

/// <summary> A colored point in 3D space that can be rendered by the GPU. </summary>
public struct Point<T> where T : INumber<T>
{
	/// <summary> The color of the point. </summary>
	public Color<T> Color { get; set; }

	/// <summary> The location of the point in 3D space. </summary>
	public Vertex<T> Vertex { get; set; }
}