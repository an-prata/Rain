using Xunit;
using Rain.Engine;
using Rain.Engine.Texturing;

namespace Rain.Engine.Tests;

public class EfficientTextureGroupTests
{
	[Fact]
	public void IndexText()
	{
		var shader = new ShaderProgram(new ShaderComponent[]
		{
			new(ShaderCompenentType.VertexShader, "./Resources/vertex_shader.glsl"),
			new(ShaderCompenentType.FragmentShader, "./Resources/fragment_shader.glsl"),
		});

		var texture0 = new Texture(shader, TextureUnit.Unit0, "texture0");
		var texture1 = new Texture(shader, TextureUnit.Unit1, "texture1");
		var texture2 = new Texture(shader, TextureUnit.Unit2, "texture2");
		var texture3 = new Texture(shader, TextureUnit.Unit3, "texture3");

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