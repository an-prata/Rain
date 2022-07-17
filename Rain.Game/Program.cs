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
		var cubeBase = new Rectangle(new(0.5f, 0.5f, -30.0f), 15.0f, 15.0f, new(203, 178, 238));
		var textures = new Texture[] { new("interesting.bmp", 0.3f) };

		var texturedBase = new TexturedFace(cubeBase, new Texture[] { new("interesting.bmp", 0.3f) } );

		var cubeTextures = new EfficientTextureGroup[] 
		{ 
			new(textures),
			new(textures),
			new(textures),
			new(textures)
		};

		var cube = Prism.MakePrism(texturedBase, cubeTextures, 15.0f);
		var models = new IRenderable[] { cube };
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