// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

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
	[InlineData(2.0f, 3.0f, 1.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f)]
	[InlineData(0.0f, 0.0f, 0.0f)]
	public void NegativeTest(float x, float y, float z)
	{
		var vertex = new Vertex(x, y, z);
		var expected = new Vertex(0.0f, 0.0f, 0.0f) - vertex;

		Assert.Equal(expected, -vertex);
	}

	[Theory]
	[InlineData(6.0f, 3.0f, 0.0f, 3.0, 2.0f, 1.0f, 4.5f, 2.5f, 0.5f)]
	[InlineData(-2.0f, 8.0f, 1.0f, 2.0, -1.0f, 4.0f, 0.0f, 3.5f, 2.5f)]
	[InlineData(0.0f, 0.0f, 0.0f, 7.0, 0.0f, 0.0f, 3.5f, 0.0f, 0.0f)]
	public void MidPointTest(float x0, float y0, float z0, 
							 float x1, float y1, float z1, 
							 float eX, float eY, float eZ)
	{
		var vertex0 = new Vertex(x0, y0, z0);
		var vertex1 = new Vertex(x1, y1, z1);
		var expectedVertex = new Vertex(eX, eY, eZ);

		Assert.Equal(vertex0.GetMidPoint(vertex1), expectedVertex);
	}

	[Theory]
	[InlineData(6.0f, 3.0f, 0.0f, 3.0, 2.0f, 1.0f, 9.0f, 5.0f, 1.0f)]
	[InlineData(-2.0f, 8.0f, 1.0f, 2.0, -1.0f, 4.0f, 0.0f, 7.0f, 5.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 7.0, 0.0f, 0.0f, 7.0f, 0.0f, 0.0f)]
	public void VectorAdditionTest(float x0, float y0, float z0, 
								   float x1, float y1, float z1, 
								   float eX, float eY, float eZ)
	{
		var vertex0 = new Vertex(x0, y0, z0);
		var vertex1 = new Vertex(x1, y1, z1);
		var expectedVertex = new Vertex(eX, eY, eZ);

		Assert.Equal(vertex0 + vertex1, expectedVertex);
	}

	[Theory]
	[InlineData(6.0f, 3.0f, 0.0f, 3.0, 2.0f, 1.0f, 3.0f, 1.0f, -1.0f)]
	[InlineData(-2.0f, 8.0f, 1.0f, 2.0, -1.0f, 4.0f, -4.0f, 9.0f, -3.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 7.0, 0.0f, 0.0f, -7.0f, 0.0f, 0.0f)]
	public void VectorSubtractionTest(float x0, float y0, float z0, 
									  float x1, float y1, float z1, 
									  float eX, float eY, float eZ)
	{
		var vertex0 = new Vertex(x0, y0, z0);
		var vertex1 = new Vertex(x1, y1, z1);
		var expectedVertex = new Vertex(eX, eY, eZ);

		Assert.Equal(vertex0 - vertex1, expectedVertex);
	}

	[Theory]
	[InlineData(3.0f, 4.0f, 5.0f, 7.0, 8.0f, 9.0f, -4.0f, 8.0f, -4.0f)]
	public void VectorCrossProductMultiplicationTest(float x0, float y0, float z0, 
													 float x1, float y1, float z1, 
													 float eX, float eY, float eZ)
	{
		var vertex0 = new Vertex(x0, y0, z0);
		var vertex1 = new Vertex(x1, y1, z1);
		var expectedVertex = new Vertex(eX, eY, eZ);

		Assert.Equal(vertex0 * vertex1, expectedVertex);
		Assert.Equal(Vertex.CrossProduct(vertex0, vertex1), expectedVertex);
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f, 9.0f, 3.0f, 1.0f)]
	[InlineData(9.0f, -3.0f, 1.0f, 9.0f, -3.0f, 1.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f, 9.0f, -3.0f, 1.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f, -2.0f, 3.0f, 1.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 9.0f, -3.0f, 0.0f)]
	public void GetHashCodeTest(float x0, float y0, float z0, float x1, float y1, float z1)
	{
		var vertex0 = new Vertex(x0, y0, z0);
		var vertex1 = new Vertex(x1, y1, z1);

		Assert.Equal(vertex0 == vertex1, vertex0.GetHashCode() == vertex1.GetHashCode());
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f)]
	[InlineData(0.0f, 0.0f, 0.0f)]
	public void EqualsTest(float x, float y, float z)
	{
		var vertex0 = new Vertex(x, y, z);
		var vertex1 = new Vertex(x, y, z);

		Assert.True(vertex0.Equals(vertex1));
		Assert.True(vertex0 == vertex1);
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f, 9.0f, 3.0f, 1.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f, 9.0f, -3.0f, 1.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 9.0f, -3.0f, 0.0f)]
	public void NonEqualTest(float x0, float y0, float z0, float x1, float y1, float z1)
	{
		var vertex0 = new Vertex(x0, y0, z0);
		var vertex1 = new Vertex(x1, y1, z1);

		Assert.True(vertex0 != vertex1);
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f, 9.0f, 3.0f, 1.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f, 9.0f, -3.0f, 1.0f)]
	[InlineData(0.0f, 0.0f, 0.0f, 9.0f, -3.0f, 0.0f)]
	public void NonEqualToOtherTypeTest(float x0, float y0, float z0, float x1, float y1, float z1)
	{
		var vertex = new Vertex(x0, y0, z0);
		var vector = new Vector3(x1, y1, z1);

		Assert.False(vertex.Equals(vector));
	}

	[Theory]
	[InlineData(2.0f, 3.0f, 1.0f)]
	[InlineData(-2.0f, 3.0f, 1.0f)]
	[InlineData(0.0f, 0.0f, 0.0f)]
	public void NotEqualToNullTest(float x, float y, float z)
	{
		var vertex = new Vertex(x, y, z);

		Assert.False(vertex.Equals(null));
	}
}