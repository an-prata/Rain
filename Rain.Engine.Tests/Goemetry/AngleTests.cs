using Xunit;
using Rain.Engine.Geometry;

namespace Rain.Engine.Tests.Geometry;

public class AngleTests
{
	[Theory]
	[InlineData(0.0f,	-2.0f,	0.0f,
				7.0f,	0.0f,	0.0f,
				0.0f,	0.0f,	0.0f,
				90.0
	)]
	[InlineData(0.0f,	0.0f,	0.0f,
				7.0f,	0.0f,	3.0f,
				11.0f,	-3.0f,	0.0f,
				37.1368973146497
	)]
	public void GetAngleTest(float aX, float aY, float aZ,
							 float bX, float bY, float bZ,
							 float vX, float vY, float vZ,
							 double expectedAngle)
	{
		var angle = Angle.GetAngle(new Vertex(vX, vY, vZ), new Vertex(aX, aY, aZ), new Vertex(bX, bY, bZ));
		Assert.True(angle.Degrees == expectedAngle);
	}

	[Theory]
	[InlineData(0.0)]
	[InlineData(1.0)]
	[InlineData(45.0)]
	[InlineData(90.0)]
	[InlineData(180.0)]
	[InlineData(273.22)]
	public void RadianAndDegreeConversionTest(double degrees)
	{
		var radians = Angle.DegreesToRadians(degrees);
		Assert.True(Angle.RadiansToDegrees(radians) == degrees);
	}
}