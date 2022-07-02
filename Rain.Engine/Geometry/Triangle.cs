namespace Rain.Engine.Geometry;

public class Triangle : ITwoDimensional
{
	private float width;

	private float height;

	private float rotationX = 0;

	private float rotationY = 0;

	private float rotationZ = 0;

	public uint[] Elements => new uint[] { 0, 1, 2 };

	public Point[] Points { get; private set; }

	public Vertex Location
	{
		get => GetCenterVertex();
		set => Translate(value.X - Location.X, value.Y - Location.Y, value.Z - Location.Z);
	}

	public float Width
	{
		get => width;
		set => Scale(value / width, 1);
	}

	public float Height 
	{ 
		get => height;
		set => Scale(1, value / height); 
	}

	public float RotationX 
	{ 
		get => rotationX; 
		set => Rotate(value / rotationX, Axes.X); 
	}

	public float RotationY
	{ 
		get => rotationY; 
		set => Rotate(value / rotationY, Axes.Y); 
	}

	public float RotationZ 
	{ 
		get => rotationZ; 
		set => Rotate(value / rotationZ, Axes.Z); 
	}

	public Triangle(Vertex location, float width, float height)
	{
		Points = new Point[]
		{
			new(location, new(255, 255, 255), new(0.0f, 0.0f)),
			new(new(location.X + width, location.Y, location.Z), new(255, 255, 255), new(1.0f, 0.0f)),
			new(new(location.X, location.Y + height, location.Z), new(255, 255, 255), new(0.0f, 1.0f)),
		};

		this.width = width;
		this.height = height;
	}

	public Triangle(Vertex location, float width, float height, Color color)
	{
		Points = new Point[]
		{
			new(location, color, new(0.0f, 0.0f)),
			new(new(location.X + width, location.Y, location.Z), color, new(1.0f, 0.0f)),
			new(new(location.X, location.Y + height, location.Z), color, new(0.0f, 1.0f)),
		};

		this.width = width;
		this.height = height;
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

	public double GetDistanceBetween(ISpacial other)
	{
		var difference = Location - other.Location;
		return Math.Sqrt(Math.Pow(difference.X, 2) + Math.Pow(difference.Y, 2) + Math.Pow(difference.Z, 2));
	}

	public void Translate(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateTranslationMatrix(x, y, z)).Points;

	public void Translate(Vertex vertex)
		=> Translate(vertex.X, vertex.Y, vertex.Z);

	public void Scale(float x, float y)
		=> Points = (this * TransformMatrix.CreateScaleMatrix(x, y, 1)).Points;

	public void Rotate(float angle, Axes axis)
		=> Rotate(angle, axis, GetCenterVertex());

	public void Rotate(float angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis);
		else
			Rotate(-angle, axis);
	}

	public void Rotate(float angle, Axes axis, Vertex vertex)
	{
		var center = GetCenterVertex();
		var distance = vertex - center;
		var rotationMatrix = TransformMatrix.CreateRotationMatrix(angle, axis);

		Translate(-(center.X + distance.X), -(center.Y + distance.Y), -(center.Z + distance.Z));
		Points = (this * rotationMatrix).Points;
		Translate(center.X + distance.X, center.Y + distance.Y, center.Z + distance.Z);

		switch(axis)
		{
			case Axes.X:
				rotationX += angle;
				break;

			case Axes.Y:
				rotationY += angle;
				break;

			case Axes.Z:
				rotationZ += angle;
				break;
		}
	}

	public void Rotate(float angle, Axes axis, RotationDirection direction, Vertex vertex)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis, vertex);
		else
			Rotate(-angle, axis, vertex);
	}
}