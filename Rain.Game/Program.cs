using Rain.Engine;
using Rain.Engine.Geometry;
using Rain.Engine.Rendering;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var rectangle0 = new Rectangle(new(0.5f, 0.5f, 1.0f), 1.0f, 1.0f, new(255, 0, 255));
		rectangle0.Rotate(90.0f, Axes.Z, RotationDirection.Clockwise);

		var rectangle1 = new Rectangle(new(-0.5f, -0.5f, 1.0f), 1.0f, 1.0f, new(0, 255, 255));
		rectangle1.Rotate(90.0f, Axes.Z, RotationDirection.CounterClockwise); 

		var rectangle2 = new Rectangle(new(-0.5f, 0.5f, 1.0f), 1.0f, 1.0f, new(255, 255, 0));

		var rectangle3 = new Rectangle(new(0.5f, -0.5f, 1.0f), 1.0f, 1.0f, new(128, 128, 255));
		rectangle3.Rotate(180.0f, Axes.Z);

		var models = new IRenderable[] 
		{ 
			Solid.SolidFromITwoDimensional(rectangle0, new("interesting.bmp")),
			Solid.SolidFromITwoDimensional(rectangle1, new("greg.bmp")), 
			Solid.SolidFromITwoDimensional(rectangle2, new("suprise.bmp")), 
			Solid.SolidFromITwoDimensional(rectangle3, new("garfield.bmp"))
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