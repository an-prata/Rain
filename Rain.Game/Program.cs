using Rain.Engine;
using Rain.Engine.Geometry;
using Rain.Engine.Extensions;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var rectangle0 = new Rectangle(new(0.0f, 0.0f, 1.0f), 1.0f, 1.0f, new(255, 0, 255));
		rectangle0.Rotate(90.0f, Axes.Z, RotationDirection.Clockwise);

		var rectangle1 = new Rectangle(new(-1.0f, -1.0f, 1.0f), 1.0f, 1.0f, new(0, 255, 255));
		rectangle1.Rotate(90.0f, Axes.Z, RotationDirection.CounterClockwise); 
		// When running in a non-square window this will appear to make the rectangle a trapazoid,
		// however that is just an effect of displaying on a 1.0f by 1.0f normalized device coordinate plane.

		var rectangle2 = new Rectangle(new(-1.0f, 0.0f, 1.0f), 1.0f, 1.0f, new(255, 255, 0));

		var rectangle3 = new Rectangle(new(0.0f, -1.0f, 1.0f), 1.0f, 1.0f, new(128, 128, 255));
		rectangle3.Rotate(180.0f, Axes.Z);

		var models = new IModel[] { rectangle0, rectangle1, rectangle2, rectangle3 };
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