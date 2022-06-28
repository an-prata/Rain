namespace Rain.Engine.Geometry;

public class Triangle : TwoDimensionalBase, IModel
{
	public override int NumberOfPoints { get => 3; }

	public override int NumberOfElements { get => 3;}

	public override uint[] Elements { get => new uint[] { 0, 1, 2 }; }

	/// <summary> Creates a new <c>Triangle</c> from an array of <c>Point</c>s. </summary>
	/// <param name="points"> An array of <c>Point</c> objects of length <c>Triangle.BufferSize</c>. </param>
	public Triangle(Point[] points) : base(points) { }

	/// <summary> Creates a new <c>Triangle</c>. </summary>
	/// <param name="location"> 
	/// The location of the <c>Triangle</c> and the point with the smallest X, Y, and Z values. 
	/// </param>
	/// <param name="lengthX"> The <c>Triangle</c>'s length along the X axis. </param>
	/// <param name="lengthY"> The <c>Triangle</c>'s length along the Y axis.</param>
	/// <param name="color"> The color of the <c>Triangle</c>. </param>
	public Triangle(Vertex location, float lengthX, float lengthY, Color color) : base(new Point[]
	{
		new Point(location, color, new(0.0f, 0.0f)),
		new Point(new(location.X + lengthX, location.Y, location.Z), color, new(1.0f, 0.0f)),
		new Point(new(location.X, location.Y + lengthY, location.Z), color, new(0.0f, 1.0f))
	}) { }
}