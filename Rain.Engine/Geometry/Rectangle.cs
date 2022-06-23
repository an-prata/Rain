namespace Rain.Engine.Geometry;

public class Rectangle : IModel
{
	public const int NumberOfVertices = 4;

	public const int NumberOfElements = 6;

	public const int BufferSize = Point.BufferSize * NumberOfVertices;

	public Vertex Location { get => Points[0].Vertex; }
	
	public Point[] Points { get; private set; }

	public uint[] Elements { get => new uint[NumberOfElements] {0, 1, 2, 1, 3, 2}; }

	public float[] GetBufferableArray() 
	{ 
		var array = new float[BufferSize];

		for (var point = 0; point < Points.Length; point++)
		{
			var pointIndex = point * (Vertex.BufferSize + Color.BufferSize + TextureCoordinate.BufferSize);

			for (var coordinate = 0; coordinate < Vertex.BufferSize; coordinate++)
			{
				array[pointIndex] = Points[point].Vertex.Array[coordinate];
				pointIndex++;
			}

			for (var component = 0; component < Color.BufferSize; component++)
			{
				array[pointIndex] = Points[point].Color.Array[component];
				pointIndex++;
			}

			for (var coordinate = 0; coordinate < TextureCoordinate.BufferSize; coordinate++)
			{
				array[pointIndex] = Points[point].TextureCoordinate.Array[coordinate];
				pointIndex++;
			}
		}

		return array;
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
			new Point(location, color, new(0.0f, 0.0f)),
			new Point(new Vertex(location.X + width, location.Y, location.Z), color, new(1.0f, 0.0f)),
			new Point(new Vertex(location.X, location.Y + height, location.Z), color, new(0.0f, 1.0f)),
			new Point(new Vertex(location.X + width, location.Y + height, location.Z), color, new(1.0f, 1.0f))
		};
	}

	public void Rotate(float angle, Axes axis)
	{
		var center = GetCenterVertex();
		var rotationMatrix = TransformMatrix.CreateRotationMatrix(angle, axis);
		var translationMatrix = TransformMatrix.CreateTranslationMatrix(-center.X, -center.Y, -center.Z);
		var inverseTranslationMatrix = TransformMatrix.CreateTranslationMatrix(center.X, center.Y, center.Z);

		var rectangle = this * translationMatrix;
		rectangle *= rotationMatrix;
		rectangle *= inverseTranslationMatrix;

		Points = rectangle.Points;
	}

	public void Rotate(float angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis);
		else
			Rotate(-angle, axis);
	}

	public Vertex GetCenterVertex()
	{
		var greatestPointX = 0.0f;
		var leastPointX = 0.0f;
		
		var greatestPointY = 0.0f;
		var leastPointY = 0.0f;

		var greatestPointZ = 0.0f;
		var leastPointZ = 0.0f;

		for (var point = 0; point < Points.Length; point++)
		{
			if (Points[point].Vertex.X > greatestPointX)
				greatestPointX = Points[point].Vertex.X;
			else if (Points[point].Vertex.X < leastPointX)
				leastPointX = Points[point].Vertex.X;
			
			if (Points[point].Vertex.Y > greatestPointY)
				greatestPointY = Points[point].Vertex.Y;
			else if (Points[point].Vertex.Y < leastPointY)
				leastPointY = Points[point].Vertex.Y;

			if (Points[point].Vertex.Z > greatestPointZ)
				greatestPointZ = Points[point].Vertex.Z;
			else if (Points[point].Vertex.Z < leastPointZ)
				leastPointZ = Points[point].Vertex.Z;
		}

		var midPointX = (greatestPointX + leastPointX) / 2;
		var midPointY = (greatestPointY + leastPointY) / 2;
		var midPointZ = (greatestPointZ + leastPointZ) / 2;

		return new Vertex(midPointX, midPointY, midPointZ);
	}

	public static Rectangle operator *(TransformMatrix a, Rectangle b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;
			
		return b;
	}

	public static Rectangle operator *(Rectangle a, TransformMatrix b) => b * a;
}