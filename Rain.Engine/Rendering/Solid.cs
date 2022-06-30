using Rain.Engine.Buffering;
using Rain.Engine.Geometry;

namespace Rain.Engine.Rendering;

public class Solid : IRenderable
{
	private ITwoDimensional[] faces;

	private Vertex location;

	private float lengthX;

	private float lengthY;

	private float lengthZ;

	private float rotationX = 0;

	private float rotationY = 0;

	private float rotationZ = 0;

	public uint[] Elements => (uint[])GetBufferableArray(BufferType.ElementBuffer);

	public Point[] Points
	{
		get
		{
			var points = new List<Point>();

			for (var face = 0; face < faces.Length; face++)
				for (var point = 0; point < faces[face].Points.Length; point++)
					points.Add(faces[face].Points[point]);
			
			return points.ToArray();
		}

		set
		{
			var pointsWritten = 0;

			for (var face = 0; face < faces.Length; face++)
			{
				for (var point = 0; point < faces[face].Points.Length; point++)
					faces[face].Points[point] = value[pointsWritten + point];

				pointsWritten += faces[face].Points.Length;
			}
		}
	}

	public Vertex Location
	{
		get => location;
		set => Translate(value.X - location.X, value.Y - location.Y, value.Z - location.Z);
	}

	public float LengthX
	{
		get => lengthX;
		set => Scale(value / lengthX, 1, 1);
	}

	public float LengthY 
	{ 
		get => lengthY;
		set => Scale(1, value / lengthY, 1); 
	}

	public float LengthZ 
	{ 
		get => lengthZ;
		set => Scale(1, 1, value / lengthZ); 
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

	private Solid(ITwoDimensional[] faces)
		=> this.faces = faces;

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

	public Array GetBufferableArray(BufferType bufferType)
	{
		if (bufferType == BufferType.VertexBuffer)
		{
			var pointData = new List<float>();

			for (var point = 0; point < Points.Length; point++)
			{
				var pointArray = (float[])Points[point].GetBufferableArray(bufferType);

				for (var i = 0; i < Point.BufferSize; i++)
					pointData.Add(pointArray[i]);
			}

			return pointData.ToArray();
		}

		var points = 0;
		var elementData = new List<uint>();

		for (var face = 0; face < faces.Length; face++)
		{
			for (var element = 0; element < faces[face].Elements.Length; element++)
				elementData.Add((uint)points + faces[face].Elements[element]);
			
			points += faces[face].Points.Length;
		}

		return elementData.ToArray();
	}

	public int GetBufferSize(BufferType bufferType)
	{
		var bufferSize = 0;

		if (bufferType == BufferType.VertexBuffer)
			for (var face = 0; face < faces.Length; face++)
				bufferSize += faces[face].Points.Length * Point.BufferSize;
		
		else
			for (var face = 0; face < faces.Length; face++)
				bufferSize += faces[face].Elements.Length;

		return bufferSize;
	}

	public void Translate(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateTranslationMatrix(x, y, z)).Points;

	public void Translate(Vertex vertex)
		=> Translate(vertex.X, vertex.Y, vertex.Z);

	public void Scale(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateScaleMatrix(x, y, z)).Points;

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

	public void Rotate(float angle, Axes axis, RotationDirection direction, Vertex vertex)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis, vertex);
		else
			Rotate(-angle, axis, vertex);
	}

	public static Solid operator *(Solid a, TransformMatrix b) => b * a;

	public static Solid operator *(TransformMatrix a, Solid b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;

		return b;
	}
}