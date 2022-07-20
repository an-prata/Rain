// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

/// <summary>
/// A class for creating and manipulating triangles.
/// </summary>
public class Triangle : ISpacial, ITwoDimensional
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

	/// <summary>
	/// Creates a new <c>Triangle</c> from an array of <c>Point</c> objects.
	/// </summary>
	/// 
	/// <param name="points">
	/// An array of <c>Points</c>s representing a triangle.
	/// </param>
	public Triangle(Point[] points)
	{
		if (points.Length != 3)
			throw new Exception($"{nameof(points)} is not length 3 (Given length: {points.Length}).");

		Points = points;
		width = (float)points[0].GetDistanceBetween(points[1]);
		height = (float)points[0].Vertex.GetMidPoint(points[1].Vertex).GetDistanceBetween(points[2].Vertex);
	}

	/// <summary>
	/// Creates a <c>Triangle</c> that has a right angle from a location, height, and width.
	/// </summary>
	/// 
	/// <param name="location">
	/// The location of the <c>Triangle</c>, will also be the <c>Point</c> with the right angle.
	/// </param>
	/// 
	/// <param name="width">
	/// The <c>Triangle</c>'s width.
	/// </param>
	/// 
	/// <param name="height">
	/// The <c>Triangle</c>'s height.
	/// </param>
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

	/// <summary>
	/// Creates a <c>Triangle</c> that has a right angle from a location, height, width, and color.
	/// </summary>
	/// 
	/// <param name="location">
	/// The location of the <c>Triangle</c>, will also be the <c>Point</c> with the right angle.
	/// </param>
	/// 
	/// <param name="width">
	/// The <c>Triangle</c>'s width.
	/// </param>
	/// 
	/// <param name="height">
	/// The <c>Triangle</c>'s height.
	/// </param>
	/// 
	/// <param name="color">
	/// The <c>Triangle</c>'s color.
	/// </param>
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

	private Triangle(Triangle triangle)
	{
		width = triangle.width;
		height = triangle.height;

		rotationX = triangle.rotationX;
		rotationY = triangle.rotationY;
		rotationZ = triangle.rotationZ;

		Points = new Point[triangle.Points.Length];
		triangle.Points.CopyTo(Points, 0);
	}

	public double GetDistanceBetween(ISpacial other)
		=> (Location - other.Location).Maginitude;

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

	public void CopyTo(out ITwoDimensional triangle)
		=> triangle = new Triangle(this);

	public void Translate(float x, float y, float z)
		=> Points = (this * TransformMatrix.CreateTranslationMatrix(x, y, z)).Points;

	public void Translate(Vertex vertex)
		=> Translate(vertex.X, vertex.Y, vertex.Z);

	public void Scale(float x, float y)
	{
		Points = (this * TransformMatrix.CreateScaleMatrix(x, y, 1)).Points;
		width *= x;
		height *= y;
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