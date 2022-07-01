using Rain.Engine.Buffering;
using Rain.Engine.Geometry;

namespace Rain.Engine.Rendering;

public class Solid : IRenderable, IEquatable<Solid>
{
	private ITwoDimensional[] faces;

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
		get => GetCenterVertex();
		set => Translate(value.X - Location.X, value.Y - Location.Y, value.Z - Location.Z);
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

	private Solid(ITwoDimensional[] faces, float lengthX, float lengthY, float lengthZ)
	{
		this.lengthX = lengthX;
		this.lengthY = lengthY;
		this.lengthZ = lengthZ;
		this.faces = faces;
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
	{
		Points = (this * TransformMatrix.CreateScaleMatrix(x, y, z)).Points;
		lengthX *= x;
		lengthY *= y;
		lengthZ *= z;
	}

	public void Rotate(float angle, Axes axis)
		=> Rotate(angle, axis, GetCenterVertex());

	public void Rotate(float angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis, GetCenterVertex());
		else
			Rotate(-angle, axis, GetCenterVertex());
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

	public override int GetHashCode()
		=> GetBufferableArray(BufferType.VertexBuffer).GetHashCode();

	public bool Equals(Solid? other)
	{
		if (other is null)
			return false;
		
		var thisBufferableArray = (float[])GetBufferableArray(BufferType.VertexBuffer);
		var otherBufferableArray = (float[])other.GetBufferableArray(BufferType.VertexBuffer);

		if (thisBufferableArray.Length != otherBufferableArray.Length)
			return false;

		for (var i = 0; i < thisBufferableArray.Length; i++)
			if (thisBufferableArray[i] != otherBufferableArray[i])
				return false;
	
		return true;
	}

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != typeof(Vertex))
			return false;

        return Equals(obj);
	}

	public static Solid operator *(Solid a, TransformMatrix b) => b * a;

	public static Solid operator *(TransformMatrix a, Solid b)
	{
		for (var i = 0; i < b.Points.Length; i++)
			b.Points[i].Vertex *= a;

		return b;
	}

	public static bool operator ==(Solid a, Solid b) => a.Equals(b);

	public static bool operator !=(Solid a, Solid b) => !a.Equals(b);
}