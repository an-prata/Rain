using Rain.Engine;
using Rain.Engine.Geometry;
using Rain.Engine.Extensions;

namespace Rain.Game;

class Program
{
	static void Main()
	{
		var triangle = new Rectangle(new(-0.3f, -0.3f, 1.0f), 0.4f, 0.4f, new(255, 255, 255, 255));

		triangle.Points[0].Color = new(255, 0, 0);
		triangle.Points[1].Color = new(0, 255, 0);
		triangle.Points[2].Color = new(0, 0, 255);

		var rotation = TransformMatrix.CreateTranslationMatrix(0.6f, 0.6f, 0.0f);

		triangle = rotation * triangle;

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