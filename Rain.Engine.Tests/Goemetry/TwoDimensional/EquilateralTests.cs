// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Xunit;
using Rain.Engine.Geometry;
using Rain.Engine.Geometry.TwoDimensional;

namespace Rain.Engine.Tests.Geometry.TwoDimensional;

public class EquilateralTests
{
	[Theory]
	[InlineData(0.0f, 0.0f, 0.0f, 3, 0.0f)]
	[InlineData(10.0f, 10.0f, 10.0f, 10, 5.0f)]
	[InlineData(250.0f, 10.0f, 0.0f, 12, 5.5f)]
	[InlineData(78.1f, 33.33f, 8.8f, 47, 0.3f)]
	public void ConstructorTest(float x, float y, float z, int sides, float radius)
	{
		var location = new Vertex(x, y, z);
		var equilateral = new Equilateral(location, radius, sides);

		Assert.Equal(location, equilateral.Location);
		Assert.Equal(location, equilateral.Points[0].Vertex);
		Assert.Equal(radius * 2.0f, equilateral.Width);
		Assert.Equal(radius * 2.0f, equilateral.Height);
		Assert.Equal(sides, equilateral.Sides.Length);

		// There will always be some inacuracy with this, but 4 decimal places of precision should suffice for now.
		var sideLength = equilateral.Sides[0].Item1.GetDistanceBetween(equilateral.Sides[0].Item2);
		sideLength = Math.Round(sideLength, 4);

		foreach (var side in equilateral.Sides)
		{
			var s = sideLength = Math.Round(sideLength, 4);
			s = Math.Round(s, 4);

			Assert.Equal(sideLength, s);

			sideLength = side.Item1.GetDistanceBetween(side.Item2);
			sideLength = Math.Round(sideLength, 4);
		}

		for (var element = 0; element < equilateral.Elements.Length; element += 3)
			Assert.Equal((uint)0, equilateral.Elements[element]);
	}

	[Theory]
	[InlineData(0.0f, 0.0f, 0.0f, 3, 0.0f)]
	[InlineData(10.0f, 10.0f, 10.0f, 10, 5.0f)]
	[InlineData(250.0f, 10.0f, 0.0f, 12, 5.5f)]
	[InlineData(78.1f, 33.33f, 8.8f, 47, 0.3f)]
	public void SidesOrderTest(float x, float y, float z, int sides, float radius)
	{
		var location = new Vertex(x, y, z);
		var equilateral = new Equilateral(location, radius, sides);

		var previousSide = equilateral.Sides[0];

		for (var side = 1; side < equilateral.Sides.Length; side++)
		{
			Assert.Equal(previousSide.Item2, equilateral.Sides[side].Item1);
			previousSide = equilateral.Sides[side];
		}
	}

	[Fact]
	public void LessThanTwoSidesExceptionTest()
	{
		var location = new Vertex(1.0f, 1.0f, 1.0f);

		for (var sides = 1024; sides > -1024; sides--)
		{
			if (sides <= 2)
			{
				var action = () => new Equilateral(location, 1.0f, sides);
				var exception = Assert.Throws<InvalidOperationException>(action);
				Assert.Equal($"Cannot create an Equilateral with {sides} sides", exception.Message);
			}
			else
			{
				// Just check if it will throw the exception when it shouldn't.
				var equilateral = new Equilateral(location, 1.0f, sides);
			}
		}
	}
}