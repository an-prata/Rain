using static System.Math;

namespace Rain.Engine.Geometry;

/// <summary>
/// A struct representing an angle in both Degrees and Radians.
/// </summary>
public struct Angle
{
	/// <summary>
	/// This <c>Angle</c> in Degrees.
	/// </summary>
	public double Degrees
	{
		get => RadiansToDegrees(Radians);
		set => Radians = DegreesToRadians(value);
	}

	/// <summary>
	/// This <c>Angle</c> in Radians.
	/// </summary>
	public double Radians { get; set; }

	/// <summary>
	/// Convert Degrees to Radians.
	/// </summary>
	/// 
	/// <param name="angle">
	/// The angle in Degrees.
	/// </param>
	public static double DegreesToRadians(double angle)
		=> angle * (PI / 180.0f);

	/// <summary>
	/// Convert Radians to Degrees.
	/// </summary>
	/// 
	/// <param name="angle">
	/// The angle in Radians.
	/// </param>
	public static double RadiansToDegrees(double angle)
		=> angle / (PI / 180.0f);
	
	/// <summary>
	/// Gets an angle from three <c>Point</c> instances.
	/// </summary>
	/// 
	/// <param name="angleVertex">
	/// The vertex of the angle to be found.
	/// </param>
	/// 
	/// <param name="a">
	/// The end point of line "a".
	/// </param>
	/// 
	/// <param name="b">
	/// The end point of line "b".
	/// </param>
	/// 
	/// <returns>
	/// A new <c>Angle</c> struct representing the angle.
	/// </returns>
	public static Angle GetAngle(Point angleVertex, Point a, Point b)
	{
		var directionRatioA = a.Vertex - angleVertex.Vertex;
		var directionRatioB = b.Vertex - angleVertex.Vertex;

		var dotProductVertex = Vertex.DotProduct(directionRatioA, directionRatioB);
		var dotProduct = dotProductVertex.X + dotProductVertex.Y + dotProductVertex.Z;

		var magnitudeVertexA = Vertex.DotProduct(a.Vertex, a.Vertex);
		var magnitudeA = magnitudeVertexA.X + magnitudeVertexA.Y + magnitudeVertexA.Z;

		var magnitudeVertexB = Vertex.DotProduct(b.Vertex, b.Vertex);
		var magnitudeB = magnitudeVertexB.X + magnitudeVertexB.Y + magnitudeVertexB.Z;

		return new() { Degrees = Abs(dotProduct / Sqrt(magnitudeA * magnitudeB)) };
	}
}