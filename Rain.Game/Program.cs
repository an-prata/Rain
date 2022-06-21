using Rain.Engine;
using Rain.Engine.Geometry;
using Rain.Engine.Extensions;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var triangle = new Rectangle(new(-1.0f, -1.0f, 1.0f), 2.0f, 2.0f, new(255, 255, 255, 255));

		triangle.Points[0].Color = new(255, 255, 255);
		triangle.Points[1].Color = new(255, 255, 255);
		triangle.Points[2].Color = new(255, 255, 255);
		triangle.Points[3].Color = new(255, 255, 255);

		var models = new IModel[] { triangle };
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