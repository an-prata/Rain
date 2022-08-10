// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

namespace Rain.Engine.Geometry.TwoDimensional;

/// <summary>
/// A class for creating and manipulating equilateral shapes.
/// </summary>
/// 
/// <remarks>
/// Creates an equilateral by assembling a perimeter of a specified number of points, all equally spaced along a circle's 
/// edge, then it takes each side and uses the center point to make a triangle. Doing this creates a triangle primitive shape 
/// for each side. Think of it like a pizza, but with straight edges on slices rather than a round edge.
/// </remarks>
public class Equilateral : TwoDimensionalBase
{
	private readonly int sides;

	public override uint[] Elements { get; }

	public override Point[] Points { get; set; }

	public override (Point, Point)[] Sides 
	{ 
		get
		{
			var sideTuples = new (Point, Point)[sides];
			
			for (var point = 1; point < Points.Length; point++)
			{
				// Gets the Point after Points[point] unless out of bounds, in which case pulls a pac-man and goes back to the 
				// first non-center Point in the array, effectively skips the Point at index 0, as it is the center point of
				// the Equilateral.
				var item2 = point == sides ? 1 : point + 1;
				var tuple = (Points[point], Points[item2]);
				sideTuples[point - 1] = tuple;
			}

			return sideTuples;
		} 
	}

	public override Vertex Location
	{
		get => Points[0].Vertex;
		set => base.Location = value;
	}

	/// <summary>
	/// Creates a new <c>Equilateral</c>.
	/// </summary>
	/// 
	/// <param name="location">
	/// The location of the <c>Equilateral</c>.
	/// </param>
	/// 
	/// <param name="radius">
	/// The distance from the center of this <c>Equilateral</c> to its first point.
	/// </param>
	/// 
	/// <param name="sides">
	/// The number of sides.
	/// </param>
	public Equilateral(Vertex location, float radius, int sides) : base(2.0f, 2.0f)
	{
		// Throws an InvalidOperationException if sides is less than 2.
		this.sides = sides > 2 ? sides : throw new InvalidOperationException($"Cannot create an Equilateral with {sides} sides");
		Points = new Point[sides + 1];
		Elements = new uint[sides * 3];

		var angleBetweenSides = Angle.FromRadians(Angle.TwoPi / sides);

		for (var triangle = 0; triangle < sides; triangle++)
		{
			var index = triangle * 3;

			Elements[index] = 0;
			Elements[index + 1] = (uint)triangle + 1;
			Elements[index + 2] = triangle + 2 > sides ? 1 : (uint)triangle + 2;
		}

		Points[0] = new Point(new Vertex(0.0f, 0.0f, 1.0f), TextureCoordinate.Center);

		for (var point = 1; point <= sides; point++)
		{
			var angle = Angle.FromRadians(angleBetweenSides.Radians * point);
			var vertex = new TwoDimensionalVertex(angle);
			var textureCoordinate = TextureCoordinate.CoordinateFromAngle(angle);

			Points[point] = new Point(new Vertex((float)vertex.X, (float)vertex.Y, 1.0f), textureCoordinate);
		}

		Scale(radius, radius);
		Location = location;
	}

	/// <summary>
	/// Creates a new <c>Equilateral</c>.
	/// </summary>
	/// 
	/// <param name="location">
	/// The location of the <c>Equilateral</c>.
	/// </param>
	/// 
	/// <param name="radius">
	/// The distance from the center of this <c>Equilateral</c> to its first point.
	/// </param>
	/// 
	/// <param name="sides">
	/// The number of sides.
	/// </param>
	public Equilateral(Vertex location, float radius, int sides, Color color) : base(2.0f, 2.0f)
	{
		// Throws an InvalidOperationException if sides is less than 2.
		this.sides = sides > 2 ? sides : throw new InvalidOperationException($"Cannot create an Equilateral with {sides} sides");
		Points = new Point[sides + 1];
		Elements = new uint[sides * 3];
		
		var angleBetweenSides = Angle.FromRadians(Angle.TwoPi / sides);

		for (var triangle = 0; triangle < sides; triangle++)
		{
			var index = triangle * 3;

			Elements[index] = 0;
			Elements[index + 1] = (uint)triangle + 1;
			Elements[index + 2] = triangle + 2 > sides ? 1 : (uint)triangle + 2;
		}

		Points[0] = new Point(new Vertex(0.0f, 0.0f, 1.0f), color, TextureCoordinate.Center);

		for (var point = 1; point <= sides; point++)
		{
			var angle = Angle.FromRadians(angleBetweenSides.Radians * point);
			var vertex = new TwoDimensionalVertex(angle);
			var textureCoordinate = TextureCoordinate.CoordinateFromAngle(angle);

			Points[point] = new Point(new Vertex((float)vertex.X, (float)vertex.Y, 1.0f), color, textureCoordinate);
		}

		Scale(radius, radius);
		Location = location;
	}

	private Equilateral(Equilateral equilateral) : base(equilateral.Width, equilateral.Height, equilateral.RotationX, equilateral.RotationY, equilateral.RotationZ)
	{
		Points = new Point[equilateral.Points.Length];
		Elements = new uint[equilateral.Elements.Length];

		sides = equilateral.sides;

		equilateral.Points.CopyTo(Points, 0);
		equilateral.Elements.CopyTo(Elements, 0);
	}

	public override void CopyTo(out TwoDimensionalBase twoDimensional)
		=> twoDimensional = new Equilateral(this);
}