// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine;
using Rain.Engine.Geometry;
using Rain.Engine.Rendering;
using Rain.Engine.Texturing;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var textures = new Texture[] { new Texture() };

		var textureGroups = new EfficientTextureGroup[] 
		{ 
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures)
		};

		var face = new Rectangle(new Vertex(10.0f, 10.0f, -50.0f), 10.0f, 10.0f);

		face.Rotate(Angle.FromDegrees(892.0f), Axes.X);
		face.Rotate(Angle.FromDegrees(90.0f), Axes.Y);

		var shapeBase = new TexturedFace(face, textures);
		var prism = new Pyramid(shapeBase, textureGroups, 10.0f);

		var models = new IRenderable[] { prism };
		var scene = new Scene(models);

		using var game = new GameWindow(new GameOptions
		{
			Width = 720,
			Height = 720,
			StartingScene = scene,
			ClearColor = new(20, 20, 20)
		});

		game.ActiveScene = scene;
		game.Run();
	}
}