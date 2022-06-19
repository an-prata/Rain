namespace Rain.Engine.Geometry;

public class Triangle : IModel
{
	public const int NumberOfVertices = 3;

	public const int NumberOfElements = 3;

	public const int BufferSize = Point.BufferSize * NumberOfElements;

	public Vertex Location { get => Points[0].Vertex; }

	public Point[] Points { get; }

	public uint[] Elements { get => new uint[NumberOfElements] { 0, 1, 2 }; }

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

	public Triangle(Vertex location, float width, float height, Color color)
	{
		Points = new Point[3]
		{
			new Point(location, color),
			new Point(new(location.X + width, location.Y, location.Z), color),
			new Point(new(location.X, location.Y + height, location.Z), color)
		};
	}

	public static Triangle operator *(TransformMatrix a, Triangle b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;
			
		return b;
	}

	public static Triangle operator *(Triangle a, TransformMatrix b) => b * a;
}