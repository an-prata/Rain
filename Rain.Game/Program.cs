using Rain.Engine;
using Rain.Engine.Geometry;
using Rain.Engine.Extensions;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var rectangle0 = new Rectangle(new(0.0f, 0.0f, 1.0f), 1.0f, 1.0f, new(255, 0, 255));

		var rectangle1 = new Rectangle(new(-1.0f, -1.0f, 1.0f), 1.0f, 1.0f, new(0, 255, 255));

		var rectangle2 = new Rectangle(new(-1.0f, 0.0f, 1.0f), 1.0f, 1.0f, new(255, 255, 0));

		var rectangle3 = new Rectangle(new(0.0f, -1.0f, 1.0f), 1.0f, 1.0f, new(128, 128, 255));

		var models = new IModel[] { rectangle0, rectangle1, rectangle2, rectangle3 };
		var scene = new Scene(models);

		using var game = new GameWindow(new GameOptions
		{
			Width = 1280,
			Height = 720,
			StartingScene = scene
		});

		game.ActiveScene = scene;
		game.Run();
	}
}