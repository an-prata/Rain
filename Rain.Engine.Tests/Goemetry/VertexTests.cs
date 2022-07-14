using Xunit;
using Rain.Engine.Geometry;

namespace Rain.Engine.Tests.Geometry;

public class VertexTests
{
	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f,
				9.0f, 3.0f, 1.0f,
				7.0
	)]
	[InlineData(-2.0f, 	3.0f,	1.0f,
				9.0f, 	-3.0f,	1.0f,
				12.529964086141668
	)]
	[InlineData(0.0f, 	0.0f,	0.0f,
				9.0f, 	-3.0f,	0.0f,
				9.486832980505138
	)]
	public void GetDistanceBetweenTest(float x1, float y1, float z1,
									   float x2, float y2, float z2,
									   double expectedDistance)
	{
		var vertex1 = new Vertex(x1, y1, z1);
		var vertex2 = new Vertex(x2, y2, z2);
		Assert.True(vertex1.GetDistanceBetween(vertex2) == expectedDistance);
	}
}