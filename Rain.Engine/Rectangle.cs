using System.Numerics;

namespace Rain.Engine;

public class Rectangle<T> : IModel<T> where T : INumber<T>
{
	public Point<T>[] Points { get; }
	
	public Vertex<T> Location { get => Points[0].Vertex; }

	public uint[] Elements { get => new uint[] {0, 1, 2, 2, 1, 3}; }

	public T[] Array 
	{ 
		get => new T[4 * Vertex<T>.SizeInT + 4 * Color<T>.SizeInT]
		{
			Points[0].Vertex.X, Points[0].Vertex.Y, Points[0].Vertex.Z, 
			Points[0].Color.R, Points[0].Color.G, Points[0].Color.B, Points[0].Color.A,

			Points[1].Vertex.X, Points[1].Vertex.Y, Points[1].Vertex.Z, 
			Points[1].Color.R, Points[1].Color.G, Points[1].Color.B, Points[1].Color.A,

			Points[2].Vertex.X, Points[2].Vertex.Y, Points[2].Vertex.Z, 
			Points[2].Color.R, Points[2].Color.G, Points[2].Color.B, Points[2].Color.A,

			Points[3].Vertex.X, Points[3].Vertex.Y, Points[3].Vertex.Z, 
			Points[3].Color.R, Points[3].Color.G, Points[3].Color.B, Points[3].Color.A,
		};
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="location"></param>
	/// <param name="width"></param>
	/// <param name="height"></param>
	/// <param name="length"></param>//
	public Rectangle(Vertex<T> location, T width, T height, Color<T> color)
	{
		Points = new Point<T>[4]
		{
			new Point<T>(location, color),
			new Point<T>(new Vertex<T>(location.X + width, location.Y, location.Z), color),
			new Point<T>(new Vertex<T>(location.X, location.Y + height, location.Z), color),
			new Point<T>(new Vertex<T>(location.X + width, location.Y + height, location.Z), color)
		};
	}
}