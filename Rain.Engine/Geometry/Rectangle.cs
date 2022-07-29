// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

/// <summary>
/// A class for creating and manipulating rectangles.
/// </summary>
public class Rectangle : TwoDimensionalBase
{
	public override uint[] Elements => new uint[] { 0, 1, 2, 0, 2, 3 };

	public override Point[] Points { get; set; }

	public override int Sides { get => 4; }

	/// <summary>
	/// Creates a new <c>Rectangle</c> from an array of <c>Point</c> objects.
	/// </summary>
	/// 
	/// <param name="points">
	/// An array of <c>Point</c>s representing a rectangle.
	/// </param>
	public Rectangle(Point[] points) :
		base((float)points[0].GetDistanceBetween(points[3]), (float)points[0].GetDistanceBetween(points[1]))
	{
		if (points.Length != 4)
			throw new Exception($"{nameof(points)} is not length 4 (Given length: {points.Length}).");
		
		if (points[0].GetDistanceBetween(points[1]) != points[2].GetDistanceBetween(points[3]))
			throw new Exception($"{nameof(points)} does not make a Rectangle");
		
		if (points[1].GetDistanceBetween(points[2]) != points[3].GetDistanceBetween(points[0]))
			throw new Exception($"{nameof(points)} does not make a Rectangle");

		Points = new Point[points.Length];
		points.CopyTo(Points, 0);
	}

	/// <summary>
	/// Creates a new <c>Rectangle</c> from a desired location, height, and width.
	/// </summary>
	/// 
	/// <param name="location">
	/// The location of the <c>Rectangle</c>, this will be positioned at it's center.
	/// </param>
	/// 
	/// <param name="width">
	/// The <c>Rectangle</c>'s width.
	/// </param>
	/// 
	/// <param name="height">
	/// The <c>Rectangle</c>'s height.
	/// </param>
	public Rectangle(Vertex location, float width, float height) :
		base(width, height)
	{
		var halfWidth = width / 2;
		var halfHeight = height / 2;

		Points = new Point[]
		{
			new(new(location.X - halfWidth, location.Y - halfHeight, location.Z), new TextureCoordinate(1.0f, 0.0f)),
			new(new(location.X + halfWidth, location.Y - halfHeight, location.Z),  new TextureCoordinate(0.0f, 0.0f)),
			new(new(location.X + halfWidth, location.Y + halfHeight, location.Z),  new TextureCoordinate(0.0f, 1.0f)),
			new(new(location.X - halfWidth, location.Y + halfHeight, location.Z),  new TextureCoordinate(1.0f, 1.0f))
		};
	}

	/// <summary>
	/// Creates a new <c>Rectangle</c> from a desired location, height, width, and color.
	/// </summary>
	/// 
	/// <param name="location">
	/// The location of the <c>Rectangle</c>, this will be positioned at it's center.
	/// </param>
	/// 
	/// <param name="width">
	/// The <c>Rectangle</c>'s width.
	/// </param>
	/// 
	/// <param name="height">
	/// The <c>Rectangle</c>'s height.
	/// </param>
	/// 
	/// <param name="color">
	/// The <c>Rectangle</c>'s color.
	/// </param>
	public Rectangle(Vertex location, float width, float height, Color color) :
		base(width, height)
	{
		var halfWidth = width / 2;
		var halfHeight = height / 2;

		Points = new Point[]
		{
			new(new(location.X - halfWidth, location.Y - halfHeight, location.Z), color, new(1.0f, 0.0f)),
			new(new(location.X + halfWidth, location.Y - halfHeight, location.Z), color, new(0.0f, 0.0f)),
			new(new(location.X + halfWidth, location.Y + halfHeight, location.Z), color, new(0.0f, 1.0f)),
			new(new(location.X - halfWidth, location.Y + halfHeight, location.Z), color, new(1.0f, 1.0f))
		};
	}

	private Rectangle(Rectangle rectangle) :
		base(rectangle.Width, rectangle.Height, rectangle.RotationX, rectangle.RotationY, rectangle.RotationZ)
	{
		Points = new Point[rectangle.Points.Length];

		for (var point = 0; point < Points.Length; point++)
			Points[point] = new(rectangle.Points[point]);
	}

	public override void CopyTo(out TwoDimensionalBase twoDimensional)
		=> twoDimensional = new Rectangle(this);
}