namespace Rain.Engine.Geometry;

public class Rectangle : IModel
{
	public const int NumberOfVertices = 4;

	public const int NumberOfElements = 6;

	public const int BufferSize = Point.BufferSize * NumberOfElements;

	public Vertex Location { get => Points[0].Vertex; }
	
	public Point[] Points { get; }

	public uint[] Elements { get => new uint[] {0, 1, 2, 2, 1, 3}; }

	public float[] Array 
	{ 
		get
		{
			var array = new float[BufferSize];

			for (var point = 0; point < Points.Length; point++)
			{
				var i = point * (Vertex.BufferSize + Color.BufferSize);

				for (var coordinate = 0; coordinate < Vertex.BufferSize; coordinate++)
				{
					array[i] = Points[point].Vertex.Array[coordinate];
					i++;
				}

				for (var component = 0; component < Color.BufferSize; component++)
				{
					array[i] = Points[point].Color.Array[component];
					i++;
				}
			}

			return array;
		}
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