// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

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
		=> angle * (PI / 180);

	/// <summary>
	/// Convert Radians to Degrees.
	/// </summary>
	/// 
	/// <param name="angle">
	/// The angle in Radians.
	/// </param>
	public static double RadiansToDegrees(double angle)
		=> angle / (PI / 180);

	/// <summary>
	/// Gets an angle from three <c>Vertex</c> instances.
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
	public static Angle GetAngle(Vertex angleVertex, Vertex a, Vertex b)
	{
		var lineA = angleVertex.GetDistanceBetween(a);
		var lineB = angleVertex.GetDistanceBetween(b);
		var lineAB = a.GetDistanceBetween(b);

		var part1 = (lineA * lineA) + (lineB * lineB) - (lineAB * lineAB);
		var part2 = 2.0 * lineA * lineB;

		return new() { Radians = Acos(part1 / part2) };
	}
	
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
		=> GetAngle(angleVertex.Vertex, a.Vertex, b.Vertex);
}