// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using System.Diagnostics.CodeAnalysis;
using static System.Math;

namespace Rain.Engine.Geometry;

/// <summary>
/// A struct representing an angle in both Degrees and Radians.
/// </summary>
public struct Angle : IEquatable<Angle>
{
	public const double TwoPi = PI * 2;

	public const double DegreeOverRadian = PI / 180;

	private double radians;

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
	public double Radians 
	{ 
		get => radians;
		set => radians = value % TwoPi; 
	}

	/// <summary>
	/// Convert Degrees to Radians.
	/// </summary>
	/// 
	/// <param name="angle">
	/// The angle in Degrees.
	/// </param>
	public static double DegreesToRadians(double angle)
		=> angle * DegreeOverRadian;

	/// <summary>
	/// Convert Radians to Degrees.
	/// </summary>
	/// 
	/// <param name="angle">
	/// The angle in Radians.
	/// </param>
	public static double RadiansToDegrees(double angle)
		=> angle / DegreeOverRadian;

	/// <summary>
	/// Produces a new <c>Angle</c> from a degree value. 
	/// </summary>
	/// 
	/// <param name="degrees">
	/// The degree value of the <c>Angle</c>.
	/// </param>
	public static Angle FromDegrees(double degrees)
		=> new() { Degrees = degrees };

	/// <summary>
	/// Produces a new <c>Angle</c> from a radian value. 
	/// </summary>
	/// 
	/// <param name="radians">
	/// The radian value of the <c>Angle</c>.
	/// </param>
	public static Angle FromRadians(double radians)
		=> new() { Radians = radians };

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
	
	public override int GetHashCode()
		=> Radians.GetHashCode();
	
	public bool Equals(Angle angle)
		=> Radians == angle.Radians;

	public override bool Equals(object? obj)
	{
		if (obj is null)
			return false;
	
		if (obj.GetType() != typeof(Angle))
			return false;

		return Equals((Angle)obj);
	}

	public static bool operator ==(Angle a, Angle b)
		=> a.Equals(b);

	public static bool operator !=(Angle a, Angle b)
		=> !a.Equals(b);

	public static Angle operator +(Angle a, Angle b)
		=> new() { Radians = a.Radians + b.Radians };

	public static Angle operator -(Angle angle)
		=> new() { Radians = -angle.Radians };

	public static Angle operator -(Angle a, Angle b)
		=> new() { Radians = a.Radians - b.Radians };

	public static Angle operator *(Angle a, Angle b)
		=> new() { Radians = a.Radians * b.Radians };

	public static Angle operator /(Angle a, Angle b)
		=> new() { Radians = a.Radians / b.Radians };

	public static Angle operator *(Angle a, double b)
		=> new() { Radians = a.Radians * b };

	public static Angle operator /(Angle a, double b)
		=> new() { Radians = a.Radians / b };
}