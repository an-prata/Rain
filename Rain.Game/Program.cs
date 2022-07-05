using Rain.Engine;
using Rain.Engine.Geometry;
using Rain.Engine.Rendering;
using Rain.Engine.Texturing;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var rectangle0 = new Rectangle(new(0.5f, 0.5f, 1.0f), 1.0f, 1.0f, new(255, 255, 255));
		rectangle0.Rotate(90.0f, Axes.Z, RotationDirection.Clockwise);

		var rectangle1 = new Rectangle(new(-0.5f, -0.5f, 1.0f), 1.0f, 1.0f, new(0, 255, 255));
		rectangle1.Rotate(90.0f, Axes.Z, RotationDirection.CounterClockwise); 

		var rectangle2 = new Rectangle(new(-0.5f, 0.5f, 1.0f), 1.0f, 1.0f, new(255, 255, 255));

		var rectangle3 = new Rectangle(new(0.5f, -0.5f, 1.0f), 1.0f, 1.0f, new(128, 128, 255));
		rectangle3.Rotate(180.0f, Axes.Z);

		var models = new IRenderable[] 
		{ 
			Solid.SolidFromITwoDimensional(rectangle0, new Texture[] 
			{ 
				new("interesting.bmp", 1.0f), 
				new("greg.bmp", 0.5f), 
				new("garfield.bmp", 0.25f) 
			}),
			Solid.SolidFromITwoDimensional(rectangle1, new Texture[] { new("greg.bmp") }), 
			Solid.SolidFromITwoDimensional(rectangle2, new Texture[] { new("suprise.bmp") }), 
			Solid.SolidFromITwoDimensional(rectangle3, new Texture[] { new("garfield.bmp") })
		};

		var scene = new Scene(models);

		using var game = new GameWindow(new GameOptions
		{
			Width = 720,
			Height = 720,
			StartingScene = scene
		});

		game.ActiveScene = scene;
		game.Run();
	}
}