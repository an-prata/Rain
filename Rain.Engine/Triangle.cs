using OpenTK.Mathematics;

namespace Rain.Engine;

public class Triangle : IModel
{
	public Vertex Location { get => Points[0].Vertex; }

	public Point[] Points { get; }

	public uint[] Elements { get => new uint[3] { 0, 1, 2 }; }

	public float[] Array 
	{ 
		get => new float[3 * Vertex.BufferSize + 3 * Color.BufferSize]
		{
			Points[0].Vertex.X, Points[0].Vertex.Y, Points[0].Vertex.Z, Points[0].Vertex.W,
			Points[0].Color.R, Points[0].Color.G, Points[0].Color.B, Points[0].Color.A,

			Points[1].Vertex.X, Points[1].Vertex.Y, Points[1].Vertex.Z, Points[0].Vertex.W,
			Points[1].Color.R, Points[1].Color.G, Points[1].Color.B, Points[1].Color.A,

			Points[2].Vertex.X, Points[2].Vertex.Y, Points[2].Vertex.Z, Points[0].Vertex.W,
			Points[2].Color.R, Points[2].Color.G, Points[2].Color.B, Points[2].Color.A
		};
	}

	public Triangle(Vertex location, float width, float height, Color color)
	{
		Points = new Point[3]
		{
			new Point(location, color),
			new Point(new(location.X + width, location.Y, location.Z), color),
			new Point(new(location.X, location.Y + height, location.Z), color)
		};
	}
}