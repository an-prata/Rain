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
		/* var rectangle0 = new Rectangle(new(0.25f, 0.25f, 0.0f), 0.5f, 0.5f, new(255, 255, 255));
		rectangle0.Rotate(90.0f, Axes.Z, RotationDirection.Clockwise);

		var rectangle1 = new Rectangle(new(-0.25f, -0.25f, 0.0f), 0.5f, 0.5f, new(0, 255, 255));
		rectangle1.Rotate(90.0f, Axes.Z, RotationDirection.CounterClockwise); 

		var rectangle2 = new Rectangle(new(-0.25f, 0.25f, 0.0f), 0.5f, 0.5f, new(255, 255, 255));

		var rectangle3 = new Rectangle(new(0.25f, -0.25f, 0.0f), 0.5f, 0.5f, new(128, 128, 255));
		rectangle3.Rotate(180.0f, Axes.Z);

		var models = new IRenderable[] 
		{ 
			Solid.SolidFromFace(rectangle0, new Texture[] 
			{ 
				new("interesting.bmp", 0.3f), 
				new("interesting.bmp", 0.5f), 
				new("interesting.bmp", 0.3f), 
				new("test.png", 1.0f)
			}),
			Solid.SolidFromFace(rectangle1, new Texture[] { new("greg.bmp") }), 
			Solid.SolidFromFace(rectangle2, new Texture[] { new("suprise.bmp") }), 
			Solid.SolidFromFace(rectangle3, new Texture[] { new("garfield.bmp") })
		}; */

		var cubeBase = new Rectangle(new(0.0f, 0.0f, -20.0f), 1.0f, 1.0f, new(203, 178, 238));
		var textures = new Texture[] { new("interesting.bmp", 0.3f) };

		var texturedBase = new TexturedFace()
		{
			Textures = new Texture[] { new("interesting.bmp", 0.3f) },
			Face = cubeBase
		};

		var cubeTextures = new EfficientTextureGroup[] 
		{ 
			new(textures),
			new(textures),
			new(textures),
			new(textures)
		};

		var cube = Prism.MakePrism(texturedBase, cubeTextures, 1.0f);
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