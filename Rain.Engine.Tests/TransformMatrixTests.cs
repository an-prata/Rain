using Rain.Engine;
using Xunit;

namespace Rain.Engine.Tests;

public class TransformMatrixTests
{
    [Fact]
    public void MultiplicationTest()
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
}