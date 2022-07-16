// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

public class Rectangle : ITwoDimensional
{
	private float rotationX = 0;

	private float rotationY = 0;

	private float rotationZ = 0;

	public uint[] Elements => new uint[] { 0, 1, 2, 1, 3, 2 };

	public Point[] Points { get; private set; }

	public Vertex Location
	{
		get => GetCenterVertex();
		set => Translate(value.X - Location.X, value.Y - Location.Y, value.Z - Location.Z);
	}

	public float Width
	{
		get => (float)Points[0].GetDistanceBetween(Points[1]);
		set => Scale(value / Width, 1);
	}

	public float Height 
	{ 
		get => (float)Points[0].GetDistanceBetween(Points[2]);
		set => Scale(1, value / Height); 
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

	public Rectangle(Point[] points)
	{
		if (points.Length != 4)
			throw new Exception($"{nameof(points)} is not length 4 (Given length: {points.Length}).");
		
		if (points[0].GetDistanceBetween(points[1]) != points[2].GetDistanceBetween(points[3]))
			throw new Exception($"{nameof(points)} does not make a Rectangle");
		
		if (points[1].GetDistanceBetween(points[2]) != points[3].GetDistanceBetween(points[0]))
			throw new Exception($"{nameof(points)} does not make a Rectangle");

		Points = points;
	}

	public Rectangle(Vertex location, float width, float height)
	{
		var halfWidth = width / 2;
		var halfHeight = height / 2;

		Points = new Point[]
		{
			new(new(location.X - halfWidth, location.Y - halfHeight, location.Z), new(255, 255, 255), new(0.0f, 0.0f)),
			new(new(location.X + halfWidth, location.Y - halfHeight, location.Z), new(255, 255, 255), new(1.0f, 0.0f)),
			new(new(location.X - halfWidth, location.Y + halfHeight, location.Z), new(255, 255, 255), new(0.0f, 1.0f)),
			new(new(location.X + halfWidth, location.Y + halfHeight, location.Z), new(255, 255, 255), new(1.0f, 1.0f))
		};
	}

	public Rectangle(Vertex location, float width, float height, Color color)
	{
		var halfWidth = width / 2;
		var halfHeight = height / 2;

		Points = new Point[]
		{
			new(new(location.X - halfWidth, location.Y - halfHeight, location.Z), color, new(0.0f, 0.0f)),
			new(new(location.X + halfWidth, location.Y - halfHeight, location.Z), color, new(1.0f, 0.0f)),
			new(new(location.X - halfWidth, location.Y + halfHeight, location.Z), color, new(0.0f, 1.0f)),
			new(new(location.X + halfWidth, location.Y + halfHeight, location.Z), color, new(1.0f, 1.0f))
		};
	}

	public Vertex GetCenterVertex()
	{
		var greatestPointX = Points[0].Vertex.X;
		var leastPointX = Points[0].Vertex.X;

		var greatestPointY = Points[0].Vertex.Y;
		var leastPointY = Points[0].Vertex.Y;

		var greatestPointZ = Points[0].Vertex.Z;
		var leastPointZ = Points[0].Vertex.Z;

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
		=> (Location - other.Location).Maginitude;

	public void Translate(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateTranslationMatrix(x, y, z)).Points;

	public void Translate(Vertex vertex)
		=> Translate(vertex.X, vertex.Y, vertex.Z);

	public void Scale(float x, float y)
		=> Points = (this * TransformMatrix.CreateScaleMatrix(x, y, 1)).Points;

	public void Scale(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateScaleMatrix(x, y, z)).Points;

	public void Rotate(float angle, Axes axis)
		=> Rotate(angle, axis, Location);

	public void Rotate(float angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis);
		else
			Rotate(-angle, axis);
	}

	public void Rotate(float angle, Axes axis, Vertex vertex)
	{
		var transform = TransformMatrix.CreateTranslationMatrix(vertex);
		transform *= TransformMatrix.CreateRotationMatrix(angle, axis);
		transform *= TransformMatrix.CreateTranslationMatrix(-vertex);

		Points = (this * transform).Points;

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