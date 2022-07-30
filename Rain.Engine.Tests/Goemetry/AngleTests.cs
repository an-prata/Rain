// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Xunit;
using Rain.Engine.Geometry;

namespace Rain.Engine.Tests.Geometry;

public class AngleTests
{
	[Theory]
	[InlineData(0.0f, -2.0f, 0.0f, 7.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 90.0)]
	[InlineData(0.0f, 0.0f, 0.0f, 7.0f, 0.0f, 3.0f, 11.0f, -3.0f, 0.0f, 37.1368973146497)]
	public void GetAngleTest(float aX, float aY, float aZ,
							 float bX, float bY, float bZ,
							 float vX, float vY, float vZ,
							 double expectedAngle)
	{
		var vertex = new Vertex(vX, vY, vZ);
		var lineA = new Vertex(aX, aY, aZ);
		var lineB = new Vertex(bX, bY, bZ);

		var vertexPoint = new Point(new Vertex(vX, vY, vZ));
		var lineAPoint = new Point(new Vertex(aX, aY, aZ));
		var lineBPoint = new Point(new Vertex(bX, bY, bZ));

		var angle = Angle.GetAngle(vertex, lineA, lineB);
		var angleFromPoints = Angle.GetAngle(vertexPoint, lineAPoint, lineBPoint);

		Assert.Equal(expectedAngle, angle.Degrees);
		Assert.Equal(expectedAngle, angleFromPoints.Degrees);
		Assert.Equal(angle, angleFromPoints);
		Assert.Equal(angle.GetHashCode(), angleFromPoints.GetHashCode());
	}

	[Theory]
	[InlineData(0.0, 0.0)]
	[InlineData(180.0, Math.PI)]
	[InlineData(90.0, Math.PI / 2)]
	[InlineData(45.0, Math.PI / 4)]
	public void FromDegreesAndRadiansTest(double degrees, double radians)
	{
		var angleFromDegrees = Angle.FromDegrees(degrees);
		var angleFromRadians = Angle.FromRadians(radians);

		Assert.Equal(angleFromDegrees, angleFromRadians);
	}

	[Theory]
	[InlineData(0.0)]
	[InlineData(1.0)]
	[InlineData(45.0)]
	[InlineData(90.0)]
	[InlineData(180.0)]
	[InlineData(273.22)]
	public void DegreeSetterTest(double degrees)
	{
		var angle = new Angle { Degrees = degrees };

		Assert.Equal(angle.Degrees, degrees);
	}

	[Theory]
	[InlineData(0.0, 0.0)]
	[InlineData(180.0, Math.PI)]
	[InlineData(90.0, Math.PI / 2)]
	[InlineData(45.0, Math.PI / 4)]
	public void RadianAndDegreeConversionEqualityTest(double degrees, double radians)
	{
		Assert.Equal(Angle.RadiansToDegrees(radians), degrees);
	}

	[Theory]
	[InlineData(8.3f, 8.3f, true)]
	[InlineData(120.0f, 120.0f, true)]
	[InlineData(120.0f, 67.0f, false)]
	public void EqualityTest(double a, double b, bool equal)
	{
		var aAngle = Angle.FromRadians(a);
		var bAngle = Angle.FromRadians(b);
		
		Assert.Equal(equal, aAngle == bAngle);
		Assert.Equal(!equal, aAngle != bAngle);

		// Cast to an object to cover the non-angle Angle.Equals() overload, this is easy to confirm with inlay hints as you
		// can simply check that the parameter name is "obj".
		Assert.Equal(equal, aAngle.Equals((object)bAngle));

		Assert.False(aAngle.Equals(null));
		Assert.False(aAngle.Equals(a));
		Assert.False(aAngle.Equals(b));
		Assert.False(aAngle.Equals(equal));

		Assert.False(bAngle.Equals(null));
		Assert.False(bAngle.Equals(a));
		Assert.False(bAngle.Equals(b));
		Assert.False(bAngle.Equals(equal));
	}

	[Theory]
	[InlineData(192.12f, 0.1256f)]
	[InlineData(1825.0f, 67.9f)]
	[InlineData(0.0f, 900.0f)]
	public void AdditionTest(double a, double b)
	{
		var aAngle = Angle.FromRadians(a);
		var bAngle = Angle.FromRadians(b);
		var expectedAngle = Angle.FromRadians(a + b);

		Assert.Equal(expectedAngle, aAngle + bAngle);
	}

	[Theory]
	[InlineData(192.12f, 0.1256f)]
	[InlineData(1825.0f, 67.9f)]
	[InlineData(0.0f, 900.0f)]
	public void SubtractionTest(double a, double b)
	{
		var aAngle = Angle.FromRadians(a);
		var bAngle = Angle.FromRadians(b);
		var expectedAngle = Angle.FromRadians(((a % Angle.TwoPi) - (b % Angle.TwoPi)) % Angle.TwoPi);

		Assert.Equal(expectedAngle, aAngle - bAngle);
	}

	[Theory]
	[InlineData(192.12f, 0.1256f)]
	[InlineData(1825.0f, 67.9f)]
	[InlineData(0.0f, 900.0f)]
	public void MultiplicationTest(double a, double b)
	{
		var aAngle = Angle.FromRadians(a);
		var bAngle = Angle.FromRadians(b);
		var expectedAngle = Angle.FromRadians(((a % Angle.TwoPi) * (b % Angle.TwoPi)) % Angle.TwoPi);

		Assert.Equal(expectedAngle, aAngle * bAngle);
	}

	[Theory]
	[InlineData(192.12f, 0.1256f)]
	[InlineData(1825.0f, 67.9f)]
	[InlineData(0.0f, 900.0f)]
	public void DivisionTest(double a, double b)
	{
		var aAngle = Angle.FromRadians(a);
		var bAngle = Angle.FromRadians(b);
		var expectedAngle = Angle.FromRadians(((a % Angle.TwoPi) / (b % Angle.TwoPi)) % Angle.TwoPi);

		Assert.Equal(expectedAngle, aAngle / bAngle);
	}
}