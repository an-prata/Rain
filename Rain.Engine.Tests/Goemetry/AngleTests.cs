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

		var vertexPoint = new Vertex(vX, vY, vZ);
		var lineAPoint = new Vertex(aX, aY, aZ);
		var lineBPoint = new Vertex(bX, bY, bZ);

		Assert.Equal(Angle.GetAngle(vertex, lineA, lineB).Degrees, expectedAngle);
		Assert.Equal(Angle.GetAngle(vertexPoint, lineAPoint, lineBPoint).Degrees, expectedAngle);
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
}