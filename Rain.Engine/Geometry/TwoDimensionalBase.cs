namespace Rain.Engine.Geometry;

public abstract class TwoDimensionalBase : ITwoDimensional
{
	public abstract int NumberOfPoints { get; }

	public abstract int NumberOfElements { get; }

	public abstract uint[] Elements { get; }

	public int BufferSize { get => Point.BufferSize * NumberOfPoints; }

	public Point[] Points { get; set; }

	public Vertex Location { get => Points[0].Vertex; }

	public float Width { get; private set; }

	public float Height { get; private set; }

	public float RotationX { get; private set; }

	public float RotationY { get; private set; }

	public float RotationZ { get; private set; }

	public TwoDimensionalBase(Point[] points)
	{
		if (points.Length != NumberOfPoints)
			throw new Exception($"Parameter {nameof(points)} was not of length {NumberOfPoints}");

		Points = points;

		RotationX = 0;
		RotationY = 0;
		RotationZ = 0;
	}

	// Saving for later.
	/* public float[] GetBufferableArray()
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
	} */

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

	public void Translate(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateTranslationMatrix(x, y, z)).Points;

	public void Translate(Vertex vertex) => Translate(vertex.X, vertex.Y, vertex.Z);

	public void Scale(float x, float y)
		=> Points = (this * TransformMatrix.CreateScaleMatrix(x, y, 1)).Points;

	public void Rotate(float angle, Axes axis)
	{
		var center = GetCenterVertex();
		var rotationMatrix = TransformMatrix.CreateRotationMatrix(angle, axis);

		Translate(-center.X, -center.Y, -center.Z);
		Points = (this * rotationMatrix).Points;
		Translate(center.X, center.Y, center.Z);

		switch(axis)
		{
			case Axes.X: 
				RotationX += angle; 
				break;
			
			case Axes.Y: 
				RotationY += angle; 
				break;
			
			case Axes.Z: 
				RotationZ += angle; 
				break;
		}
	}

	public void Rotate(float angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis);
		else
			Rotate(-angle, axis);
	}

	public static TwoDimensionalBase operator *(TwoDimensionalBase a, TransformMatrix b) => b * a;

	public static TwoDimensionalBase operator *(TransformMatrix a, TwoDimensionalBase b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;
			
		return b;
	}
}