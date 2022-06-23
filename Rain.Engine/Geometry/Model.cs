namespace Rain.Engine.Geometry;

public abstract class Model : IModel
{
	public abstract int NumberOfPoints { get; }

	public abstract int NumberOfElements { get; }

	public abstract uint[] Elements { get; }

	public int BufferSize { get => Point.BufferSize * NumberOfPoints; }

	public Point[] Points { get; set; }

	public Vertex Location { get => Points[0].Vertex; }

	public float LengthX
	{ 
		get
		{
			var greatest = Points[0].Vertex.X;
			var least = Points[0].Vertex.X;

			foreach (var point in Points)
			{
				if (point.Vertex.X > greatest) greatest = point.Vertex.X;
				if (point.Vertex.X < least) least = point.Vertex.X;
			}

			return greatest - least;
		}
	}

	public float LengthY
	{ 
		get
		{
			var greatest = Points[0].Vertex.Y;
			var least = Points[0].Vertex.Y;

			foreach (var point in Points)
			{
				if (point.Vertex.Y > greatest) greatest = point.Vertex.Y;
				if (point.Vertex.Y < least) least = point.Vertex.Y;
			}

			return greatest - least;
		}
	}

	public float LengthZ
	{ 
		get
		{
			var greatest = Points[0].Vertex.Z;
			var least = Points[0].Vertex.Z;

			foreach (var point in Points)
			{
				if (point.Vertex.Z > greatest) greatest = point.Vertex.Z;
				if (point.Vertex.Z < least) least = point.Vertex.Z;
			}

			return greatest - least;
		}
	}

	public Model(Point[] points)
	{
		if (points.Length != NumberOfPoints)
			throw new Exception($"Parameter {nameof(points)} was not of length {NumberOfPoints}");

		Points = points;
	}

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

	public void Rotate(float angle, Axes axis)
	{
		var center = GetCenterVertex();
		var rotationMatrix = TransformMatrix.CreateRotationMatrix(angle, axis);
		var translationMatrix = TransformMatrix.CreateTranslationMatrix(-center.X, -center.Y, -center.Z);
		var inverseTranslationMatrix = TransformMatrix.CreateTranslationMatrix(center.X, center.Y, center.Z);

		var model = this * translationMatrix;
		model *= rotationMatrix;
		model *= inverseTranslationMatrix;

		Points = model.Points;
	}

	public void Rotate(float angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis);
		else
			Rotate(-angle, axis);
	}

	public static Model operator *(TransformMatrix a, Model b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;
			
		return b;
	}

	public static Model operator *(Model a, TransformMatrix b) => b * a;
}