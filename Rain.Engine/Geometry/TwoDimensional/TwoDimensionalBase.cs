// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

public abstract class TwoDimensionalBase : ITwoDimensional, ISpacial
{
	private float width = 0.0f;

	private float height = 0.0f;

	private Angle rotationX;

	private Angle rotationY;

	private Angle rotationZ;

	public abstract uint[] Elements { get; }

	public abstract Point[] Points { get; set; }

	public abstract (Point, Point)[] Sides { get; }

	public virtual Vertex Location
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

	public Angle RotationX 
	{ 
		get => rotationX; 
		set => Rotate(value / rotationX, Axes.X); 
	}

	public Angle RotationY
	{ 
		get => rotationY; 
		set => Rotate(value / rotationY, Axes.Y); 
	}

	public Angle RotationZ 
	{ 
		get => rotationZ; 
		set => Rotate(value / rotationZ, Axes.Z); 
	}

	public TwoDimensionalBase(float width, float height)
	{
		this.width = width;
		this.height = height;

		rotationX = new() { Radians = 0.0f };
		rotationY = new() { Radians = 0.0f };
		rotationZ = new() { Radians = 0.0f };
	}

	public TwoDimensionalBase(float width, float height, Angle rotationX, Angle rotationY, Angle rotationZ)
	{
		this.width = width;
		this.height = height;

		this.rotationX = rotationX;
		this.rotationY = rotationY;
		this.rotationZ = rotationZ;
	}

	public double GetDistanceBetween(ISpacial other)
		=> (Location - other.Location).Maginitude;

	public Vertex GetCenterVertex()
	{
		var averageVertex = Points[0].Vertex;

		for (var point = 1; point < Points.Length; point++)
			averageVertex += Points[point].Vertex;
		
		averageVertex /= Points.Length;
		return averageVertex;
	}

	public abstract void CopyTo(out TwoDimensionalBase twoDimensional);

	public void Translate(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateTranslationMatrix(x, y, z)).Points;

	public void Translate(Vertex vertex)
		=> Translate(vertex.X, vertex.Y, vertex.Z);

	public void Scale(float x = 1, float y = 1)
	{
		var transform = TransformMatrix.CreateTranslationMatrix(Location);

		transform *= TransformMatrix.CreateRotationMatrix(-rotationX, Axes.X);
		transform *= TransformMatrix.CreateRotationMatrix(-rotationY, Axes.Y);
		transform *= TransformMatrix.CreateRotationMatrix(-rotationZ, Axes.Z);

		transform *= TransformMatrix.CreateScaleMatrix(x, y, 1);

		transform *= TransformMatrix.CreateRotationMatrix(rotationX, Axes.X);
		transform *= TransformMatrix.CreateRotationMatrix(rotationY, Axes.Y);
		transform *= TransformMatrix.CreateRotationMatrix(rotationZ, Axes.Z);
		
		transform *= TransformMatrix.CreateTranslationMatrix(-Location);

		Points = (this * transform).Points;

		width *= x;
		height *= y;
	}

	public void Rotate(Angle angle, Axes axis)
		=> Rotate(angle, axis, Location);

	public void Rotate(Angle angle, Axes axis, RotationDirection direction)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis, Location);
		else
			Rotate(-angle, axis, Location);
	}

	public void Rotate(Angle angle, Axes axis, Vertex vertex)
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

	public void Rotate(Angle angle, Axes axis, RotationDirection direction, Vertex vertex)
	{
		if (direction == RotationDirection.CounterClockwise)
			Rotate(angle, axis, vertex);
		else
			Rotate(-angle, axis, vertex);
	}
}