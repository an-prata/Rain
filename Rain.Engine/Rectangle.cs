using System.Numerics;

namespace Rain.Engine;

public class Rectangle : IModel
{
	public Vertex Location { get => Points[0].Vertex; }
	
	public Point[] Points { get; }

	public uint[] Elements { get => new uint[] {0, 1, 2, 2, 1, 3}; }

	public float[] Array 
	{ 
		get => new float[4 * Vertex.BufferSize + 4 * Color.BufferSize]
		{
			Points[0].Vertex.X, Points[0].Vertex.Y, Points[0].Vertex.Z, Points[0].Vertex.W,
			Points[0].Color.R, Points[0].Color.G, Points[0].Color.B, Points[0].Color.A,

			Points[1].Vertex.X, Points[1].Vertex.Y, Points[1].Vertex.Z, Points[0].Vertex.W,
			Points[1].Color.R, Points[1].Color.G, Points[1].Color.B, Points[1].Color.A,

			Points[2].Vertex.X, Points[2].Vertex.Y, Points[2].Vertex.Z, Points[0].Vertex.W,
			Points[2].Color.R, Points[2].Color.G, Points[2].Color.B, Points[2].Color.A,

			Points[3].Vertex.X, Points[3].Vertex.Y, Points[3].Vertex.Z, Points[0].Vertex.W,
			Points[3].Color.R, Points[3].Color.G, Points[3].Color.B, Points[3].Color.A,
		};
	}

	/// <summary> Creates a new Rectangle. </summary>
	/// <param name="location"> The location of the Rectangle and the point with the smallest X, Y, and Z values. </param>
	/// <param name="width"> The rectangle's length along the X axis. </param>
	/// <param name="height"> The rectangle's length along the Y axis.</param>
	/// <param name="color"> The color of the rectangle. </param>
	public Rectangle(Vertex location, float width, float height, Color color)
	{
		Points = new Point[4]
		{
			new Point(location, color),
			new Point(new Vertex(location.X + width, location.Y, location.Z), color),
			new Point(new Vertex(location.X, location.Y + height, location.Z), color),
			new Point(new Vertex(location.X + width, location.Y + height, location.Z), color)
		};
	}

	public static Rectangle operator *(TransformMatrix a, Rectangle b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;
			
		return b;
	}

	public static Rectangle operator *(Rectangle a, TransformMatrix b) => b * a;
}