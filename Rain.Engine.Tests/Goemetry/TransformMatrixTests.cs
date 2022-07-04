using Xunit;
using Rain.Engine.Geometry;

namespace Rain.Engine.Tests.Geometry;

public class TransformMatrixTests
{
	[Fact]
	public void MatrixMultiplicationTest()
	{
		var matrix1 = new float[4,4]
		{
			{ 4.0f, 2.0f, 0.0f, 0.0f },
			{ 0.0f, 8.0f, 1.0f, 0.0f },
			{ 0.0f, 1.0f, 0.0f, 0.0f },
			{ 0.0f, 0.0f, 0.0f, 0.0f }
		};

		var matrix2 = new float[4,4]
		{
			{ 4.0f, 2.0f, 1.0f, 0.0f },
			{ 2.0f, 0.0f, 4.0f, 0.0f },
			{ 9.0f, 4.0f, 2.0f, 0.0f },
			{ 0.0f, 0.0f, 0.0f, 0.0f }
		};

		var transformMatrix1 = new TransformMatrix(matrix1);
		var transformMatrix2 = new TransformMatrix(matrix2);

		var productMatrix = new float[4,4]
		{
			{ 20.0f, 8.0f, 12.0f, 0.0f },
			{ 25.0f, 4.0f, 34.0f, 0.0f },
			{ 2.0f, 0.0f, 4.0f, 0.0f },
			{ 0.0f, 0.0f, 0.0f, 0.0f }
		};

		Assert.True(transformMatrix1 * transformMatrix2 == new TransformMatrix(productMatrix));
	}

	[Theory]
	[InlineData(0.3f, 0.2f, 1.0f,
				0.2f, 0.2f, 0.0f,
				0.5f, 0.4f, 1.0f)]

	// Floating point error, 0.1f + 0.6f = 0.70000005
	[InlineData(0.5f, 0.1f,         1.0f,
				0.3f, 0.6f,         0.0f,
				0.8f, 0.70000005f,  1.0f)] 

	[InlineData(0.5f,   0.2f,   1.0f,
				20.2f,  300.3f, 0.0f,
				20.7f,   300.5f, 1.0f)]
	public void TranslationTest(float x,    float y,    float z, 
								float tX,   float tY,   float tZ, 
								float eX,   float eY,   float eZ)
	{
		var vertex = new Vertex(x, y, z);
		var translation = TransformMatrix.CreateTranslationMatrix(tX, tY, tZ);
		Assert.True(translation * vertex == new Vertex(eX, eY, eZ));
	}

	[Theory]
	[InlineData(270.0f,	Axes.Z,
				2.0f,	3.0f,	1.0f,
				3.0f,	-2.0f,	1.0f
	)]
	// More floating point error/inaccuracies inherent to the data type.
	[InlineData(180.0f,	Axes.Z,
				2.0f,		3.0f,		1.0f,
				-1.9999998,	-3.0000002,	1.0f
	)]
	[InlineData(90.0f,	Axes.X,
				5.0f,	1.0f,	3.0f,
				5.0f,	-3.0f,	0.9999999f
	)]
	public void RotationTest(float angle, Axes axis,
							 float x,   float y,    float z, 
							 float eX,  float eY,   float eZ)
	{
		var vertex = new Vertex(x, y, z);
		var transformMatrix = TransformMatrix.CreateRotationMatrix(angle, axis);
		Assert.True(vertex * transformMatrix == new Vertex(eX, eY, eZ));
	}
}