// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Xunit;
using Rain.Engine.Geometry;
using Rain.Engine.Rendering;
using Rain.Engine.Texturing;

namespace Rain.Engine.Tests;

public class PrismTests
{
	[Theory]
	[InlineData(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f)]
	[InlineData(10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 892.0f, 90.0f)]
	[InlineData(250.0f, 10.0f, 0.0f, 50.0f, 10.0f, 11.0f, 0.0f, 0.0f)]
	// Just because this test passes does not mean the Prism has been constructed correctly, the same values should be put in
	// a project and rendered on an actual window to confirm that the geometry has been rendered correcly.
	public void ConstructorTest(float x, float y, float z, float lengthX, float lengthY, float lengthZ, float angleX, float angleY)
	{
		var textures = new Texture[] { new Texture() };

		var textureGroups = new EfficientTextureGroup[] 
		{ 
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures)
		};

		var face = new Rectangle(new Vertex(x, y, z), lengthX, lengthY);

		face.Rotate(angleX, Axes.X);
		face.Rotate(angleY, Axes.Y);

		var shapeBase = new TexturedFace(face, textures);
		var prism = new Prism(shapeBase, textureGroups, lengthZ);

		Assert.Equal(lengthX, prism.LengthX);
		Assert.Equal(lengthY, prism.LengthY);
		Assert.Equal(lengthY, prism.LengthY);

		Assert.Equal(angleX, prism.RotationX);
		Assert.Equal(angleY, prism.RotationY);
	}

	[Theory]
	[InlineData(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f)]
	[InlineData(10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 892.0f, 90.0f, 0.0f)]
	[InlineData(250.0f, 10.0f, 0.0f, 50.0f, 10.0f, 11.0f, 0.0f, 0.0f, 900.0f)]
	public void ScalingTest(float x, float y, float z, float lengthX, float lengthY, float lengthZ, float scaleX, float scaleY, float scaleZ)
	{
		var textures = new Texture[] { new Texture() };

		var textureGroups = new EfficientTextureGroup[] 
		{ 
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures)
		};

		var face = new Rectangle(new Vertex(x, y, z), lengthX, lengthY);
		var shapeBase = new TexturedFace(face, textures);
		var prism = new Prism(shapeBase, textureGroups, lengthZ);

		prism.Scale(scaleX, scaleY, scaleZ);

		Assert.Equal(scaleX * lengthX, prism.LengthX);
		Assert.Equal(scaleY * lengthY, prism.LengthY);
		Assert.Equal(scaleZ * lengthZ, prism.LengthZ);
	}

	[Theory]
	[InlineData(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f)]
	[InlineData(10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 10.0f, 892.0f, 90.0f, 0.0f)]
	[InlineData(250.0f, 10.0f, 0.0f, 50.0f, 10.0f, 11.0f, 0.0f, 0.0f, 900.0f)]
	public void RotationTest(float x, float y, float z, float lengthX, float lengthY, float lengthZ, float angleX, float angleY, float angleZ)
	{
		var textures = new Texture[] { new Texture() };

		var textureGroups = new EfficientTextureGroup[] 
		{ 
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures)
		};

		var face = new Rectangle(new Vertex(x, y, z), lengthX, lengthY);
		var shapeBase = new TexturedFace(face, textures);
		var prism = new Prism(shapeBase, textureGroups, lengthZ);

		prism.Rotate(angleX, Axes.X);
		prism.Rotate(angleY, Axes.Y);
		prism.Rotate(angleZ, Axes.Z);

		Assert.Equal(angleX, prism.RotationX);
		Assert.Equal(angleY, prism.RotationY);
		Assert.Equal(angleY, prism.RotationY);
	}
}