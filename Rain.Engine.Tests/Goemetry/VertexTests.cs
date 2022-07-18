using Xunit;
using OpenTK.Mathematics;
using Rain.Engine.Geometry;

namespace Rain.Engine.Tests.Geometry;

public class VertexTests
{
	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f, 9.0f, 3.0f, 1.0f, 7.0)]
	[InlineData(-2.0f, 3.0f, 1.0f, 9.0f, -3.0f, 1.0f, 12.529964086141668)]
	[InlineData(0.0f, 0.0f, 0.0f, 9.0f, -3.0f, 0.0f, 9.486832980505138)]
	public void GetDistanceBetweenTest(float x1, float y1, float z1, float x2, float y2, float z2, double expectedDistance)
	{
		var vertex0 = new Vertex(x1, y1, z1);
		var vertex1 = new Vertex(x2, y2, z2);
		Assert.True(vertex0.GetDistanceBetween(vertex1) == expectedDistance);
	}

	[Theory]
	[InlineData(2)]
	[InlineData(3)]
	[InlineData(7)]
	public void VertexConstructorArgumentExceptionTest(int length)
	{
		var badArgument = new float[length];
		Assert.Throws<ArgumentException>(() => { new Vertex(badArgument); } );
	}

	[Theory]
	[InlineData(0.0f, 0.0f, 0.0f)]
	[InlineData(3.0f, 2.0f, 8.0f)]
	[InlineData(-12.0f, 32.0f, 1.0f)]
	public void VertexVector3ConstructorTest(float x, float y, float z)
	{
		var vector = new Vector3(x, y, z);
		var vertex = new Vertex(vector);

		Assert.Equal(vector.X, vertex.X);
		Assert.Equal(vector.Y, vertex.Y);
		Assert.Equal(vector.Z, vertex.Z);
	}

	[Theory]
	[InlineData(0.0f, 0.0f, 0.0f)]
	[InlineData(3.0f, 2.0f, 8.0f)]
	[InlineData(-12.0f, 32.0f, 1.0f)]
	public void VertexToVector3Test(float x, float y, float z)
	{
		var vertex = new Vertex(x, y, z);
		var vector = vertex.ToVector3();

		Assert.Equal(new Vector3(x, y, z), vector);
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f, 3.0, 5.0f, 6.0f, 4.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f, -2.0, -4.0f, 1.0f, -1.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 7.0, 7.0f, 7.0f, 7.0f)]
	public void ScalaarAdditionTest(float x, float y, float z, double scalaar, float eX, float eY, float eZ)
	{
		var vertex = new Vertex(x, y, z) + scalaar;
		var vertexSingle = new Vertex(x, y, z) + (float)scalaar;

		Assert.Equal(vertex, new Vertex(eX, eY, eZ));
		Assert.Equal(vertex, vertexSingle);
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f, 3.0, -1.0f, 0.0f, -2.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f, -2.0, 0.0f, 5.0f, 3.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 7.0, -7.0f, -7.0f, -7.0f)]
	public void ScalaarSubtractionTest(float x, float y, float z, double scalaar, float eX, float eY, float eZ)
	{
		var vertex = new Vertex(x, y, z) - scalaar;
		var vertexSingle = new Vertex(x, y, z) - (float)scalaar;

		Assert.Equal(vertex, new Vertex(eX, eY, eZ));
		Assert.Equal(vertex, vertexSingle);
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f, 3.0, 6.0f, 9.0f, 3.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f, -2.0, 4.0f, -6.0f, -2.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 7.0, 0.0f, 0.0f, 0.0f)]
	public void ScalaarMultiplicationTest(float x, float y, float z, double scalaar, float eX, float eY, float eZ)
	{
		var vertex = new Vertex(x, y, z) * scalaar;
		var vertexSingle = new Vertex(x, y, z) * (float)scalaar;

		Assert.Equal(vertex, new Vertex(eX, eY, eZ));
		Assert.Equal(vertex, vertexSingle);
	}

	[Theory]
	[InlineData(6.0f, 3.0f, 0.0f, 3.0, 2.0f, 1.0f, 0.0f)]
	[InlineData(-2.0f, 8.0f, 1.0f, 2.0, -1.0f, 4.0f, 0.5f)]
	[InlineData(0.0f, 0.0f, 0.0f, 7.0, 0.0f, 0.0f, 0.0f)]
	public void ScalaarDivisionTest(float x, float y, float z, double scalaar, float eX, float eY, float eZ)
	{
		var vertex = new Vertex(x, y, z) / scalaar;
		var vertexSingle = new Vertex(x, y, z) / (float)scalaar;

		Assert.Equal(vertex, new Vertex(eX, eY, eZ));
		Assert.Equal(vertex, vertexSingle);
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f, 9.0f, 3.0f, 1.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f, 9.0f, -3.0f, 1.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 9.0f, -3.0f, 0.0f)]
	public void NonEqualityTest(float x1, float y1, float z1, float x2, float y2, float z2)
	{
		var vertex0 = new Vertex(x1, y1, z1);
		var vertex1 = new Vertex(x2, y2, z2);

		Assert.NotEqual(vertex0, vertex1);
	}
}