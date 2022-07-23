// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry;

/// <summary>
/// A class for creating and manipulating triangles.
/// </summary>
public class Triangle : TwoDimensionalBase
{
	public override uint[] Elements => new uint[] { 0, 1, 2 };

	public override Point[] Points { get; set; }

	public override int Sides { get => 3; }

	/// <summary>
	/// Creates a new <c>Triangle</c> from an array of <c>Point</c> objects.
	/// </summary>
	/// 
	/// <param name="points">
	/// An array of <c>Points</c>s representing a triangle.
	/// </param>
	public Triangle(Point[] points) :
		base((float)points[0].GetDistanceBetween(points[1]), (float)points[0].Vertex.GetMidPoint(points[1].Vertex).GetDistanceBetween(points[2].Vertex))
	{
		if (points.Length != 3)
			throw new Exception($"{nameof(points)} is not length 3 (Given length: {points.Length}).");

		Points = points;
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
	public Triangle(Vertex location, float width, float height) :
		base(width, height)
	{
		Points = new Point[]
		{
			new(location, new TextureCoordinate(0.0f, 0.0f)),
			new(new(location.X + width, location.Y, location.Z), new TextureCoordinate(1.0f, 0.0f)),
			new(new(location.X, location.Y + height, location.Z), new TextureCoordinate(0.0f, 1.0f)),
		};
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
	public Triangle(Vertex location, float width, float height, Color color) :
		base(width, height)
	{
		Points = new Point[]
		{
			new(location, color, new(0.0f, 0.0f)),
			new(new(location.X + width, location.Y, location.Z), color, new(1.0f, 0.0f)),
			new(new(location.X, location.Y + height, location.Z), color, new(0.0f, 1.0f)),
		};
	}

	private Triangle(Triangle triangle) :
		base(triangle.Width, triangle.Height, triangle.RotationX, triangle.RotationY, triangle.RotationZ)
	{
		Points = new Point[triangle.Points.Length];
		triangle.Points.CopyTo(Points, 0);
	}

	public override void CopyTo(out TwoDimensionalBase triangle)
		=> triangle = new Triangle(this);
}