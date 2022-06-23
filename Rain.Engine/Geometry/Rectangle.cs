namespace Rain.Engine.Geometry;

public class Rectangle : Model, IModel
{
	public override int NumberOfPoints { get => 4; }

	public override int NumberOfElements { get => 6;}

	public override uint[] Elements { get => new uint[] { 0, 1, 2, 1, 3, 2 }; }

	/// <summary> Creates a new <c>Rectangle</c> from an array of <c>Point</c>s. </summary>
	/// <param name="points"> An array of <c>Point</c> objects of length <c>Rectangle.BufferSize</c>. </param>
	public Rectangle(Point[] points) : base(points) { }

	/// <summary> Creates a new <c>Rectangle</c>. </summary>
	/// <param name="location"> 
	/// The location of the <c>Rectangle</c> and the point with the smallest X, Y, and Z values. 
	/// </param>
	/// <param name="lengthX"> The <c>Rectangle</c>'s length along the X axis. </param>
	/// <param name="lengthY"> The <c>Rectangle</c>'s length along the Y axis.</param>
	/// <param name="color"> The color of the <c>Rectangle</c>. </param>
	public Rectangle(Vertex location, float lengthX, float lengthY, Color color) : base(new Point[]
	{
		new Point(location, color, new(0.0f, 0.0f)),
		new Point(new Vertex(location.X + lengthX, location.Y, location.Z), color, new(1.0f, 0.0f)),
		new Point(new Vertex(location.X, location.Y + lengthY, location.Z), color, new(0.0f, 1.0f)),
		new Point(new Vertex(location.X + lengthX, location.Y + lengthY, location.Z), color, new(1.0f, 1.0f))
	}) { }
}