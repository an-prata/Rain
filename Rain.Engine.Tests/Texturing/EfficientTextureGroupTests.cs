// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Xunit;
using Rain.Engine.Texturing;

namespace Rain.Engine.Tests;

public class EfficientTextureGroupTests
{
	[Fact]
	public void IndexText()
	{
		var texture0 = new Texture(@"Resources/interesting.bmp");
		var texture1 = new Texture(@"Resources/greg.bmp");
		var texture2 = new Texture(@"Resources/suprise.bmp");
		var texture3 = new Texture(@"Resources/garfield.bmp");

		var textures = new Texture[]
		{
			texture0, texture0, texture0, texture1,
			texture1, texture0, texture2, texture2,
			texture3, texture3, texture1, texture0
		};

		var textureGroup = new EfficientTextureGroup(textures);

		for (var texture = 0; texture < textures.Length; texture++)
			Assert.True(textures[texture] == textureGroup[texture]);
	}
}