// Copyright (c) 2022 Evan Overman (https://an-prata.it). Licensed under the MIT License.
// See LICENSE file in repository root for complete license text.

using Rain.Engine;
using Rain.Engine.Geometry;
using Rain.Engine.Geometry.TwoDimensional;
using Rain.Engine.Rendering;
using Rain.Engine.Texturing;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var textures = new Texture[] { new Texture("interesting.bmp") };

		var textureGroups = new EfficientTextureGroup[] 
		{ 
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures)
		};

		var textureGroups2 = new EfficientTextureGroup[] 
		{ 
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures),
			new EfficientTextureGroup(textures)
		};

		var face = new Equilateral(new Vertex(1.0f, 1.0f, -5.0f), 1.0f, 5, new Color(255, 0, 255));

		face.Rotate(Angle.FromDegrees(892.0f), Axes.X);
		face.Rotate(Angle.FromDegrees(90.0f), Axes.Y);

		/* var shapeBase = new Face(face);
		var prism = new Pyramid(shapeBase, 10.0f); */
		var shapeBase = new TexturedFace(face, textures);
		var prism = new Pyramid(shapeBase, textureGroups, 1.0f);

		var t_face = new Equilateral(new Vertex(-1.0f, -1.0f, -5.0f), 1.0f, 8, new Color(0, 255, 255));

		t_face.Rotate(Angle.FromDegrees(892.0f), Axes.X);
		t_face.Rotate(Angle.FromDegrees(90.0f), Axes.Y);

		/* var t_shapeBase = new Face(t_face);
		var t_prism = new Prism(t_shapeBase, 10.0f); */
		var t_shapeBase = new TexturedFace(t_face, textures);
		var t_prism = new Prism(t_shapeBase, textureGroups2, 1.0f);

		var floor = new TexturedFace(new Rectangle(new Vertex(0.0f, -3.0f, 0.0f), 8.0f, 8.0f), textures);
		floor.Rotate(Angle.FromDegrees(90.0), Axes.X);
		var p_floor = new Prism(floor, 0.1f);

		var floor1 = new TexturedFace(new Rectangle(new Vertex(0.0f, 20.0f, -90.0f), 15.0f, 15.0f), textures);
		floor1.Rotate(Angle.FromDegrees(180.0), Axes.X);
		var p_floor1 = new Prism(floor1, 0.1f);

		var models = new IRenderable[] { prism, t_prism, p_floor, p_floor1 };
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